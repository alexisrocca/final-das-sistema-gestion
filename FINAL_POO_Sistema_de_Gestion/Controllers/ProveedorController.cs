using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class ProveedorController
    {
        private readonly TechStoreDbContext _context;

        public ProveedorController(TechStoreDbContext context)
        {
            _context = context;
        }

        public List<Proveedor> ListarProveedores()
        {
            return _context.Proveedores.ToList();
        }

        public List<Proveedor> ListarProveedoresActivos()
        {
            return _context.Proveedores.Where(p => p.Activo).ToList();
        }

        public Proveedor ObtenerProveedorPorId(string id)
        {
            return _context.Proveedores.FirstOrDefault(p => p.Id == id);
        }

        public string AgregarProveedor(Proveedor proveedor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(proveedor.Nombre))
                    return "El nombre del proveedor no puede estar vacío.";

                _context.Proveedores.Add(proveedor);
                _context.SaveChanges();
                return "Proveedor agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar proveedor: {ex.Message}";
            }
        }

        public string ModificarProveedor(Proveedor proveedorEditado)
        {
            try
            {
                var proveedor = _context.Proveedores.FirstOrDefault(p => p.Id == proveedorEditado.Id);
                if (proveedor == null) return "El proveedor no existe.";

                proveedor.Nombre = proveedorEditado.Nombre;
                proveedor.Direccion = proveedorEditado.Direccion;
                proveedor.Email = proveedorEditado.Email;
                proveedor.Tel = proveedorEditado.Tel;
                _context.SaveChanges();
                return "Proveedor modificado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al modificar proveedor: {ex.Message}";
            }
        }

        public string EliminarProveedor(string idProveedor)
        {
            try
            {
                var proveedor = _context.Proveedores.FirstOrDefault(p => p.Id == idProveedor);
                if (proveedor == null) return "El proveedor no existe.";

                proveedor.Deshabilitar();
                _context.SaveChanges();
                return "Proveedor eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar proveedor: {ex.Message}";
            }
        }
    }
}
