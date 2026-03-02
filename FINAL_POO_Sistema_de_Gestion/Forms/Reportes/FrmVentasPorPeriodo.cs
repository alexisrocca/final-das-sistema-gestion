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
    public partial class FrmVentasPorPeriodo : Form
    {
        private VentaController _ventaController;

        public FrmVentasPorPeriodo(TechStoreDbContext context)
        {
            InitializeComponent();
            _ventaController = new VentaController(context);
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1);

            var ventas = _ventaController.ListarVentasPorPeriodo(desde, hasta);

            dgvVentas.DataSource = ventas.Select(v => new
            {
                v.Id,
                Fecha = v.Fecha.ToShortDateString(),
                Cliente = v.Cliente != null ? v.Cliente.Nombre : "N/A",
                Vendedor = v.Vendedor != null ? v.Vendedor.Nombre : "N/A",
                MetodoPago = v.MetodoPago.ToString(),
                Descuento = v.DescuentoAplicado.ToString("P0"),
                Total = v.MontoTotal.ToString("C")
            }).ToList();

            decimal totalPeriodo = ventas.Sum(v => v.MontoTotal);
            lblTotalPeriodo.Text = $"Total del periodo: {totalPeriodo:C}";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
