using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class VentaController
    {
        private TechStoreDbContext _context;

        public VentaController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        public List<Venta> ListarVentas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Detalles.Select(d => d.Producto))
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        public List<Venta> ListarVentasPorFecha(DateTime desde, DateTime hasta)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Detalles.Select(d => d.Producto))
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        public List<Venta> ListarVentasPorPeriodo(DateTime desde, DateTime hasta)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include("Detalles")
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .OrderByDescending(v => v.Fecha)
                .ToList();
        }

        public List<Venta> ListarVentasPorSucursal(DateTime desde, DateTime hasta, string sucursalId)
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Vendedor.Sucursal)
                .Include("Detalles")
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .ToList();

            if (!string.IsNullOrEmpty(sucursalId))
                ventas = ventas.Where(v => v.Vendedor != null && v.Vendedor.SucursalId == sucursalId).ToList();

            return ventas.OrderByDescending(v => v.Fecha).ToList();
        }

        public List<Venta> ListarVentasPorVendedor(DateTime desde, DateTime hasta, string vendedorId)
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Vendedor.Sucursal)
                .Include("Detalles")
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .ToList();

            if (!string.IsNullOrEmpty(vendedorId))
                ventas = ventas.Where(v => v.VendedorId == vendedorId).ToList();

            return ventas.OrderByDescending(v => v.Fecha).ToList();
        }

        public List<DetalleVenta> ListarDetallesPorProducto(DateTime desde, DateTime hasta, string productoId)
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Detalles.Select(d => d.Producto))
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .ToList();

            var detalles = ventas.SelectMany(v => v.Detalles).ToList();

            if (!string.IsNullOrEmpty(productoId))
                detalles = detalles.Where(d => d.ProductoId == productoId).ToList();

            return detalles;
        }

        public List<object> ObtenerProductosMasVendidos(DateTime desde, DateTime hasta, int topN)
        {
            var detalles = _context.Ventas
                .Include("Detalles.Producto")
                .Where(v => v.Fecha >= desde && v.Fecha < hasta)
                .SelectMany(v => v.Detalles)
                .ToList();

            return detalles
                .GroupBy(d => new { d.ProductoId, Nombre = d.Producto != null ? d.Producto.Nombre : "Desconocido", Categoria = d.Producto != null ? d.Producto.Categoria : "" })
                .Select(g => (object)new
                {
                    Producto = g.Key.Nombre,
                    Categoria = g.Key.Categoria,
                    CantidadVendida = g.Sum(d => d.Cantidad),
                    MontoTotal = g.Sum(d => d.Subtotal).ToString("C")
                })
                .OrderByDescending(r => ((dynamic)r).CantidadVendida)
                .Take(topN)
                .ToList();
        }

        public string RegistrarVenta(Cliente cliente, Vendedor vendedor, List<DetalleVenta> detalles, MetodoPago metodo)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (cliente == null) return "El cliente no puede ser nulo.";
                    if (vendedor == null) return "El vendedor no puede ser nulo.";
                    if (detalles == null || !detalles.Any()) return "La venta debe tener al menos un detalle.";

                    decimal limiteDescuento = cliente.ObtenerLimiteDescuento();

                    // Asignar IDs únicos a la venta y sus detalles
                    var venta = new Venta
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cliente = cliente,
                        Vendedor = vendedor,
                        Fecha = DateTime.Now,
                        DescuentoAplicado = limiteDescuento,
                        MetodoPago = metodo
                    };

                    foreach (var detalle in detalles)
                    {
                        detalle.Id = Guid.NewGuid().ToString();
                        venta.Detalles.Add(detalle);
                    }

                    _context.Ventas.Add(venta);

                    // Usar SucursalId directamente (más confiable que lazy loading)
                    string idSucursal = vendedor.SucursalId;

                    if (string.IsNullOrEmpty(idSucursal))
                        return "El vendedor no tiene una sucursal asignada.";

                    foreach (var detalle in detalles)
                    {
                        int cantidadRequerida = detalle.Cantidad;
                        string idProducto = detalle.ProductoId ?? detalle.Producto?.Id;

                        if (string.IsNullOrEmpty(idProducto))
                            return "Un detalle de venta no tiene producto asignado.";

                        // Buscar lotes de la sucursal para este producto (FIFO por vencimiento)
                        var lotesProducto = _context.Lotes
                            .Where(l => l.SucursalId == idSucursal && l.IdProducto == idProducto && l.Cantidad > 0)
                            .OrderBy(l => l.FechaVencimiento)
                            .ToList();

                        int stockDisponible = lotesProducto.Sum(l => l.Cantidad);
                        if (stockDisponible < cantidadRequerida)
                        {
                            transaction.Rollback();
                            return $"Stock insuficiente para el producto {detalle.Producto?.Nombre ?? idProducto}. Requerido: {cantidadRequerida}, Disponible en sucursal: {stockDisponible}.";
                        }

                        foreach (var lote in lotesProducto)
                        {
                            if (cantidadRequerida == 0) break;

                            if (lote.Cantidad >= cantidadRequerida)
                            {
                                lote.Cantidad -= cantidadRequerida;
                                cantidadRequerida = 0;
                            }
                            else
                            {
                                cantidadRequerida -= lote.Cantidad;
                                lote.Cantidad = 0;
                            }
                        }

                        // Actualizar el stock general del producto
                        var productoBd = _context.Productos.FirstOrDefault(p => p.Id == idProducto);
                        if (productoBd != null)
                        {
                            productoBd.StockActual -= detalle.Cantidad;
                        }

                        // Registrar movimiento de stock (egreso por venta)
                        var movimiento = new MovimientoStock(idProducto, detalle.Cantidad, venta.Fecha, TipoEgreso.Venta);
                        movimiento.Id = Guid.NewGuid().ToString();
                        _context.MovimientosStock.Add(movimiento);
                    }

                    // Si el método de pago es Cuenta Corriente, registrar débito
                    if (metodo == MetodoPago.CuentaCorriente)
                    {
                        decimal montoTotal = venta.MontoTotal;
                        var movimientoCC = new MovimientoCuentaCorriente(
                            cliente.Id,
                            TipoMovimientoCuenta.Debito,
                            montoTotal,
                            $"Venta #{venta.Id} - Cuenta Corriente",
                            venta.Id
                        );
                        _context.MovimientosCuentaCorriente.Add(movimientoCC);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return "Venta registrada exitosamente.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error al registrar la venta: {ex.Message}";
                }
            }
        }
    }
}