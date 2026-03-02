using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class StockController
    {
        private readonly TechStoreDbContext _context;

        public StockController(TechStoreDbContext context)
        {
            _context = context;
        }

        public string RegistrarIngresoStock(string idProducto, int cantidad, string idProveedor,
            DateTime fechaIngreso, decimal precioCompra, DateTime? fechaVencimiento = null, string sucursalId = null)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto == null)
                    return "El producto no existe.";

                var lote = new Lote
                {
                    Id = Guid.NewGuid().ToString(),
                    IdProducto = idProducto,
                    FechaIngreso = fechaIngreso,
                    FechaVencimiento = (producto.Vencimiento && fechaVencimiento.HasValue)
                        ? fechaVencimiento.Value
                        : DateTime.MaxValue,
                    Cantidad = cantidad,
                    PrecioCompra = precioCompra,
                    SucursalId = sucursalId
                };
                _context.Lotes.Add(lote);
                string idLote = lote.Id;

                if (producto.Vencimiento && fechaVencimiento.HasValue)
                {
                    if (!producto.FechaVencimiento.HasValue || fechaVencimiento < producto.FechaVencimiento)
                    {
                        producto.FechaVencimiento = fechaVencimiento;
                    }
                }

                var movimiento = new MovimientoStock(idProducto, cantidad, fechaIngreso, idLote, precioCompra);
                movimiento.Id = Guid.NewGuid().ToString();
                movimiento.IdProveedor = idProveedor;
                _context.MovimientosStock.Add(movimiento);

                producto.ActualizarPrecioCompraPromedio(precioCompra, cantidad);
                producto.StockActual += cantidad;

                _context.SaveChanges();
                return "Ingreso de stock registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar ingreso: {ex.Message}";
            }
        }

        public string RegistrarEgresoStock(string idProducto, int cantidad, DateTime fechaEgreso, TipoEgreso tipoEgreso, string sucursalId = null)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto == null)
                    return "El producto no existe.";

                if (producto.StockActual < cantidad)
                    return "Stock insuficiente para realizar el egreso.";

                var movimiento = new MovimientoStock(idProducto, cantidad, fechaEgreso, tipoEgreso);
                movimiento.Id = Guid.NewGuid().ToString();
                _context.MovimientosStock.Add(movimiento);

                producto.StockActual -= cantidad;

                // Descontar de lotes (FIFO), filtrar por sucursal si se indicó
                var lotesQuery = _context.Lotes
                    .Where(l => l.IdProducto == idProducto && l.Cantidad > 0);

                if (!string.IsNullOrEmpty(sucursalId))
                    lotesQuery = lotesQuery.Where(l => l.SucursalId == sucursalId);

                var lotes = lotesQuery
                    .OrderBy(l => l.FechaVencimiento)
                    .ToList();

                int restante = cantidad;
                foreach (var lote in lotes)
                {
                    if (restante <= 0) break;
                    int descuento = Math.Min(lote.Cantidad, restante);
                    lote.Cantidad -= descuento;
                    restante -= descuento;
                }

                _context.SaveChanges();
                return "Egreso de stock registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar egreso: {ex.Message}";
            }
        }

        public List<MovimientoStock> ListarMovimientos()
        {
            return _context.MovimientosStock.ToList();
        }

        public List<MovimientoStock> ListarMovimientosPorProducto(string idProducto)
        {
            return _context.MovimientosStock.Where(m => m.IdProducto == idProducto).ToList();
        }

        public List<MovimientoStock> ListarMovimientosPorProveedor(string idProveedor)
        {
            return _context.MovimientosStock.Where(m => m.IdProveedor == idProveedor).ToList();
        }

        public List<object> ListarMovimientosConDetalles()
        {
            var movimientos = _context.MovimientosStock.ToList();
            var productos = _context.Productos.ToList();
            var proveedores = _context.Proveedores.ToList();

            return movimientos.Select(m => (object)new
            {
                m.Id,
                Producto = productos.FirstOrDefault(p => p.Id == m.IdProducto)?.Nombre ?? "-",
                Cantidad = m.Cantidad,
                TipoMovimiento = m.TipoMovimiento.ToString(),
                Proveedor = m.IdProveedor != null ? proveedores.FirstOrDefault(p => p.Id == m.IdProveedor)?.Nombre ?? "-" : "-",
                Fecha = m.Fecha.ToShortDateString(),
                TipoEgreso = m.TipoEgreso != null ? m.TipoEgreso.ToString() : "-",
                PrecioCompra = m.PrecioCompra != null ? m.PrecioCompra.ToString() : "-"
            }).OrderBy(d => d.ToString()).ToList();
        }

        public List<object> ListarMovimientosPorProductoConDetalles(string nombreProducto)
        {
            var movimientos = _context.MovimientosStock.ToList();
            var productos = _context.Productos.ToList();
            var proveedores = _context.Proveedores.ToList();

            var productoFiltrado = productos.FirstOrDefault(p => p.Nombre == nombreProducto);
            if (productoFiltrado != null)
                movimientos = movimientos.Where(m => m.IdProducto == productoFiltrado.Id).ToList();

            return movimientos.Select(m => (object)new
            {
                m.Id,
                Producto = productos.FirstOrDefault(p => p.Id == m.IdProducto)?.Nombre ?? "-",
                Cantidad = m.Cantidad,
                TipoMovimiento = m.TipoMovimiento.ToString(),
                Proveedor = m.IdProveedor != null ? proveedores.FirstOrDefault(p => p.Id == m.IdProveedor)?.Nombre ?? "-" : "-",
                Fecha = m.Fecha.ToShortDateString(),
                TipoEgreso = m.TipoEgreso != null ? m.TipoEgreso.ToString() : "-",
                PrecioCompra = m.PrecioCompra != null ? m.PrecioCompra.ToString() : "-"
            }).OrderBy(d => d.ToString()).ToList();
        }

        public List<object> ListarMovimientosPorProveedorConDetalles(string nombreProveedor)
        {
            var movimientos = _context.MovimientosStock.ToList();
            var productos = _context.Productos.ToList();
            var proveedores = _context.Proveedores.ToList();

            var proveedorFiltrado = proveedores.FirstOrDefault(p => p.Nombre == nombreProveedor);
            if (proveedorFiltrado != null)
                movimientos = movimientos.Where(m => m.IdProveedor == proveedorFiltrado.Id).ToList();

            return movimientos.Select(m => (object)new
            {
                m.Id,
                Producto = productos.FirstOrDefault(p => p.Id == m.IdProducto)?.Nombre ?? "-",
                Cantidad = m.Cantidad,
                TipoMovimiento = m.TipoMovimiento.ToString(),
                Proveedor = m.IdProveedor != null ? proveedores.FirstOrDefault(p => p.Id == m.IdProveedor)?.Nombre ?? "-" : "-",
                Fecha = m.Fecha.ToShortDateString(),
                TipoEgreso = m.TipoEgreso != null ? m.TipoEgreso.ToString() : "-",
                PrecioCompra = m.PrecioCompra != null ? m.PrecioCompra.ToString() : "-"
            }).OrderBy(d => d.ToString()).ToList();
        }
        public List<string> ProcesarProductosVencidos(DateTime fechaActual)
        {
            var nombresProcessados = new List<string>();

            var productosVencidos = _context.Productos
                .Where(p => p.Activo && p.Vencimiento && p.FechaVencimiento.HasValue &&
                            p.StockActual > 0)
                .ToList()
                .Where(p => p.FechaVencimiento.Value < fechaActual)
                .ToList();

            foreach (var producto in productosVencidos)
            {
                var lotesVencidos = _context.Lotes
                    .Where(l => l.IdProducto == producto.Id && l.Cantidad > 0)
                    .ToList()
                    .Where(l => l.FechaVencimiento < fechaActual)
                    .ToList();

                foreach (var lote in lotesVencidos)
                {
                    var movimiento = new MovimientoStock(producto.Id, lote.Cantidad, fechaActual, TipoEgreso.Merma);
                    movimiento.Id = Guid.NewGuid().ToString();
                    _context.MovimientosStock.Add(movimiento);

                    producto.StockActual -= lote.Cantidad;
                    lote.Cantidad = 0;
                }

                // Actualizar fecha de vencimiento al próximo lote activo
                var proximoLote = _context.Lotes
                    .Where(l => l.IdProducto == producto.Id && l.Cantidad > 0)
                    .OrderBy(l => l.FechaVencimiento)
                    .FirstOrDefault();
                producto.FechaVencimiento = proximoLote?.FechaVencimiento;

                nombresProcessados.Add(producto.Nombre);
            }

            if (nombresProcessados.Count > 0)
                _context.SaveChanges();

            return nombresProcessados;
        }
    }
}
