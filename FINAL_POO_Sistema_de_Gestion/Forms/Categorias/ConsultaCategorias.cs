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
    public partial class FrmConsultaCategorias : Form
    {
        private CategoriaController _categoriaController;
        private void Check()
        {
            if (_categoriaController.ListarCategorias().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay categorias disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                updateCategorias();
            }
        }
        private void updateCategorias()
        {
            dgvCategorias.DataSource = null;

            dgvCategorias.DataSource = _categoriaController.ListarCategorias();
            if (dgvCategorias.DataSource != null)
            {
                dgvCategorias.Columns["Id"].HeaderText = "ID";
                dgvCategorias.Columns["Id"].DisplayIndex = 0;
                dgvCategorias.Columns["Nombre"].HeaderText = "Nombre";
                dgvCategorias.Columns["Nombre"].DisplayIndex = 1;
                dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
                dgvCategorias.Columns["Descripcion"].DisplayIndex = 2;
                dgvCategorias.Columns["Activo"].DisplayIndex = 3;
            }
        }
        public FrmConsultaCategorias(TechStoreDbContext context)
        {
            InitializeComponent();
            _categoriaController = new CategoriaController(context);
            updateCategorias();
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
