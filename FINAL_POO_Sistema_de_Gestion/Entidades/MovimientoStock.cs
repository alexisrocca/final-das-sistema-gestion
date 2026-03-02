using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class MovimientoStock : BaseEntity
    {
        private string _idProducto;
        private int _cantidad;
        private string _idProveedor;
        private TipoMovimiento _tipoMovimiento;
        private TipoEgreso? _tipoEgreso;
        private string _idLote; // Vinculación con Lote para productos con vencimiento
        private decimal? _precioCompra; // Precio de compra para ingresos

        public string IdProducto
        {
            get { return _idProducto; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El ID del producto no puede estar vacío.");
                _idProducto = value;
            }
        }

        public int Cantidad
        {
            get { return _cantidad; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La cantidad debe ser mayor a cero.");
                _cantidad = value;
            }
        }

        public DateTime Fecha { get; set; }

        public string IdProveedor
        {
            get { return _idProveedor; }
            set
            {
                // Solo requerido para ingresos excepto si es ingreso sin proveedor
                if (TipoMovimiento == TipoMovimiento.Ingreso && string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El ID del proveedor es requerido para ingresos de stock.");
                _idProveedor = value;
            }
        }

        public TipoMovimiento TipoMovimiento
        {
            get { return _tipoMovimiento; }
            set { _tipoMovimiento = value; }
        }

        public TipoEgreso? TipoEgreso
        {
            get { return _tipoEgreso; }
            set
            {
                // Solo aplicable para egresos
                if (TipoMovimiento == TipoMovimiento.Egreso && value == null)
                    throw new ArgumentException("El tipo de egreso es requerido para salidas de stock.");
                if (TipoMovimiento == TipoMovimiento.Ingreso && value != null)
                    throw new ArgumentException("El tipo de egreso no es aplicable para ingresos de stock.");
                _tipoEgreso = value;
            }
        }

        public string IdLote
        {
            get { return _idLote; }
            set { _idLote = value; }
        }

        public decimal? PrecioCompra
        {
            get { return _precioCompra; }
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentException("El precio de compra no puede ser negativo.");
                _precioCompra = value;
            }
        }

        public MovimientoStock() { }

        // Constructor para Ingreso de Stock (sin lote)
        public MovimientoStock(string idProducto, int cantidad, string idProveedor, DateTime fecha)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            IdProveedor = idProveedor;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Ingreso;
            TipoEgreso = null;
        }

        // Constructor para Ingreso de Stock con lote y precio de compra
        public MovimientoStock(string idProducto, int cantidad, string idProveedor, DateTime fecha, string idLote, decimal precioCompra)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            IdProveedor = idProveedor;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Ingreso;
            TipoEgreso = null;
            IdLote = idLote;
            PrecioCompra = precioCompra;
        }
        // Constructor para Ingreso de Stock (sin lote), sin proveedor
        public MovimientoStock(string idProducto, int cantidad, DateTime fecha)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Ingreso;
            TipoEgreso = null;
        }

        // Constructor para Ingreso de Stock con lote y precio de compra, sin proveedor
        public MovimientoStock(string idProducto, int cantidad, DateTime fecha, string idLote, decimal precioCompra)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Ingreso;
            TipoEgreso = null;
            IdLote = idLote;
            PrecioCompra = precioCompra;
        }

        // Constructor para Egreso de Stock
        public MovimientoStock(string idProducto, int cantidad, DateTime fecha, TipoEgreso tipoEgreso)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Egreso;
            TipoEgreso = tipoEgreso;
            IdProveedor = null;
        }

        // Constructor para Egreso de Stock con lote específico
        public MovimientoStock(string idProducto, int cantidad, DateTime fecha, TipoEgreso tipoEgreso, string idLote)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
            Fecha = fecha;
            TipoMovimiento = TipoMovimiento.Egreso;
            TipoEgreso = tipoEgreso;
            IdLote = idLote;
            IdProveedor = null;
        }
    }
}