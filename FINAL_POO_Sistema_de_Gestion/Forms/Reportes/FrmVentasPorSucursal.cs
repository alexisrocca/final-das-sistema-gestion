using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FINAL_POO_Sistema_de_Gestion.Entidades;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Helpers;

namespace FINAL_POO_Sistema_de_Gestion
{
    public partial class FrmVentasPorSucursal : Form
    {
        private VentaController _ventaController;
        private SucursalController _sucursalController;

        public FrmVentasPorSucursal(TechStoreDbContext context)
        {
            InitializeComponent();
            _ventaController = new VentaController(context);
            _sucursalController = new SucursalController(context);
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
            LoadSucursales();
        }

        private void LoadSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.Add("-- Todas las sucursales --");
            foreach (var s in _sucursalController.ListarSucursales())
                cbSucursal.Items.Add($"[{s.Id}] {s.Nombre}");
            cbSucursal.SelectedIndex = 0;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1);

            string sucursalId = null;
            if (cbSucursal.SelectedIndex > 0)
            {
                string sel = cbSucursal.SelectedItem.ToString();
                sucursalId = ComboBoxHelper.ExtraerId(sel);
            }

            var ventas = _ventaController.ListarVentasPorSucursal(desde, hasta, sucursalId);

            var resultado = ventas.Select(v => new
            {
                v.Id,
                Fecha = v.Fecha.ToShortDateString(),
                Cliente = v.Cliente?.Nombre ?? "N/A",
                Vendedor = v.Vendedor?.Nombre ?? "N/A",
                Sucursal = v.Vendedor?.Sucursal?.Nombre ?? "N/A",
                MetodoPago = v.MetodoPago.ToString(),
                Descuento = v.DescuentoAplicado.ToString("P0"),
                Total = v.MontoTotal.ToString("C")
            }).ToList();

            dgvVentas.DataSource = resultado;

            decimal totalPeriodo = ventas.Sum(v => v.MontoTotal);
            lblTotalPeriodo.Text = $"Total: {totalPeriodo:C} ({resultado.Count} ventas)";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVentasPorSucursal_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadSucursales();
                dgvVentas.DataSource = null;
                lblTotalPeriodo.Text = "Total: $0.00";
            }
        }
    }
}
