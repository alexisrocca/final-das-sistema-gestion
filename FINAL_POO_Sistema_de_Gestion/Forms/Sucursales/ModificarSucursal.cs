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
    public partial class FrmModificarSucursal : Form
    {
        private SucursalController _sucursalController;
        private Sucursal _selectedSucursal;

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
            txtDireccion.Clear();
            txtTelefono.Clear();
            cbSucursal.Text = string.Empty;
            _selectedSucursal = null;
        }

        public FrmModificarSucursal(TechStoreDbContext context)
        {
            InitializeComponent();
            this._sucursalController = new SucursalController(context);
        }

        private void cbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSucursal.SelectedItem == null) return;
            string idSucursal = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());
            Sucursal sucursal = _sucursalController.ObtenerSucursalPorId(idSucursal);
            _selectedSucursal = sucursal;
            if (sucursal != null)
            {
                txtNombre.Text = sucursal.Nombre;
                txtDireccion.Text = sucursal.Direccion;
                txtTelefono.Text = sucursal.Telefono;
            }
            else
            {
                MessageBox.Show("Sucursal no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedSucursal == null)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string resultado = _sucursalController.ModificarSucursal(
                    _selectedSucursal.Id,
                    txtNombre.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtTelefono.Text.Trim());

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                UpdateSucursales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmModificarSucursal_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateSucursales();
        }

        private void cbSucursal_Click(object sender, EventArgs e)
        {
            UpdateSucursales();
        }
    }
}
