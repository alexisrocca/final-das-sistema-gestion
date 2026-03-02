using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Proveedor : NamedEntity
    {
        private string _tel {  get; set; }
        private string _email { get; set; }
        private string _direccion { get; set; }
        public string Tel
        {
            get { return _tel; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentException("El teléfono del proveedor no puede estar vacío.");
                if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
                    throw new ArgumentException("El teléfono del proveedor debe contener solo números.");
                this._tel = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El email del proveedor no puede estar vacío.");
                if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("El email del proveedor no es válido.");
                this._email = value;
            }
        }
        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La dirección del proveedor no puede estar vacía.");
                this._direccion = value;
            }
        }
        public Proveedor() { }
        public Proveedor(string nombre, string tel, string email, string direccion) : base(nombre)
        {
            this.Tel = tel;
            this.Email = email;
            this.Direccion = direccion;
        }

    }
}
