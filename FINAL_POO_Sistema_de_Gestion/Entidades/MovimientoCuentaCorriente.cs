using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class MovimientoCuentaCorriente : BaseEntity
    {
        public string ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public DateTime Fecha { get; set; }
        public TipoMovimientoCuenta Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }

        // Referencia opcional a la venta que generó el débito
        public string VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        public MovimientoCuentaCorriente() : base()
        {
            Fecha = DateTime.Now;
        }

        public MovimientoCuentaCorriente(string clienteId, TipoMovimientoCuenta tipo, decimal monto, string descripcion, string ventaId = null) : base()
        {
            ClienteId = clienteId;
            Fecha = DateTime.Now;
            Tipo = tipo;
            Monto = monto;
            Descripcion = descripcion;
            VentaId = ventaId;
        }
    }
}
