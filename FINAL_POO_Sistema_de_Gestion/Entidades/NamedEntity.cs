using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class NamedEntity : BaseEntity
    {
        public new string Id { get; set; }
        private string _nombre { get; set; }
        public string Nombre
        {
            get { return this._nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del elemento no puede estar vacío.");
                this._nombre = value;
            }
        }
        public NamedEntity() { }
        public NamedEntity(string nombre)
        {
            Id = $"{nombre.ToLower().ElementAt(0)}{new Random().Next(1, 1000)}";
            this.Nombre = nombre;
            Activo = true;
        }

    }
}
