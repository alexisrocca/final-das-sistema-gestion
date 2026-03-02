using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public abstract class Cliente : NamedEntity
    {
        public string Email { get; set; }
        public string DniCuit { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public Cliente() : base() { }

        public Cliente(string nombre, string dniCuit, string email) : base(nombre)
        {
            this.DniCuit = dniCuit;
            this.Email = email;
        }

        public abstract decimal ObtenerLimiteDescuento();
    }
}
