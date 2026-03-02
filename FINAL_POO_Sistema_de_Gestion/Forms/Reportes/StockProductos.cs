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
    public partial class FrmStockProductos : Form
    {
        private ProductoController _productoController;
        private void Check()
        {
            if (_productoController.ListarTodosLosProductos().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay productos disponibles.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                UpdateStock();
            }
        }
        private void UpdateStock()
        {
            dgvStockActual.DataSource = null;

            List<Producto> productos = _productoController.ListarProductos();
            var productosDataGrid = from p in productos
                                    select new
                                    {
                                        p.Id,
                                        p.Nombre,
                                        p.StockActual
                                    };

            dgvStockActual.DataSource = productosDataGrid.ToList();
            if (dgvStockActual.DataSource != null)
            {
                dgvStockActual.Columns["Id"].HeaderText = "ID";
                dgvStockActual.Columns["Nombre"].HeaderText = "Nombre";
                dgvStockActual.Columns["StockActual"].HeaderText = "Stock";
            }
        }
        public FrmStockProductos(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            UpdateStock();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FrmConsultaProductos_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible) return;
            Check();
        }
    }
}
