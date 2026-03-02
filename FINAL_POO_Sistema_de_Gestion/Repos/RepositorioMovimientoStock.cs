using FINAL_POO_Sistema_de_Gestion.Entidades;
using FINAL_POO_Sistema_de_Gestion.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Repos
{
    public class RepositorioMovimientoStock : RepositorioBaseEntity<MovimientoStock>
    {
        private Welcome frmInicio;
        public RepositorioMovimientoStock(RepositorioMaestro repoMain, Welcome frmInicio) : base(repoMain)
        {
            this.frmInicio = frmInicio;
        }

        // Método para registrar ingreso de stock con lote y precio de compra
        public void RegistrarIngresoStock(string idProducto, int cantidad, string idProveedor, DateTime fecha, decimal precioCompra, DateTime? fechaVencimiento = null)
        {
            try 
            {
                var producto = repoMain.Inventario.Obtener(idProducto);
                if (producto == null)
                    throw new ArgumentException("El producto no existe.");

                var proveedor = repoMain.Proveedores.Obtener(idProveedor);
                if (proveedor == null)
                    throw new ArgumentException("El proveedor no existe.");

                // Crear lote si el producto tiene vencimiento
                string idLote = null;
                if (producto.Vencimiento && fechaVencimiento.HasValue)
                {
                    var lote = new Lote
                    {
                        IdProducto = idProducto,
                        IdProveedor = idProveedor,
                        FechaIngreso = fecha,
                        FechaVencimiento = fechaVencimiento.Value,
                        Cantidad = cantidad,
                        PrecioCompra = precioCompra
                    };
                    repoMain.Stock.Agregar(lote);
                    idLote = lote.Id;

                    if (!producto.FechaVencimiento.HasValue || fechaVencimiento < producto.FechaVencimiento)
                    {
                        producto.FechaVencimiento = fechaVencimiento;
                    }
                }

                var movimiento = new MovimientoStock(idProducto, cantidad, idProveedor, fecha, idLote, precioCompra);
                Agregar(movimiento);
                
                producto.ActualizarPrecioCompraPromedio(precioCompra, cantidad);
                producto.StockActual += cantidad;
                repoMain.Inventario.Actualizar(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar ingreso de stock: {ex.Message}");
            }
        }

        public void RegistrarIngresoStock(string idProducto, int cantidad, string idProveedor, DateTime fecha, decimal precioCompra)
        {
            RegistrarIngresoStock(idProducto, cantidad, idProveedor, fecha, precioCompra, null);
        }
        public void RegistrarIngresoStock(string idProducto, int cantidad, DateTime fecha, decimal precioCompra, DateTime? fechaVencimiento = null)
        {
            try
            {
                var producto = repoMain.Inventario.Obtener(idProducto);
                if (producto == null)
                    throw new ArgumentException("El producto no existe.");

                // Crear lote si el producto tiene vencimiento
                string idLote = null;
                if (producto.Vencimiento && fechaVencimiento.HasValue)
                {
                    var lote = new Lote
                    {
                        IdProducto = idProducto,
                        FechaIngreso = fecha,
                        FechaVencimiento = fechaVencimiento.Value,
                        Cantidad = cantidad,
                        PrecioCompra = precioCompra
                    };
                    repoMain.Stock.Agregar(lote);
                    idLote = lote.Id;

                    if (!producto.FechaVencimiento.HasValue || fechaVencimiento < producto.FechaVencimiento)
                    {
                        producto.FechaVencimiento = fechaVencimiento;
                    }
                }

                var movimiento = new MovimientoStock(idProducto, cantidad, fecha, idLote, precioCompra);
                Agregar(movimiento);

                producto.ActualizarPrecioCompraPromedio(precioCompra, cantidad);
                producto.StockActual += cantidad;
                repoMain.Inventario.Actualizar(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar ingreso de stock: {ex.Message}");
            }
        }

        public void RegistrarIngresoStock(string idProducto, int cantidad, DateTime fecha, decimal precioCompra)
        {
            RegistrarIngresoStock(idProducto, cantidad, fecha, precioCompra, null);
        }
        public void RegistrarEgresoStock(string idProducto, int cantidad, DateTime fecha, TipoEgreso tipoEgreso)
        {
            try 
            {
                var producto = repoMain.Inventario.Obtener(idProducto) ?? throw new ArgumentException("El producto no existe.");
                    
                if (producto.StockActual < cantidad)
                    throw new ArgumentException($"Stock insuficiente. Stock actual: {producto.StockActual}, cantidad solicitada: {cantidad}");

                int cantidadRestante = cantidad;

                // Si el producto tiene vencimiento, se usa FIFO por fecha de vencimiento
                if (producto.Vencimiento)
                {
                    var lotes = repoMain.Stock.ListarActivos()
                        .Where(l => l.IdProducto == idProducto && l.Cantidad > 0)
                        .OrderBy(l => l.FechaVencimiento)
                        .ToList();

                    foreach (var lote in lotes)
                    {
                        if (cantidadRestante <= 0) break;

                        int cantidadAUsar = Math.Min(cantidadRestante, lote.Cantidad);
                        
                        // Crear movimiento por lote
                        var movimiento = new MovimientoStock(idProducto, cantidadAUsar, fecha, tipoEgreso, lote.Id);
                        Agregar(movimiento);

                        // Actualizar cantidad del lote
                        lote.Cantidad -= cantidadAUsar;
                        repoMain.Stock.Actualizar(lote);

                        cantidadRestante -= cantidadAUsar;
                    }
                }
                else
                {
                    // sin vencimiento
                    var movimiento = new MovimientoStock(idProducto, cantidad, fecha, tipoEgreso);
                    Agregar(movimiento);
                }

                // Actualizar stock del producto
                producto.StockActual -= cantidad;
                
                // Actualizar fecha de vencimiento más próxima si es necesario
                if (producto.Vencimiento)
                {
                    ActualizarFechaVencimientoProducto(producto);
                }

                repoMain.Inventario.Actualizar(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar egreso de stock: {ex.Message}");
            }
        }
        // Método para obtener productos próximos a vencer (1 semana)
        public List<Producto> ObtenerProductosProximosAVencer()
        {
            var fechaLimite = repoMain.FechaActual.AddDays(7);
            return repoMain.Inventario.ListarActivos()
                .Where(p => p.Vencimiento && p.FechaVencimiento.HasValue && 
                           p.FechaVencimiento.Value <= fechaLimite && 
                           p.FechaVencimiento.Value >= repoMain.FechaActual && 
                           p.StockActual > 0)
                .OrderBy(p => p.FechaVencimiento)
                .ToList();
        }
        public List<Producto> ObtenerProductosVencidos()
        {
            return repoMain.Inventario.ListarActivos()
                .Where(p => p.Vencimiento && p.FechaVencimiento.HasValue && 
                           p.FechaVencimiento.Value < repoMain.FechaActual && 
                           p.StockActual > 0)
                .OrderBy(p => p.FechaVencimiento)
                .ToList();
        }
        public void ProcesarProductosVencidos()
        {
            var productosVencidos = repoMain.Inventario.ListarActivos()
                .Where(p => p.Vencimiento && p.FechaVencimiento.HasValue && 
                           p.FechaVencimiento.Value < repoMain.FechaActual && 
                           p.StockActual > 0)
                .ToList();
            
            foreach (var producto in productosVencidos)
            {
                var lotesVencidos = repoMain.Stock.ListarActivos()
                    .Where(l => l.IdProducto == producto.Id && 
                               l.FechaVencimiento < repoMain.FechaActual && 
                               l.Cantidad > 0)
                    .ToList();

                foreach (var lote in lotesVencidos)
                {
                    // Registrar egreso por vencimiento
                    var movimiento = new MovimientoStock(producto.Id, lote.Cantidad, repoMain.FechaActual, 
                        TipoEgreso.Merma, lote.Id);
                    Agregar(movimiento);

                    // Actualizar stock del producto
                    producto.StockActual -= lote.Cantidad;

                    // Marcar lote como agotado
                    lote.Cantidad = 0;
                    repoMain.Stock.Actualizar(lote);
                }

                // Actualizar fecha de vencimiento del producto
                ActualizarFechaVencimientoProducto(producto);
                repoMain.Inventario.Actualizar(producto);
                if (this.frmInicio.lbNotificaciones.InvokeRequired)
                {
                    this.frmInicio.lbNotificaciones.Invoke(new Action(() =>
                        this.frmInicio.lbNotificaciones.Items.Add($"El producto '{producto.Nombre}' ha sido marcado como vencido y se han registrado las mermas correspondientes.")
                    ));
                }
                else
                {
                    this.frmInicio.lbNotificaciones.Items.Add($"El producto '{producto.Nombre}' ha sido marcado como vencido y se han registrado las mermas correspondientes.");
                }
            }
            repoMain.GuardarDatos();
        }
        // Método privado para actualizar la fecha de vencimiento más próxima de un producto
        private void ActualizarFechaVencimientoProducto(Producto producto)
        {
            var proximoLote = repoMain.Stock.ListarActivos()
                .Where(l => l.IdProducto == producto.Id && l.Cantidad > 0)
                .OrderBy(l => l.FechaVencimiento)
                .FirstOrDefault();
            if (proximoLote == null)
            {
                producto.FechaVencimiento = null; // No hay lotes activos
            }
            else
            {
                producto.FechaVencimiento = proximoLote?.FechaVencimiento;

            }
        }
        public List<MovimientoStock> ObtenerMovimientosPorProducto(string idProducto)
        {
            return ListarActivos().Where(m => m.IdProducto == idProducto).OrderByDescending(m => m.Fecha).ToList();
        }
        public List<MovimientoStock> ObtenerMovimientosPorProveedor(string idProveedor)
        {
            return ListarActivos().Where(m => m.IdProveedor == idProveedor && m.TipoMovimiento == TipoMovimiento.Ingreso)
                                  .OrderByDescending(m => m.Fecha).ToList();
        }
        public List<MovimientoStock> ObtenerMovimientosPorTipo(TipoMovimiento tipoMovimiento)
        {
            return ListarActivos().Where(m => m.TipoMovimiento == tipoMovimiento).OrderByDescending(m => m.Fecha).ToList();
        }
        public List<MovimientoStock> ObtenerMovimientosPorTipoEgreso(TipoEgreso tipoEgreso)
        {
            return ListarActivos().Where(m => m.TipoMovimiento == TipoMovimiento.Egreso && m.TipoEgreso == tipoEgreso)
                                  .OrderByDescending(m => m.Fecha).ToList();
        }
        public List<MovimientoStock> ObtenerMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return ListarActivos().Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                                  .OrderByDescending(m => m.Fecha).ToList();
        }
        public Dictionary<string, (int TotalIngresos, int TotalEgresos, int StockCalculado)> ObtenerEstadisticasPorProducto()
        {
            var estadisticas = new Dictionary<string, (int TotalIngresos, int TotalEgresos, int StockCalculado)>();
            
            foreach (var grupo in ListarActivos().GroupBy(m => m.IdProducto))
            {
                var ingresos = grupo.Where(m => m.TipoMovimiento == TipoMovimiento.Ingreso).Sum(m => m.Cantidad);
                var egresos = grupo.Where(m => m.TipoMovimiento == TipoMovimiento.Egreso).Sum(m => m.Cantidad);
                estadisticas[grupo.Key] = (ingresos, egresos, ingresos - egresos);
            }
            
            return estadisticas;
        }
    }
}