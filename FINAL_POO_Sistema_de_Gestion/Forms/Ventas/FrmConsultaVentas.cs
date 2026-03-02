using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FINAL_POO_Sistema_de_Gestion.Entidades;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;

namespace FINAL_POO_Sistema_de_Gestion
{
    public partial class FrmConsultaVentas : Form
    {
        private VentaController _ventaController;

        public FrmConsultaVentas(TechStoreDbContext context)
        {
            InitializeComponent();
            this._ventaController = new VentaController(context);
        }

        private void LoadVentas()
        {
            dgvVentas.DataSource = null;
            var ventas = _ventaController.ListarVentas();

            var ventasGrid = ventas.Select(v => new
            {
                v.Id,
                Fecha = v.Fecha.ToShortDateString(),
                Cliente = v.Cliente?.Nombre,
                Vendedor = v.Vendedor?.Nombre,
                MetodoPago = v.MetodoPago.ToString(),
                Descuento = v.DescuentoAplicado.ToString("P0"),
                Total = v.MontoTotal.ToString("C")
            }).ToList();

            dgvVentas.DataSource = ventasGrid;
        }

        private void LoadVentasFiltradas()
        {
            dgvVentas.DataSource = null;
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1);

            var ventas = _ventaController.ListarVentasPorFecha(desde, hasta);

            var ventasGrid = ventas.Select(v => new
            {
                v.Id,
                Fecha = v.Fecha.ToShortDateString(),
                Cliente = v.Cliente?.Nombre,
                Vendedor = v.Vendedor?.Nombre,
                MetodoPago = v.MetodoPago.ToString(),
                Descuento = v.DescuentoAplicado.ToString("P0"),
                Total = v.MontoTotal.ToString("C")
            }).ToList();

            dgvVentas.DataSource = ventasGrid;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            LoadVentasFiltradas();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            LoadVentas();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FrmConsultaVentas_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;
                LoadVentas();
            }
        }
    }
}
