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
    public partial class FrmModificarProducto : Form
    {
        private ProductoController _productoController;
        private CategoriaController _categoriaController;
        private SucursalController _sucursalController;
        private Producto selectedProd;
        private void CheckAll()
        {
            if (_productoController.ListarProductos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("No se encontraron productos para modificar.", "Sin Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_categoriaController.ListarCategoriasActivas().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar al menos un categoria antes de modificar un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                updateCategorias();
                UpdateProductos();
                UpdateSucursales();
            }
        }
        private void updateCategorias()
        {
            _categoriaController.ListarCategorias().ForEach(r =>
            {
                if (!cbCategorias.Items.Contains(r.Nombre))
                {
                    cbCategorias.Items.Add(r.Nombre);
                }
            });
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
            cbCategorias.SelectedItem = null;
            cbProducto.SelectedItem = null;
            cbSucursal.SelectedItem = null;
            chbPoseeVencimiento.Checked = false;
        }
        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            _sucursalController.ListarSucursales().ForEach(s =>
            {
                cbSucursal.Items.Add($"[{s.Id}] {s.Nombre}");
            });
        }
        public FrmModificarProducto(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            this._categoriaController = new CategoriaController(context);
            this._sucursalController = new SucursalController(context);
        }
        private void cbProducto_MouseEnter(object sender, EventArgs e)
        {
            UpdateProductos();
        }
        private void cbCategorias_MouseEnter(object sender, EventArgs e)
        {
            updateCategorias();
        }
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                selectedProd.Nombre = txtNombre.Text.Trim();
                selectedProd.Descripcion = txtDescripcion.Text.Trim();
                selectedProd.PrecioUnitario = numPrecioUnitario.Value;
                selectedProd.Categoria = cbCategorias.SelectedItem.ToString();
                selectedProd.Vencimiento = chbPoseeVencimiento.Checked;
                _productoController.ModificarProducto(selectedProd);

                // Transferir lotes a la nueva sucursal si se seleccionó una
                if (cbSucursal.SelectedItem != null)
                {
                    string nuevaSucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());
                    string resultado = _productoController.TransferirProductoASucursal(selectedProd.Id, nuevaSucursalId);
                    if (!resultado.Contains("exitosamente"))
                    {
                        MessageBox.Show(resultado, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                ClearFields();
                MessageBox.Show($"Producto modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el producto\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }
        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex == -1) return;
            string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
            Producto prod = _productoController.ObtenerProductoPorId(idProducto);
            selectedProd = prod;
            if (prod != null)
            {
                txtNombre.Text = prod.Nombre;
                txtDescripcion.Text = prod.Descripcion;
                numPrecioUnitario.Value = prod.PrecioUnitario;
                cbCategorias.SelectedItem = prod.Categoria;
                chbPoseeVencimiento.Checked = prod.Vencimiento;

                // Mostrar sucursal actual del producto
                string sucursalId = _productoController.ObtenerSucursalIdDeProducto(prod.Id);
                if (sucursalId != null)
                {
                    for (int i = 0; i < cbSucursal.Items.Count; i++)
                    {
                        if (ComboBoxHelper.ExtraerId(cbSucursal.Items[i].ToString()) == sucursalId)
                        {
                            cbSucursal.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cbSucursal.SelectedItem = null;
                }
            }
            else
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmModificarProducto_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ClearFields();
                return;
            }
            CheckAll();
        }
        private void cbProducto_Click(object sender, EventArgs e)
        {
            UpdateProductos();
        }

        private void cbCategorias_Click(object sender, EventArgs e)
        {
            updateCategorias();
        }
    }
}
