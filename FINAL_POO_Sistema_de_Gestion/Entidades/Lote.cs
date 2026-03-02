using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Lote : BaseEntity
    {
        private string _idProducto;
        private DateTime _fechaVencimiento;
        private int _cantidad;

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
        public string IdProveedor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set
            {
                if (value < FechaIngreso)
                    throw new ArgumentException("La fecha de vencimiento no puede ser anterior a la fecha de ingreso.");
                if (value == null) throw new ArgumentNullException(nameof(value), "La fecha de vencimiento no puede ser nula.");
                
                _fechaVencimiento = value;
            }
        }
        public int Cantidad
        {
            get { return _cantidad; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("La cantidad no puede ser negativa.");
                _cantidad = value;
            }
        }
        public decimal PrecioCompra { get; set; }

        public string SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        public Lote() { }
    }
}
