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
    public partial class FrmAltaProducto : Form
    {
        private ProductoController _productoController;
        private SucursalController _sucursalController;
        private CategoriaController _categoriaController;
        private void CheckCategorias()
        {
            if (_categoriaController.ListarCategoriasActivas().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar al menos un categoria antes de agregar un producto.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                updateCategorias();
            }
        }
        public void updateCategorias()
        {
            _categoriaController.ListarCategoriasActivas().ForEach(categoria =>
            {
                if (!cbCategorias.Items.Contains(categoria.Nombre))
                {
                    cbCategorias.Items.Add(categoria.Nombre);
                }
            });
        }
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            numPrecioUnitario.Value = 0;
            numStockInicial.Value = 0;
            cbCategorias.SelectedIndex = -1;
            chbPoseeVencimiento.Checked = false;
            if (cbSucursal.Items.Count > 0) cbSucursal.SelectedIndex = 0;
        }
        private void CheckVencimientoFields()
        {
            gbVencimiento.Visible = chbPoseeVencimiento.Checked && numStockInicial.Value > 0;
        }
        public FrmAltaProducto(TechStoreDbContext context)
        {
            InitializeComponent();
            _productoController = new ProductoController(context);
            _sucursalController = new SucursalController(context);
            _categoriaController = new CategoriaController(context);
            updateCategorias();
            updateSucursales();
        }

        private void cbCategorias_MouseEnter(object sender, EventArgs e)
        {
            updateCategorias();
        }

        private void updateSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.Add("(Sin sucursal)");
            foreach (var s in _sucursalController.ListarSucursales())
                cbSucursal.Items.Add($"[{s.Id}] {s.Nombre}");
            cbSucursal.SelectedIndex = 0;
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto(txtNombre.Text.Trim(), txtDescripcion.Text.Trim(),numPrecioUnitario.Value,cbCategorias.Text,chbPoseeVencimiento.Checked);
                DateTime? fechaVenc = chbPoseeVencimiento.Checked ? (DateTime?)dtpFechaVencimiento.Value : null;

                string sucursalId = null;
                if (cbSucursal.SelectedIndex > 0)
                {
                    string sel = cbSucursal.SelectedItem.ToString();
                    sucursalId = ComboBoxHelper.ExtraerId(sel);
                }

                string resultado = _productoController.AgregarProductoConStock(
                    producto,
                    (int)numStockInicial.Value,
                    numPrecioUnitario.Value,
                    fechaVenc,
                    sucursalId
                );

                if (resultado.Contains("exitosamente"))
                {
                    MessageBox.Show($"Producto {txtNombre.Text.Trim()} agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void FrmAltaProducto_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            CheckCategorias();
        }

        private void chbPoseeVencimiento_CheckedChanged(object sender, EventArgs e)
        {
            CheckVencimientoFields();
        }

        private void numStockInicial_ValueChanged(object sender, EventArgs e)
        {
            CheckVencimientoFields();
        }
    }
}
