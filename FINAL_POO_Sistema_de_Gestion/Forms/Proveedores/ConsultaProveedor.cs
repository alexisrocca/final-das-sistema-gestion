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
    public partial class FrmConsultaProveedor : Form
    {
        private ProveedorController _proveedorController;
        private void Check()
        {
            if (_proveedorController.ListarProveedores().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay proveedores disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                UpdateProveedores();
            }
        }
        private void ClearFields()
        {
            txtNombreCodigo.Clear();
        }
        private void UpdateProveedores()
        {
            dgvProveedores.DataSource = null;
            List<Proveedor> proveedores = _proveedorController.ListarProveedores();

            if(!string.IsNullOrWhiteSpace(txtNombreCodigo.Text))
            {
                proveedores = (from p in proveedores
                                where p.Nombre.ToLower().Contains(txtNombreCodigo.Text.ToLower().Trim()) ||
                                      p.Id.ToLower().Contains(txtNombreCodigo.Text.ToLower().Trim())
                                      select p).ToList();

            }
            dgvProveedores.DataSource = proveedores;
            if (dgvProveedores.DataSource != null)
            {
                dgvProveedores.Columns["Id"].HeaderText = "ID";
                dgvProveedores.Columns["Id"].DisplayIndex = 0;
                dgvProveedores.Columns["Nombre"].HeaderText = "Nombre";
                dgvProveedores.Columns["Nombre"].DisplayIndex = 1;
                dgvProveedores.Columns["Direccion"].HeaderText = "Dirección";
                dgvProveedores.Columns["Direccion"].DisplayIndex = 2;
                dgvProveedores.Columns["Email"].HeaderText = "Email";
                dgvProveedores.Columns["Email"].DisplayIndex = 3;
                dgvProveedores.Columns["Tel"].HeaderText = "Teléfono";
                dgvProveedores.Columns["Tel"].DisplayIndex = 4;
                dgvProveedores.Columns["Activo"].DisplayIndex = 5;
            }
        }
        public FrmConsultaProveedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._proveedorController = new ProveedorController(context);
            UpdateProveedores();
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

        private void txtNombreCodigo_TextChanged(object sender, EventArgs e)
        {
            UpdateProveedores();
        }
    }
}
