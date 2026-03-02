using System;
using System.Collections.Generic;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class ClienteController
    {
        private TechStoreDbContext _context;

        public ClienteController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        public List<Cliente> ListarClientes()
        {
            try
            {
                return _context.Clientes.Where(c => c.Activo).ToList();
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public Cliente ObtenerClientePorId(string id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public string AgregarClienteMinorista(string nombre, string dni, string email, string telefono, string direccion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(dni))
                    return "El nombre y DNI son obligatorios.";

                var cliente = new ClienteMinorista(nombre, dni, email)
                {
                    Telefono = telefono,
                    Direccion = direccion
                };

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return "Cliente minorista agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar cliente minorista: {ex.Message}";
            }
        }

        public string AgregarClienteMayorista(string nombre, string cuit, string email, string telefono, string direccion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cuit))
                    return "El nombre y CUIT son obligatorios.";

                var cliente = new ClienteMayorista(nombre, cuit, email)
                {
                    Telefono = telefono,
                    Direccion = direccion
                };

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return "Cliente mayorista agregado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al agregar cliente mayorista: {ex.Message}";
            }
        }
        
        public string ModificarCliente(string id, string nombre, string dniCuit, string email, string telefono, string direccion)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null)
                    return "El cliente no existe.";
                    
                cliente.Nombre = nombre;
                cliente.DniCuit = dniCuit;
                cliente.Email = email;
                cliente.Telefono = telefono;
                cliente.Direccion = direccion;
                
                _context.SaveChanges();
                return "Cliente modificado exitosamente.";
            }
            catch(Exception ex)
            {
                return $"Error al modificar cliente: {ex.Message}";
            }
        }

        public string EliminarCliente(string idCliente)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == idCliente);
                if (cliente == null)
                    return "El cliente no existe.";

                cliente.Deshabilitar(); // asumiendo que tiene la propiedad Activo
                _context.SaveChanges();
                return "Cliente eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar cliente: {ex.Message}";
            }
        }
    }
}