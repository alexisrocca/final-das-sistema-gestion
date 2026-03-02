using System;
using System.Collections.Generic;
using System.Linq;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Venta : BaseEntity
    {
        public string ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string VendedorId { get; set; }
        public virtual Vendedor Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal DescuentoAplicado { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public virtual List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

        public decimal MontoTotal 
        { 
            get { return Detalles.Sum(d => d.Subtotal) * (1 - DescuentoAplicado); } 
        }

        public Venta() : base()
        {
            Fecha = DateTime.Now;
        }

        public void AgregarDetalle(DetalleVenta detalle)
        {
            Detalles.Add(detalle);
        }
    }
}
