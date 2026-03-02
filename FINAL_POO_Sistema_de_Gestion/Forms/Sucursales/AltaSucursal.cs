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
    public partial class FrmAltaSucursal : Form
    {
        private SucursalController _sucursalController;

        private void ClearFields()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
        }

        public FrmAltaSucursal(TechStoreDbContext context)
        {
            InitializeComponent();
            this._sucursalController = new SucursalController(context);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = _sucursalController.AgregarSucursal(
                    txtNombre.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtTelefono.Text.Trim());

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmAltaSucursal_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
        }
    }
}
