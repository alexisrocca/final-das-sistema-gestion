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
    public partial class FrmModificarCategoria : Form
    {
        private CategoriaController _categoriaController;
        private Categoria selectedCategoria;
        public FrmModificarCategoria(TechStoreDbContext context)
        {
            InitializeComponent();
            _categoriaController = new CategoriaController(context);
        }
        private void updateCategorias()
        {
            cbCategorias.Items.Clear();
            cbCategorias.Items.AddRange(_categoriaController.ListarCategoriasActivas()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            cbCategorias.SelectedItem = null;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Clear();
            txtNombre.Clear();
        }

        private void cbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategorias.SelectedIndex == -1) return;
            string idCategoria = ComboBoxHelper.ExtraerId(cbCategorias.SelectedItem.ToString());
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

        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                selectedCategoria.Nombre = txtNombre.Text.Trim();
                selectedCategoria.Descripcion = txtDescripcion.Text.Trim();
                _categoriaController.ModificarCategoria(selectedCategoria);
                ClearFields();
                MessageBox.Show($"categoria {txtNombre.Text.Trim()} modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la categoria\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbCategorias_MouseEnter(object sender, EventArgs e)
        {
            updateCategorias();
        }

        private void FrmModificarCategoria_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            updateCategorias();
        }

        private void cbCategorias_MouseMove(object sender, MouseEventArgs e)
        {
            updateCategorias();
        }
    }
}
