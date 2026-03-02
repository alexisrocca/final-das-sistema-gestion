using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class VendedorController
    {
        private TechStoreDbContext _context;

        public VendedorController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        public List<Vendedor> ListarVendedores()
        {
            try
            {
                return _context.Vendedores.Include("Sucursal").Where(v => v.Activo).ToList();
            }
            catch (Exception)
            {
                // In a production app context, we'd log the exception here
                return new List<Vendedor>();
            }
        }

        public Vendedor ObtenerVendedorPorId(string id)
        {
            return _context.Vendedores.Include("Sucursal").FirstOrDefault(v => v.Id == id);
        }

        public string AgregarVendedor(string nombre, string legajo, string idSucursal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(legajo)) 
                    return "El nombre y legajo son obligatorios.";

                var sucursal = _context.Sucursales.FirstOrDefault(s => s.Id == idSucursal);
                if (sucursal == null) return "La sucursal asignada no existe.";

                var vendedor = new Vendedor(nombre, sucursal)
                {
                    Legajo = legajo
                };
                
                _context.Vendedores.Add(vendedor);
                _context.SaveChanges();
                
                return "Vendedor agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar vendedor: {ex.Message}";
            }
        }

        public string ModificarVendedor(string id, string nombre, string legajo, string idSucursal)
        {
            try
            {
                var vendedor = _context.Vendedores.FirstOrDefault(v => v.Id == id);
                if (vendedor == null) return "El vendedor no existe.";

                var sucursal = _context.Sucursales.FirstOrDefault(s => s.Id == idSucursal);
                if (sucursal == null) return "La sucursal asignada no existe.";

                vendedor.Nombre = nombre;
                vendedor.Legajo = legajo;
                vendedor.Sucursal = sucursal;
                
                _context.SaveChanges();
                return "Vendedor modificado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al modificar vendedor: {ex.Message}";
            }
        }

        public string EliminarVendedor(string id)
        {
            try
            {
                var vendedor = _context.Vendedores.FirstOrDefault(v => v.Id == id);
                if (vendedor == null) return "El vendedor no existe.";

                vendedor.Deshabilitar(); // baja lógica
                _context.SaveChanges();
                
                return "Vendedor eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar vendedor: {ex.Message}";
            }
        }
    }
}