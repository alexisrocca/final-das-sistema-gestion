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
    public partial class FrmAltaProveedor : Form
    {
        private ProveedorController _proveedorController;
        private void ClearFields()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
        }
        public FrmAltaProveedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._proveedorController = new ProveedorController(context);
        }

        private void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor(txtNombre.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim().ToLower(), txtDireccion.Text.Trim());
                _proveedorController.AgregarProveedor(proveedor);
                MessageBox.Show($"Proveedor {txtNombre.Text.Trim()} agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el proveedor.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void FrmAltaProveedor_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
        }
    }
}
