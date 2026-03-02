using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class DetalleVenta : BaseEntity
    {
        public string ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;

        public DetalleVenta() : base() { }

        public DetalleVenta(Producto producto, int cantidad, decimal precioUnitario) : base()
        {
            this.Producto = producto;
            this.ProductoId = producto?.Id;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
        }
    }
}
