using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Controllers
{
    public class CuentaCorrienteController
    {
        private TechStoreDbContext _context;

        public CuentaCorrienteController(TechStoreDbContext context = null)
        {
            _context = context ?? new TechStoreDbContext();
        }

        /// <summary>
        /// Obtiene todos los movimientos de cuenta corriente de un cliente.
        /// </summary>
        public List<MovimientoCuentaCorriente> ObtenerMovimientos(string clienteId)
        {
            return _context.MovimientosCuentaCorriente
                .Include(m => m.Cliente)
                .Include(m => m.Venta)
                .Where(m => m.ClienteId == clienteId)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        /// <summary>
        /// Obtiene movimientos filtrados por rango de fechas.
        /// </summary>
        public List<MovimientoCuentaCorriente> ObtenerMovimientosPorFecha(string clienteId, DateTime desde, DateTime hasta)
        {
            return _context.MovimientosCuentaCorriente
                .Include(m => m.Cliente)
                .Include(m => m.Venta)
                .Where(m => m.ClienteId == clienteId && m.Fecha >= desde && m.Fecha < hasta)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        /// <summary>
        /// Calcula el saldo actual del cliente (positivo = el cliente debe, negativo = saldo a favor).
        /// </summary>
        public decimal ObtenerSaldo(string clienteId)
        {
            var movimientos = _context.MovimientosCuentaCorriente
                .Where(m => m.ClienteId == clienteId)
                .ToList();

            decimal debitos = movimientos.Where(m => m.Tipo == TipoMovimientoCuenta.Debito).Sum(m => m.Monto);
            decimal creditos = movimientos.Where(m => m.Tipo == TipoMovimientoCuenta.Credito).Sum(m => m.Monto);

            return debitos - creditos;
        }

        /// <summary>
        /// Registra un débito en la cuenta corriente (compra a crédito).
        /// </summary>
        public string RegistrarDebito(string clienteId, decimal monto, string descripcion, string ventaId = null)
        {
            try
            {
                if (monto <= 0) return "El monto debe ser mayor a cero.";

                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
                if (cliente == null) return "El cliente no existe.";

                var movimiento = new MovimientoCuentaCorriente(clienteId, TipoMovimientoCuenta.Debito, monto, descripcion, ventaId);
                _context.MovimientosCuentaCorriente.Add(movimiento);
                _context.SaveChanges();

                return "Débito registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar débito: {ex.Message}";
            }
        }

        /// <summary>
        /// Registra un pago (crédito) del cliente.
        /// </summary>
        public string RegistrarPago(string clienteId, decimal monto, string descripcion = null)
        {
            try
            {
                if (monto <= 0) return "El monto debe ser mayor a cero.";

                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
                if (cliente == null) return "El cliente no existe.";

                string desc = descripcion ?? $"Pago recibido - {DateTime.Now:dd/MM/yyyy}";

                var movimiento = new MovimientoCuentaCorriente(clienteId, TipoMovimientoCuenta.Credito, monto, desc);
                _context.MovimientosCuentaCorriente.Add(movimiento);
                _context.SaveChanges();

                return "Pago registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar pago: {ex.Message}";
            }
        }

        /// <summary>
        /// Obtiene un resumen de cuentas corrientes de todos los clientes con saldo distinto de cero.
        /// </summary>
        public List<ResumenCuentaCorriente> ObtenerResumenGeneral()
        {
            var clientes = _context.Clientes.Where(c => c.Activo).ToList();
            var resumen = new List<ResumenCuentaCorriente>();

            foreach (var cliente in clientes)
            {
                var movimientos = _context.MovimientosCuentaCorriente
                    .Where(m => m.ClienteId == cliente.Id)
                    .ToList();

                decimal debitos = movimientos.Where(m => m.Tipo == TipoMovimientoCuenta.Debito).Sum(m => m.Monto);
                decimal creditos = movimientos.Where(m => m.Tipo == TipoMovimientoCuenta.Credito).Sum(m => m.Monto);
                decimal saldo = debitos - creditos;

                resumen.Add(new ResumenCuentaCorriente
                {
                    ClienteId = cliente.Id,
                    ClienteNombre = cliente.Nombre,
                    TipoCliente = cliente is ClienteMayorista ? "Mayorista" : "Minorista",
                    TotalDebitos = debitos,
                    TotalCreditos = creditos,
                    Saldo = saldo
                });
            }

            return resumen;
        }
    }

    /// <summary>
    /// Clase auxiliar para el resumen de cuentas corrientes.
    /// </summary>
    public class ResumenCuentaCorriente
    {
        public string ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public string TipoCliente { get; set; }
        public decimal TotalDebitos { get; set; }
        public decimal TotalCreditos { get; set; }
        public decimal Saldo { get; set; }
    }
}
