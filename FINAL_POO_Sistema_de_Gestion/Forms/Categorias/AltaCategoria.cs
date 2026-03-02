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
    public partial class FrmAltaCategoria : Form
    {
        private CategoriaController _categoriaController;
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
        }
        public FrmAltaCategoria(TechStoreDbContext context)
        {
            InitializeComponent();
            _categoriaController = new CategoriaController(context);
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria categoria = new Categoria(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                string resultado = _categoriaController.AgregarCategoria(categoria);
                if (resultado.Contains("exitosamente"))
                {
                    MessageBox.Show("Categoria agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescripcion.Clear();
                    txtNombre.Clear();
                }
                else
                {
                    MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la categoria\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmAltaCategoria_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
        }
    }
}
