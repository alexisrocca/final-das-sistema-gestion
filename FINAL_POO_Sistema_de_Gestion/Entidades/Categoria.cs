using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class Categoria : NamedEntity
    {
        private string _descripcion { get; set; }
        public string Descripcion
        {
            get { return this._descripcion; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La descripción dLa categoria no puede estar vacía.");
                this._descripcion = value;
            }
        }

        public Categoria() { }
        public Categoria(string nombre, string descripcion) : base(nombre)
        {
            this.Descripcion = descripcion;
            this.Activo = true;
        }
    }
}
