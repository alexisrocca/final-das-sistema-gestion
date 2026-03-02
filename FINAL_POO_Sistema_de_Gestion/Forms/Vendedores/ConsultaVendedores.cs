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
    public partial class FrmConsultaVendedores : Form
    {
        private VendedorController _vendedorController;
        private SucursalController _sucursalController;

        private void UpdateVendedores()
        {
            dgvVendedores.DataSource = null;
            dgvVendedores.Columns.Clear();
            List<Vendedor> vendedores = _vendedorController.ListarVendedores();

            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                vendedores = (from v in vendedores
                              where v.Nombre.ToLower().Contains(txtBuscar.Text.ToLower().Trim()) ||
                                    v.Id.ToLower().Contains(txtBuscar.Text.ToLower().Trim())
                              select v).ToList();
            }

            var vendedoresGrid = vendedores.Select(v => new
            {
                v.Id,
                v.Nombre,
                v.Legajo,
                SucursalNombre = v.Sucursal != null ? v.Sucursal.Nombre : string.Empty
            }).ToList();

            dgvVendedores.DataSource = vendedoresGrid;
            if (dgvVendedores.DataSource != null && dgvVendedores.Columns.Count > 0)
            {
                dgvVendedores.Columns["Id"].HeaderText = "ID";
                dgvVendedores.Columns["Nombre"].HeaderText = "Nombre";
                dgvVendedores.Columns["Legajo"].HeaderText = "Legajo";
                dgvVendedores.Columns["SucursalNombre"].HeaderText = "Sucursal";
            }
        }

        private void ClearFields()
        {
            txtBuscar.Clear();
        }

        public FrmConsultaVendedores(TechStoreDbContext context)
        {
            InitializeComponent();
            this._vendedorController = new VendedorController(context);
            this._sucursalController = new SucursalController(context);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            UpdateVendedores();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
            UpdateVendedores();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmConsultaVendedores_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateVendedores();
        }
    }
}
