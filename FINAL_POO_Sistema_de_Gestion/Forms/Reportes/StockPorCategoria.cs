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
    public partial class FrmStockPorCategoria : Form
    {
        private ProductoController _productoController;
        private CategoriaController _categoriaController;
        private void Check()
        {
            if (_productoController.ListarProductos().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay productos activos disponibles.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_categoriaController.ListarCategoriasActivas().Count == 0)
            {
                this.Close();
                MessageBox.Show("Debe haber al menos un categoria activa para ver el reporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                UpdateProductos();
                updateCategorias();
            }
        }
        private void ClearFields()
        {
            cbCategoria.SelectedItem = null;
        }
        private void updateCategorias()
        {
            cbCategoria.Items.Clear();
            _categoriaController.ListarCategorias().ForEach(categoria =>
            {
                if (!cbCategoria.Items.Contains(categoria.Nombre))
                {
                    cbCategoria.Items.Add(categoria.Nombre);
                }
            });
        }
        private void UpdateProductos()
        {
            List<Producto> productos = _productoController.ListarProductos();

            if (cbCategoria.SelectedItem != null)
            {
                productos = (from p in productos
                            where p.Categoria == cbCategoria.SelectedItem.ToString()
                            select p).ToList();
            }

            var productosGrid = productos.Select(p => new
            {
                p.Id,
                p.Nombre,
                p.Categoria,
                p.StockActual
            }).ToList();

            dgvProductos.DataSource = productosGrid;
            if (dgvProductos.DataSource != null)
            {
                dgvProductos.Columns["Id"].HeaderText = "ID";
                dgvProductos.Columns["StockActual"].HeaderText = "Stock";
            }
        }
        public FrmStockPorCategoria(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            this._categoriaController = new CategoriaController(context);
            updateCategorias();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void FrmConsultaProductos_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Check();
        }

        private void cbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProductos();
        }
    }
}
