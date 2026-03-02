using FINAL_POO_Sistema_de_Gestion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Repos
{
    public class RepositorioNamedEntity<T> : RepositorioBaseEntity<T> where T : NamedEntity
    {
        public RepositorioNamedEntity(RepositorioMaestro repoMain) : base(repoMain) { }
        
        public new virtual T Obtener(string idORnombre)
        {
            string busqueda = idORnombre.ToLower().Trim();

            T resultado = datos.FirstOrDefault(d => d.Id.ToLower() == busqueda);

            if (resultado == null)
                resultado = datos.FirstOrDefault(d => d.Nombre.ToLower() == busqueda);

            if (resultado == null)
            {
                resultado = datos.FirstOrDefault(d =>
                    d.Id.ToLower().Contains(busqueda) ||
                    d.Nombre.ToLower().Contains(busqueda));
            }

            return resultado ?? throw new KeyNotFoundException($"No se encontró el elemento: {idORnombre}");
        }
    }
}
