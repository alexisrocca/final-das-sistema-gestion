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
    public partial class FrmBajaCategoria : Form
    {
        private CategoriaController _categoriaController;
        private Categoria selectedCategoria;
        private void Chceck()
        {
            if (_categoriaController.ListarCategoriasActivas().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar categorias antes de poder darlos de baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                updateCategorias();
            }
        }
        private void updateCategorias()
        {
            cbCategoria.Items.Clear();
            cbCategoria.Items.AddRange(_categoriaController.ListarCategoriasActivas()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            cbCategoria.Text = string.Empty;
        }
        public FrmBajaCategoria(TechStoreDbContext context)
        {
            InitializeComponent();
            _categoriaController = new CategoriaController(context);
        }
        private void cbRubro_Click(object sender, EventArgs e)
        {
            updateCategorias();
        }
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                bool tieneProductos = _categoriaController.TieneCategoriaProductosAsociados(selectedCategoria.Nombre);
                if (tieneProductos)
                {
                    MessageBox.Show($"No se puede dar de baja la categoria {selectedCategoria.Nombre} porque tiene productos asociados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _categoriaController.EliminarCategoria(selectedCategoria.Id);
                ClearFields();
                MessageBox.Show($"categoria {txtNombre.Text.Trim()} dada de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja La categoria\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }
        private void cbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idCategoria = ComboBoxHelper.ExtraerId(cbCategoria.SelectedItem.ToString());
            Categoria categoria = _categoriaController.ObtenerCategoriaPorId(idCategoria);
            selectedCategoria = categoria;
            if (categoria != null)
            {
                txtNombre.Text = categoria.Nombre;
                txtDescripcion.Text = categoria.Descripcion;
            }
            else
            {
                MessageBox.Show("Categoria no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            updateCategorias();
        }
    }
}
