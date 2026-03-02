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
    public partial class FrmModificarVendedor : Form
    {
        private VendedorController _vendedorController;
        private SucursalController _sucursalController;
        private Vendedor _selectedVendedor;

        private void UpdateVendedores()
        {
            cbVendedor.Items.Clear();
            cbVendedor.Items.AddRange(_vendedorController.ListarVendedores()
                .Select(v => $"[{v.Id}] {v.Nombre}")
                .ToArray());
        }

        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.AddRange(_sucursalController.ListarSucursales()
                .Select(s => $"[{s.Id}] {s.Nombre}")
                .ToArray());
        }

        private void ClearFields()
        {
            txtNombre.Clear();
            txtLegajo.Clear();
            cbVendedor.Text = string.Empty;
            cbSucursal.SelectedIndex = -1;
            _selectedVendedor = null;
        }

        public FrmModificarVendedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._vendedorController = new VendedorController(context);
            this._sucursalController = new SucursalController(context);
        }

        private void cbVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVendedor.SelectedItem == null) return;
            string idVendedor = ComboBoxHelper.ExtraerId(cbVendedor.SelectedItem.ToString());
            Vendedor vendedor = _vendedorController.ListarVendedores().FirstOrDefault(v => v.Id == idVendedor);
            _selectedVendedor = vendedor;
            if (vendedor != null)
            {
                txtNombre.Text = vendedor.Nombre;
                txtLegajo.Text = vendedor.Legajo;

                // Select the current sucursal in cbSucursal
                if (vendedor.Sucursal != null)
                {
                    for (int i = 0; i < cbSucursal.Items.Count; i++)
                    {
                        if (cbSucursal.Items[i].ToString().StartsWith($"[{vendedor.SucursalId}]"))
                        {
                            cbSucursal.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vendedor no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedVendedor == null)
                {
                    MessageBox.Show("Debe seleccionar un vendedor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbSucursal.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());

                string resultado = _vendedorController.ModificarVendedor(
                    _selectedVendedor.Id,
                    txtNombre.Text.Trim(),
                    txtLegajo.Text.Trim(),
                    sucursalId);

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                UpdateVendedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar vendedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmModificarVendedor_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateVendedores();
            UpdateSucursales();
        }
    }
}
