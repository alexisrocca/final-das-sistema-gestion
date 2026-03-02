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
    public partial class FrmBajaCliente : Form
    {
        private ClienteController _clienteController;
        private Cliente _selectedCliente;

        private void UpdateClientes()
        {
            cbCliente.Items.Clear();
            cbCliente.Items.AddRange(_clienteController.ListarClientes()
                .Select(c => $"[{c.Id}] {c.Nombre}")
                .ToArray());
        }

        private void ClearFields()
        {
            txtNombre.Clear();
            txtDniCuit.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtTipo.Clear();
            cbCliente.Text = string.Empty;
            _selectedCliente = null;
        }

        public FrmBajaCliente(TechStoreDbContext context)
        {
            InitializeComponent();
            this._clienteController = new ClienteController(context);
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedItem == null) return;
            string idCliente = ComboBoxHelper.ExtraerId(cbCliente.SelectedItem.ToString());
            Cliente cliente = _clienteController.ObtenerClientePorId(idCliente);
            _selectedCliente = cliente;
            if (cliente != null)
            {
                txtNombre.Text = cliente.Nombre;
                txtDniCuit.Text = cliente.DniCuit;
                txtEmail.Text = cliente.Email;
                txtTelefono.Text = cliente.Telefono;
                txtDireccion.Text = cliente.Direccion;
                txtTipo.Text = cliente is ClienteMinorista ? "Minorista" : "Mayorista";
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCliente == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string resultado = _clienteController.EliminarCliente(_selectedCliente.Id);
                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                UpdateClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmBajaCliente_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateClientes();
        }

        private void cbCliente_Click(object sender, EventArgs e)
        {
            UpdateClientes();
        }
    }
}
