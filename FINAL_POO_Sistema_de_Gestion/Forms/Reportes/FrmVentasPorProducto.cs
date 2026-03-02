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
    public partial class FrmVentasPorProducto : Form
    {
        private VentaController _ventaController;
        private ProductoController _productoController;

        public FrmVentasPorProducto(TechStoreDbContext context)
        {
            InitializeComponent();
            _ventaController = new VentaController(context);
            _productoController = new ProductoController(context);
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
            LoadProductos();
        }

        private void LoadProductos()
        {
            cbProducto.Items.Clear();
            cbProducto.Items.Add("-- Todos los productos --");
            foreach (var p in _productoController.ListarProductos())
                cbProducto.Items.Add($"[{p.Id}] {p.Nombre}");
            cbProducto.SelectedIndex = 0;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1);

            var ventas = _ventaController.ListarVentasPorFecha(desde, hasta);

            string productoId = null;
            if (cbProducto.SelectedIndex > 0)
            {
                string sel = cbProducto.SelectedItem.ToString();
                productoId = ComboBoxHelper.ExtraerId(sel);
            }

            var detallesFiltrados = ventas.SelectMany(v => v.Detalles.Select(d => new { Venta = v, Detalle = d }));

            if (!string.IsNullOrEmpty(productoId))
                detallesFiltrados = detallesFiltrados.Where(x => x.Detalle.ProductoId == productoId);

            var resultado = detallesFiltrados.Select(x => new
            {
                x.Venta.Id,
                Fecha = x.Venta.Fecha.ToShortDateString(),
                Cliente = x.Venta.Cliente?.Nombre ?? "N/A",
                Vendedor = x.Venta.Vendedor?.Nombre ?? "N/A",
                Producto = x.Detalle.Producto?.Nombre ?? "N/A",
                x.Detalle.Cantidad,
                PrecioUnit = x.Detalle.PrecioUnitario.ToString("C"),
                Subtotal = x.Detalle.Subtotal.ToString("C")
            }).ToList();

            dgvVentas.DataSource = resultado;

            decimal totalPeriodo = detallesFiltrados.Sum(x => x.Detalle.Subtotal);
            lblTotalPeriodo.Text = $"Total: {totalPeriodo:C} ({resultado.Count} registros)";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVentasPorProducto_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadProductos();
                dgvVentas.DataSource = null;
                lblTotalPeriodo.Text = "Total: $0.00";
            }
        }
    }
}
