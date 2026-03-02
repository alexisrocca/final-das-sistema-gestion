using System;
using System.Windows.Forms;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Forms.Clientes
{
    public partial class FrmRegistrarPago : Form
    {
        private CuentaCorrienteController _ccController;
        private Cliente _cliente;

        public FrmRegistrarPago(TechStoreDbContext context, Cliente cliente)
        {
            InitializeComponent();
            _ccController = new CuentaCorrienteController(context);
            _cliente = cliente;
        }

        private void FrmRegistrarPago_Load(object sender, EventArgs e)
        {
            lblClienteNombre.Text = _cliente.Nombre;
            decimal saldo = _ccController.ObtenerSaldo(_cliente.Id);
            lblSaldoActual.Text = $"Saldo actual: {saldo:C2}";
            lblSaldoActual.ForeColor = saldo > 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                ? $"Pago recibido - {DateTime.Now:dd/MM/yyyy}"
                : txtDescripcion.Text;

            string resultado = _ccController.RegistrarPago(_cliente.Id, monto, descripcion);

            if (resultado.Contains("exitosamente"))
            {
                MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
