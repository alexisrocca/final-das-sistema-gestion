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
    public partial class FrmBajaVendedor : Form
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

        private void ClearFields()
        {
            txtNombre.Clear();
            txtLegajo.Clear();
            txtSucursal.Clear();
            cbVendedor.Text = string.Empty;
            _selectedVendedor = null;
        }

        public FrmBajaVendedor(TechStoreDbContext context)
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
                txtSucursal.Text = vendedor.Sucursal != null ? vendedor.Sucursal.Nombre : string.Empty;
            }
            else
            {
                MessageBox.Show("Vendedor no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedVendedor == null)
                {
                    MessageBox.Show("Debe seleccionar un vendedor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string resultado = _vendedorController.EliminarVendedor(_selectedVendedor.Id);
                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                UpdateVendedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dar de baja el vendedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmBajaVendedor_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateVendedores();
        }
    }
}
