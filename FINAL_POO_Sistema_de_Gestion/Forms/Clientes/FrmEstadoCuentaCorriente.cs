using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Forms.Clientes
{
    public partial class FrmEstadoCuentaCorriente : Form
    {
        private TechStoreDbContext _context;
        private CuentaCorrienteController _ccController;
        private ClienteController _clienteController;

        public FrmEstadoCuentaCorriente(TechStoreDbContext context)
        {
            InitializeComponent();
            _context = context;
            _ccController = new CuentaCorrienteController(_context);
            _clienteController = new ClienteController(_context);
        }

        private void FrmEstadoCuentaCorriente_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarResumenGeneral();
            dtpDesde.Value = DateTime.Now.AddMonths(-3);
            dtpHasta.Value = DateTime.Now;
        }

        private void CargarClientes()
        {
            var clientes = _clienteController.ListarClientes();
            cbCliente.DataSource = null;
            cbCliente.Items.Clear();
            cbCliente.Items.Add("-- Todos los clientes --");
            foreach (var c in clientes)
            {
                cbCliente.Items.Add(c);
            }
            cbCliente.DisplayMember = "Nombre";
            cbCliente.SelectedIndex = 0;
        }

        private void CargarResumenGeneral()
        {
            var resumen = _ccController.ObtenerResumenGeneral();
            dgvCuentas.DataSource = null;
            dgvCuentas.DataSource = resumen;
            dgvCuentas.Columns["ClienteId"].HeaderText = "ID Cliente";
            dgvCuentas.Columns["ClienteNombre"].HeaderText = "Cliente";
            dgvCuentas.Columns["TipoCliente"].HeaderText = "Tipo";
            dgvCuentas.Columns["TotalDebitos"].HeaderText = "Total Débitos";
            dgvCuentas.Columns["TotalDebitos"].DefaultCellStyle.Format = "C2";
            dgvCuentas.Columns["TotalCreditos"].HeaderText = "Total Créditos";
            dgvCuentas.Columns["TotalCreditos"].DefaultCellStyle.Format = "C2";
            dgvCuentas.Columns["Saldo"].HeaderText = "Saldo";
            dgvCuentas.Columns["Saldo"].DefaultCellStyle.Format = "C2";
            dgvCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Colorear saldos
            foreach (DataGridViewRow row in dgvCuentas.Rows)
            {
                if (row.Cells["Saldo"].Value != null)
                {
                    decimal saldo = (decimal)row.Cells["Saldo"].Value;
                    if (saldo > 0)
                        row.Cells["Saldo"].Style.ForeColor = Color.Red;
                    else if (saldo < 0)
                        row.Cells["Saldo"].Style.ForeColor = Color.Green;
                }
            }

            decimal saldoTotal = resumen.Sum(r => r.Saldo);
            lblSaldoTotal.Text = $"Saldo Total Cuentas Corrientes: {saldoTotal:C2}";
            lblSaldoTotal.ForeColor = saldoTotal > 0 ? Color.Red : Color.Green;
        }

        private void CargarMovimientosCliente(string clienteId)
        {
            List<MovimientoCuentaCorriente> movimientos;

            if (chkFiltrarFechas.Checked)
            {
                movimientos = _ccController.ObtenerMovimientosPorFecha(clienteId, dtpDesde.Value.Date, dtpHasta.Value.Date.AddDays(1));
            }
            else
            {
                movimientos = _ccController.ObtenerMovimientos(clienteId);
            }

            var datos = movimientos.Select(m => new
            {
                m.Id,
                Fecha = m.Fecha.ToString("dd/MM/yyyy HH:mm"),
                Tipo = m.Tipo == TipoMovimientoCuenta.Debito ? "Débito" : "Crédito",
                Monto = m.Monto,
                m.Descripcion,
                VentaId = m.VentaId ?? "-"
            }).ToList();

            dgvCuentas.DataSource = null;
            dgvCuentas.DataSource = datos;
            dgvCuentas.Columns["Id"].HeaderText = "ID Mov.";
            dgvCuentas.Columns["Fecha"].HeaderText = "Fecha";
            dgvCuentas.Columns["Tipo"].HeaderText = "Tipo";
            dgvCuentas.Columns["Monto"].HeaderText = "Monto";
            dgvCuentas.Columns["Monto"].DefaultCellStyle.Format = "C2";
            dgvCuentas.Columns["Descripcion"].HeaderText = "Descripción";
            dgvCuentas.Columns["VentaId"].HeaderText = "ID Venta";
            dgvCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Colorear según tipo
            foreach (DataGridViewRow row in dgvCuentas.Rows)
            {
                if (row.Cells["Tipo"].Value != null)
                {
                    string tipo = row.Cells["Tipo"].Value.ToString();
                    if (tipo == "Débito")
                        row.Cells["Monto"].Style.ForeColor = Color.Red;
                    else
                        row.Cells["Monto"].Style.ForeColor = Color.Green;
                }
            }

            decimal saldo = _ccController.ObtenerSaldo(clienteId);
            lblSaldoTotal.Text = $"Saldo del cliente: {saldo:C2}";
            lblSaldoTotal.ForeColor = saldo > 0 ? Color.Red : Color.Green;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex == 0)
            {
                CargarResumenGeneral();
            }
            else
            {
                var cliente = cbCliente.SelectedItem as Cliente;
                if (cliente != null)
                {
                    CargarMovimientosCliente(cliente.Id);
                }
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un cliente para registrar un pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = cbCliente.SelectedItem as Cliente;
            if (cliente == null) return;

            using (var frmPago = new FrmRegistrarPago(_context, cliente))
            {
                if (frmPago.ShowDialog() == DialogResult.OK)
                {
                    CargarMovimientosCliente(cliente.Id);
                }
            }
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRegistrarPago.Enabled = cbCliente.SelectedIndex > 0;
        }
    }
}
