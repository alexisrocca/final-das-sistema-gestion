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
    public partial class FrmModificarProveedor : Form
    {
        private ProveedorController _proveedorController;
        private Proveedor selectedProv;
        private void CheckAll()
        {
            if (_proveedorController.ListarProveedoresActivos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("No se encontraron proveedores para modificar.", "Sin Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                UpdateProveedores();
            }
        }
        private void UpdateProveedores()
        {
            cbProveedor.Items.Clear();
            cbProveedor.Items.AddRange(_proveedorController.ListarProveedoresActivos()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            cbProveedor.SelectedItem = null;
        }
        public FrmModificarProveedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._proveedorController = new ProveedorController(context);

        }
        private void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                selectedProv.Nombre = txtNombre.Text.Trim();
                selectedProv.Tel = txtTelefono.Text.Trim();
                selectedProv.Email = txtEmail.Text.Trim();
                selectedProv.Direccion = txtDireccion.Text.Trim();
                _proveedorController.ModificarProveedor(selectedProv);
                ClearFields();
                MessageBox.Show($"Proveedor {txtNombre.Text.Trim()} modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el proveedor\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }
        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedIndex == -1) return;
            string idProveedor = ComboBoxHelper.ExtraerId(cbProveedor.SelectedItem.ToString());
            Proveedor prov = _proveedorController.ObtenerProveedorPorId(idProveedor);
            selectedProv = prov;
            if (prov != null)
            {
                txtNombre.Text = prov.Nombre;
                txtDireccion.Text = prov.Direccion;
                txtEmail.Text = prov.Email;
                txtTelefono.Text = prov.Tel;
            }
            else
            {
                MessageBox.Show("Proveedor no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmModificarProveedor_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ClearFields();
                return;
            }
            CheckAll();
        }
        private void cbProveedor_Click(object sender, EventArgs e)
        {
            UpdateProveedores();
        }
    }
}
