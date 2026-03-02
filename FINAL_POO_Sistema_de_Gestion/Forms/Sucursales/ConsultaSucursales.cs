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
    public partial class FrmConsultaSucursales : Form
    {
        private SucursalController _sucursalController;

        private void UpdateSucursales()
        {
            dgvSucursales.DataSource = null;
            List<Sucursal> sucursales = _sucursalController.ListarSucursales();

            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                sucursales = (from s in sucursales
                              where s.Nombre.ToLower().Contains(txtBuscar.Text.ToLower().Trim()) ||
                                    s.Id.ToLower().Contains(txtBuscar.Text.ToLower().Trim())
                              select s).ToList();
            }

            var sucursalesGrid = sucursales.Select(s => new
            {
                s.Id,
                s.Nombre,
                s.Direccion,
                s.Telefono,
                s.Activo
            }).ToList();

            dgvSucursales.DataSource = sucursalesGrid;
            if (dgvSucursales.DataSource != null && dgvSucursales.Columns.Count > 0)
            {
                dgvSucursales.Columns["Id"].HeaderText = "ID";
                dgvSucursales.Columns["Direccion"].HeaderText = "Dirección";
                dgvSucursales.Columns["Telefono"].HeaderText = "Teléfono";
            }
        }

        private void ClearFields()
        {
            txtBuscar.Clear();
        }

        public FrmConsultaSucursales(TechStoreDbContext context)
        {
            InitializeComponent();
            this._sucursalController = new SucursalController(context);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            UpdateSucursales();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
            UpdateSucursales();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmConsultaSucursales_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateSucursales();
        }
    }
}
