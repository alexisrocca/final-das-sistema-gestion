using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class CategoriaController
    {
        private readonly TechStoreDbContext _context;

        public CategoriaController(TechStoreDbContext context)
        {
            _context = context;
        }

        public List<Categoria> ListarCategorias()
        {
            return _context.Categorias.ToList();
        }

        public List<Categoria> ListarCategoriasActivas()
        {
            return _context.Categorias.Where(r => r.Activo).ToList();
        }

        public Categoria ObtenerCategoriaPorId(string id)
        {
            return _context.Categorias.FirstOrDefault(r => r.Id == id);
        }

        public bool TieneCategoriaProductosAsociados(string nombreCategoria)
        {
            return _context.Productos.Where(p => p.Activo).ToList().Any(p => p.Categoria == nombreCategoria);
        }

        public string AgregarCategoria(Categoria categoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                    return "El nombre dLa categoria no puede estar vacío.";

                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return "Categoria agregada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar categoria: {ex.Message}";
            }
        }

        public string ModificarCategoria(Categoria categoriaEditada)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(r => r.Id == categoriaEditada.Id);
                if (categoria == null) return "La categoria no existe.";

                categoria.Nombre = categoriaEditada.Nombre;
                _context.SaveChanges();
                return "Categoria modificada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al modificar categoria: {ex.Message}";
            }
        }

        public string EliminarCategoria(string idCategoria)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(r => r.Id == idCategoria);
                if (categoria == null) return "La categoria no existe.";

                categoria.Deshabilitar();
                _context.SaveChanges();
                return "Categoria eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar categoria: {ex.Message}";
            }
        }
    }
}
