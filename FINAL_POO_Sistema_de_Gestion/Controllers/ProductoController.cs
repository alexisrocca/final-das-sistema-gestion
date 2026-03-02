using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class ProductoController
    {
        private TechStoreDbContext _context;

        public ProductoController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        public List<Producto> ListarProductos()
        {
            try
            {
                return _context.Productos.Where(p => p.Activo).ToList();
            }
            catch (Exception)
            {
                return new List<Producto>();
            }
        }

        public List<Producto> ListarTodosLosProductos()
        {
            try
            {
                return _context.Productos.ToList();
            }
            catch (Exception)
            {
                return new List<Producto>();
            }
        }

        public Producto ObtenerProductoPorId(string id)
        {
            return _context.Productos.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> ListarProductosBajoStock(int umbral)
        {
            try
            {
                return _context.Productos.Where(p => p.Activo).ToList().Where(p => p.StockActual < umbral).ToList();
            }
            catch (Exception)
            {
                return new List<Producto>();
            }
        }

        public string AgregarProductoConStock(Producto producto, int stockInicial, decimal precioCompra, DateTime? fechaVencimiento = null, string sucursalId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(producto.Nombre))
                    return "El nombre del producto no puede estar vacío.";

                _context.Productos.Add(producto);

                if (stockInicial > 0)
                {
                    var fechaActual = DateTime.Now;

                    var lote = new Lote
                    {
                        Id = Guid.NewGuid().ToString(),
                        IdProducto = producto.Id,
                        FechaIngreso = fechaActual,
                        FechaVencimiento = (producto.Vencimiento && fechaVencimiento.HasValue)
                            ? fechaVencimiento.Value
                            : DateTime.MaxValue,
                        Cantidad = stockInicial,
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

                    var movimiento = new MovimientoStock(producto.Id, stockInicial, fechaActual, idLote, precioCompra);
                    movimiento.Id = Guid.NewGuid().ToString();
                    _context.MovimientosStock.Add(movimiento);

                    producto.ActualizarPrecioCompraPromedio(precioCompra, stockInicial);
                    producto.StockActual += stockInicial;
                }

                _context.SaveChanges();
                return "Producto agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar producto: {ex.Message}";
            }
        }

        public string AgregarProducto(Producto producto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(producto.Nombre))
                    return "El nombre del producto no puede estar vacío.";

                _context.Productos.Add(producto);
                _context.SaveChanges();
                return "Producto agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar producto: {ex.Message}";
            }
        }

        public string EliminarProducto(string idProducto)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.Id == idProducto);
                if (producto == null)
                    return "El producto no existe.";

                // Baja lógica
                producto.Deshabilitar();
                _context.SaveChanges();
                return "Producto eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar producto: {ex.Message}";
            }
        }

        public string ModificarProducto(Producto productoEditado)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.Id == productoEditado.Id);
                if (producto == null)
                    return "El producto no existe.";

                producto.Nombre = productoEditado.Nombre;
                producto.Descripcion = productoEditado.Descripcion;
                producto.PrecioUnitario = productoEditado.PrecioUnitario;
                producto.Categoria = productoEditado.Categoria;
                producto.Vencimiento = productoEditado.Vencimiento;
                producto.FechaVencimiento = productoEditado.FechaVencimiento;

                _context.SaveChanges();
                return "Producto modificado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al modificar producto: {ex.Message}";
            }
        }

        public Dictionary<string, string> ObtenerMapaSucursalesPorProducto()
        {
            var lotes = _context.Lotes.Where(l => l.Cantidad > 0).ToList();
            var sucursales = _context.Sucursales.ToList();

            return lotes
                .GroupBy(l => l.IdProducto)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(", ", g
                        .Where(l => l.SucursalId != null)
                        .Select(l => l.SucursalId)
                        .Distinct()
                        .Select(sid => sucursales.FirstOrDefault(s => s.Id == sid)?.Nombre ?? "-")
                        .DefaultIfEmpty("Sin sucursal"))
                );
        }

        public List<Producto> ListarProductosPorSucursal(string sucursalId)
        {
            var productIds = _context.Lotes
                .Where(l => l.SucursalId == sucursalId && l.Cantidad > 0)
                .Select(l => l.IdProducto)
                .Distinct()
                .ToList();
            return _context.Productos.Where(p => productIds.Contains(p.Id)).ToList();
        }

        public string ObtenerSucursalIdDeProducto(string productoId)
        {
            var lote = _context.Lotes
                .Where(l => l.IdProducto == productoId && l.SucursalId != null)
                .FirstOrDefault();
            return lote?.SucursalId;
        }

        public string TransferirProductoASucursal(string productoId, string nuevaSucursalId)
        {
            try
            {
                var lotes = _context.Lotes
                    .Where(l => l.IdProducto == productoId)
                    .ToList();

                if (lotes.Count == 0)
                    return "El producto no tiene lotes activos para transferir.";

                foreach (var lote in lotes)
                {
                    lote.SucursalId = nuevaSucursalId;
                }

                _context.SaveChanges();
                return "Sucursal actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al transferir producto: {ex.Message}";
            }
        }
    }
}
