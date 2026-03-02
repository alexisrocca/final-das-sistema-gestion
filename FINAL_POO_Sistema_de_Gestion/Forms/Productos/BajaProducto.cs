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
using FINAL_POO_Sistema_de_Gestion.Helpers;

namespace FINAL_POO_Sistema_de_Gestion
{
    public partial class FrmBajaProducto : Form
    {
        private ProductoController _productoController;
        private Producto selectedProd;
        private void Chceck()
        {
            if (_productoController.ListarProductos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar productos antes de poder darlos de baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                UpdateProductos();
            }
        }
        private void UpdateProductos()
        {
            cbProducto.Items.Clear();
            cbProducto.Items.AddRange(_productoController.ListarProductos()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            numPrecioUnitario.Value = 0;
            txtCategoria.Text = string.Empty;
            cbProducto.Text = string.Empty;
            chbPoseeVencimiento.Checked = false;
        }
        public FrmBajaProducto(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
        }
        private void cbProducto_MouseEnter(object sender, EventArgs e)
        {
            UpdateProductos();
        }
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                _productoController.EliminarProducto(selectedProd.Id);
                ClearFields();
                MessageBox.Show($"Producto {txtNombre.Text.Trim()} dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja el producto\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }
        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
            Producto prod = _productoController.ObtenerProductoPorId(idProducto);
            selectedProd = prod;
            if (prod != null)
            {
                txtNombre.Text = prod.Nombre;
                txtDescripcion.Text = prod.Descripcion;
                numPrecioUnitario.Value = prod.PrecioUnitario;
                txtCategoria.Text = prod.Categoria;
                chbPoseeVencimiento.Checked = prod.Vencimiento;
            }
            else
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBajaProducto_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Chceck();
        }

        private void cbProducto_Click(object sender, EventArgs e)
        {
            UpdateProductos();
        }
    }
}
