using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class SucursalController
    {
        private TechStoreDbContext _context;

        public SucursalController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        public List<Sucursal> ListarSucursales()
        {
            try
            {
                return _context.Sucursales.Where(s => s.Activo).ToList();
            }
            catch (Exception)
            {
                return new List<Sucursal>();
            }
        }

        public Sucursal ObtenerSucursalPorId(string id)
        {
            return _context.Sucursales.FirstOrDefault(s => s.Id == id);
        }

        public string AgregarSucursal(string nombre, string direccion, string telefono)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return "El nombre es obligatorio.";
                
                var sucursal = new Sucursal(nombre, direccion, telefono);
                _context.Sucursales.Add(sucursal);
                _context.SaveChanges();
                
                return "Sucursal agregada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar sucursal: {ex.Message}";
            }
        }

        public string ModificarSucursal(string id, string nombre, string direccion, string telefono)
        {
            try
            {
                var sucursal = _context.Sucursales.FirstOrDefault(s => s.Id == id);
                if (sucursal == null) return "La sucursal no existe.";

                sucursal.Nombre = nombre;
                sucursal.Direccion = direccion;
                sucursal.Telefono = telefono;
                
                _context.SaveChanges();
                return "Sucursal modificada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al modificar sucursal: {ex.Message}";
            }
        }

        public string EliminarSucursal(string id)
        {
            try
            {
                var sucursal = _context.Sucursales.FirstOrDefault(s => s.Id == id);
                if (sucursal == null) return "La sucursal no existe.";

                sucursal.Deshabilitar(); // baja lógica
                _context.SaveChanges();
                
                return "Sucursal eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar sucursal: {ex.Message}";
            }
        }
    }
}