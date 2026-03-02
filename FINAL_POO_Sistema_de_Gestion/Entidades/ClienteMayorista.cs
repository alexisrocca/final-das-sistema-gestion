using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class ClienteMayorista : Cliente
    {
        public ClienteMayorista() : base() { }

        public ClienteMayorista(string nombre, string cuit, string email) : base(nombre, cuit, email)
        {
        }

        public override decimal ObtenerLimiteDescuento() => 0.15m;
    }
}
