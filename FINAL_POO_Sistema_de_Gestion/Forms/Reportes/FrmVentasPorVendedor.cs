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
    public partial class FrmVentasPorVendedor : Form
    {
        private VentaController _ventaController;
        private VendedorController _vendedorController;

        public FrmVentasPorVendedor(TechStoreDbContext context)
        {
            InitializeComponent();
            _ventaController = new VentaController(context);
            _vendedorController = new VendedorController(context);
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
            LoadVendedores();
        }

        private void LoadVendedores()
        {
            cbVendedor.Items.Clear();
            cbVendedor.Items.Add("-- Todos los vendedores --");
            foreach (var v in _vendedorController.ListarVendedores())
                cbVendedor.Items.Add($"[{v.Id}] {v.Nombre}");
            cbVendedor.SelectedIndex = 0;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1);

            string vendedorId = null;
            if (cbVendedor.SelectedIndex > 0)
            {
                string sel = cbVendedor.SelectedItem.ToString();
                vendedorId = ComboBoxHelper.ExtraerId(sel);
            }

            var ventas = _ventaController.ListarVentasPorVendedor(desde, hasta, vendedorId);

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

        private void FrmVentasPorVendedor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadVendedores();
                dgvVentas.DataSource = null;
                lblTotalPeriodo.Text = "Total: $0.00";
            }
        }
    }
}
