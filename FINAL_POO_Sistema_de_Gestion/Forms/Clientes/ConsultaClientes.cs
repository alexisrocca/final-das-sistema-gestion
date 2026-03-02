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
    public partial class FrmConsultaClientes : Form
    {
        private ClienteController _clienteController;

        private void UpdateClientes()
        {
            dgvClientes.DataSource = null;
            List<Cliente> clientes = _clienteController.ListarClientes();

            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                clientes = (from c in clientes
                            where c.Nombre.ToLower().Contains(txtBuscar.Text.ToLower().Trim()) ||
                                  c.Id.ToLower().Contains(txtBuscar.Text.ToLower().Trim())
                            select c).ToList();
            }

            var clientesGrid = clientes.Select(c => new
            {
                c.Id,
                c.Nombre,
                c.DniCuit,
                c.Email,
                c.Telefono,
                c.Direccion,
                c.Activo
            }).ToList();

            dgvClientes.DataSource = clientesGrid;
            if (dgvClientes.DataSource != null && dgvClientes.Columns.Count > 0)
            {
                dgvClientes.Columns["Id"].HeaderText = "ID";
                dgvClientes.Columns["DniCuit"].HeaderText = "DNI/CUIT";
                dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
                dgvClientes.Columns["Direccion"].HeaderText = "Dirección";
            }
        }

        private void ClearFields()
        {
            txtBuscar.Clear();
        }

        public FrmConsultaClientes(TechStoreDbContext context)
        {
            InitializeComponent();
            this._clienteController = new ClienteController(context);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            UpdateClientes();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
            UpdateClientes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Hide();
        }

        private void FrmConsultaClientes_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            UpdateClientes();
        }
    }
}
