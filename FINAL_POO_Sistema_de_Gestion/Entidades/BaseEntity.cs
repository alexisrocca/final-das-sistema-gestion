using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public bool Activo { get; set; }

        public BaseEntity()
        {
            Id = $"{new Random().Next(1, 1000)}";
            Activo = true;
        }
        public void Habilitar()
        {
            this.Activo = true;
        }
        public void Deshabilitar()
        {
            this.Activo = false;
        }
    }
}
