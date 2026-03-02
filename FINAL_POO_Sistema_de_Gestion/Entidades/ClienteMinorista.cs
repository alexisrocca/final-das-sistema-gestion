using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class ClienteMinorista : Cliente
    {
        public ClienteMinorista() : base() { }

        public ClienteMinorista(string nombre, string dni, string email) : base(nombre, dni, email)
        {
        }

        public override decimal ObtenerLimiteDescuento() => 0.05m;
    }
}
