using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Sucursal : NamedEntity
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Sucursal() : base() { }

        public Sucursal(string nombre, string direccion, string telefono) : base(nombre)
        {
            this.Direccion = direccion;
            this.Telefono = telefono;
        }
    }
}
