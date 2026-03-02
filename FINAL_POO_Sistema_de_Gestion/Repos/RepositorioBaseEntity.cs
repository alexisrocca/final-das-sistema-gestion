using FINAL_POO_Sistema_de_Gestion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Repos
{
    public class RepositorioBaseEntity<T> where T : BaseEntity
    {
        protected List<T> datos;
        protected readonly RepositorioMaestro repoMain;
        public RepositorioBaseEntity(RepositorioMaestro repoMain)
        {
            this.repoMain = repoMain ?? throw new ArgumentNullException(nameof(repoMain), "El repositorio maestro no puede ser nulo.");
            datos = new List<T>();
        }
        public virtual void Agregar(T e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e), "El elemento no puede ser nulo.");
            if (datos.Any(p => p.Id == e.Id))
                throw new InvalidOperationException("El elemento ya existe.");
            datos.Add(e);
            repoMain.GuardarDatos();
        }
        public virtual void Eliminar(string id)
        {
            T elem = datos.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException("Elemento a eliminar no encontrado.");
                
            datos.Remove(elem);
            repoMain.GuardarDatos();
        }
        public virtual T Obtener(string id)
        {
            return datos.FirstOrDefault(d => d.Id.ToLower() == id.ToLower().Trim()) ?? throw new KeyNotFoundException("No se encontró el elemento");
        }
        public virtual void Actualizar(T e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e), "El elemento no puede ser nulo.");

            int index = datos.IndexOf(e);

            if (index == -1)
                throw new InvalidOperationException("Error interno: no se pudo encontrar el índice del elemento obtenido.");

            datos[index] = e;
            repoMain.GuardarDatos();
        }
        public virtual List<T> ListarActivos()
        {
            return new List<T>(datos.Where(d => d.Activo == true));
        }
        public virtual List<T> Listar()
        {
            return new List<T>(datos);
        }
        public virtual void Importar(List<T> lista)
        {
            datos = lista ?? new List<T>();
        }
    }
}
