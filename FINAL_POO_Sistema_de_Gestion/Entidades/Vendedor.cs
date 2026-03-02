using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Vendedor : NamedEntity
    {
        public string SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public string Legajo { get; set; }

        public Vendedor() : base() { }

        public Vendedor(string nombre, Sucursal sucursal) : base(nombre)        
        {
            this.Sucursal = sucursal;
            this.SucursalId = sucursal?.Id;
        }
    }
}
