using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Producto : NamedEntity
    {
        private string _descripcion { get; set; }
        private decimal _precioUnitario { get; set; }
        private int _stock { get; set; }
        private string _categoria { get; set; }
        private decimal _precioCompraPromedio { get; set; }
        private DateTime? _fechaVencimiento { get; set; }

        public int StockActual
        {
            get { return this._stock; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El stock actual no puede ser negativo.");
                this._stock = value;
            }
        }

        public string Descripcion
        {
            get { return this._descripcion; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La descripción no puede estar vacía.");
                this._descripcion = value;
            }
        }

        public decimal PrecioUnitario
        {
            get { return this._precioUnitario; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El precio unitario no puede ser negativo.");
                this._precioUnitario = value;
            }
        }

        public string Categoria
        {
            get { return this._categoria; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La categoría del producto no puede estar vacía.");
                this._categoria = value;
            }
        }

        public bool Vencimiento { get; set; }
        public decimal PrecioCompraPromedio
        {
            get { return _precioCompraPromedio; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El precio de compra promedio no puede ser negativo.");
                _precioCompraPromedio = value;
                // Actualizar precio de venta automáticamente (50% de ganancia)
                if (value > 0)
                    PrecioUnitario = value * 1.5m;
            }
        }
        public DateTime? FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        public Producto(string nombre, decimal precioUnitario, string categoria) : base(nombre)
        {
            this.Descripcion = $"Producto {nombre}";
            this.PrecioUnitario = precioUnitario;
            this.StockActual = 0;
            this.Categoria = categoria;
            this.Vencimiento = false;
            this.PrecioCompraPromedio = 0;
        }

        public Producto() { }

        public Producto(string nombre, string descripcion, decimal precioUnitario, string categoria, bool vencimiento) : base(nombre)
        {
            this.Descripcion = descripcion;
            this.PrecioUnitario = precioUnitario;
            this.Categoria = categoria;
            this.Vencimiento = vencimiento;
            this.PrecioCompraPromedio = 0;
        }

        // Constructor con precio de compra
        public Producto(string nombre, string descripcion, decimal precioCompra, string categoria, bool vencimiento, DateTime? fechaVencimiento) : base(nombre)
        {
            this.Descripcion = descripcion;
            this.Categoria = categoria;
            this.Vencimiento = vencimiento;
            this.FechaVencimiento = fechaVencimiento;
            this.PrecioCompraPromedio = precioCompra; // Esto automáticamente calcula el precio de venta
        }

        // Método para actualizar precio de compra promedio basado en movimientos
        public void ActualizarPrecioCompraPromedio(decimal nuevoPrecioCompra, int cantidadComprada)
        {
            if (PrecioCompraPromedio == 0)
            {
                PrecioCompraPromedio = nuevoPrecioCompra;
            }
            else
            {
                // Calcular precio promedio ponderado
                decimal stockAnterior = StockActual - cantidadComprada;
                if (stockAnterior < 0) stockAnterior = 0;
                
                decimal valorAnterior = PrecioCompraPromedio * stockAnterior;
                decimal valorNuevo = nuevoPrecioCompra * cantidadComprada;
                decimal totalStock = stockAnterior + cantidadComprada;
                
                if (totalStock > 0)
                {
                    PrecioCompraPromedio = (valorAnterior + valorNuevo) / totalStock;
                }
            }
        }
    }
}
