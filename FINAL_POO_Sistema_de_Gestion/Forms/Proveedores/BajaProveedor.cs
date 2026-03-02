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
    public partial class FrmBajaProveedor : Form
    {
        private ProveedorController _proveedorController;
        private Proveedor selectedProv;
        private void Chceck()
        {
            if (_proveedorController.ListarProveedoresActivos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar proveedores antes de poder darlos de baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
        public FrmBajaProveedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._proveedorController = new ProveedorController(context);
        }
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                _proveedorController.EliminarProveedor(selectedProv.Id);
                ClearFields();
                MessageBox.Show($"Proveedor {txtNombre.Text.Trim()} dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja el proveedor\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }
        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void FrmBajaProveedor_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Chceck();
        }

        private void cbProveedor_Click(object sender, EventArgs e)
        {
            UpdateProveedores();
        }
    }
}
