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
    public partial class FrmAltaCliente : Form
    {
        private ClienteController _clienteController;

        private void ClearFields()
        {
            txtNombre.Clear();
            txtDniCuit.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            cbTipoCliente.SelectedIndex = -1;
        }

        public FrmAltaCliente(TechStoreDbContext context)
        {
            InitializeComponent();
            this._clienteController = new ClienteController(context);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTipoCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un tipo de cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string resultado;
                if (cbTipoCliente.SelectedItem.ToString() == "Minorista")
                {
                    resultado = _clienteController.AgregarClienteMinorista(
                        txtNombre.Text.Trim(),
                        txtDniCuit.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim());
                }
                else
                {
                    resultado = _clienteController.AgregarClienteMayorista(
                        txtNombre.Text.Trim(),
                        txtDniCuit.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim());
                }

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmAltaCliente_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
