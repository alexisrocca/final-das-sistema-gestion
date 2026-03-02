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
    public partial class FrmProductosMasVendidos : Form
    {
        private VentaController _ventaController;

        public FrmProductosMasVendidos(TechStoreDbContext context)
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
            int topN = (int)nudTop.Value;

            var ranking = _ventaController.ObtenerProductosMasVendidos(desde, hasta, topN);

            dgvProductos.DataSource = ranking;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
