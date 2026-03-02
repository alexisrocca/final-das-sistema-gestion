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
    public partial class FrmAltaVendedor : Form
    {
        private VendedorController _vendedorController;
        private SucursalController _sucursalController;

        private void ClearFields()
        {
            txtNombre.Clear();
            txtLegajo.Clear();
            cbSucursal.SelectedIndex = -1;
        }

        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.AddRange(_sucursalController.ListarSucursales()
                .Select(s => $"[{s.Id}] {s.Nombre}")
                .ToArray());
        }

        public FrmAltaVendedor(TechStoreDbContext context)
        {
            InitializeComponent();
            this._vendedorController = new VendedorController(context);
            this._sucursalController = new SucursalController(context);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSucursal.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());

                string resultado = _vendedorController.AgregarVendedor(
                    txtNombre.Text.Trim(),
                    txtLegajo.Text.Trim(),
                    sucursalId);

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar vendedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmAltaVendedor_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateSucursales();
        }
    }
}
