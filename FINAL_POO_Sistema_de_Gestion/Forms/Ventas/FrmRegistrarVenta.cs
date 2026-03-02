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
    public partial class FrmRegistrarVenta : Form
    {
        private VentaController _ventaController;
        private ClienteController _clienteController;
        private VendedorController _vendedorController;
        private ProductoController _productoController;
        private CuentaCorrienteController _ccController;
        private List<DetalleVenta> _detalles = new List<DetalleVenta>();
        private decimal _descuento = 0;

        public FrmRegistrarVenta(TechStoreDbContext context)
        {
            InitializeComponent();
            this._ventaController = new VentaController(context);
            this._clienteController = new ClienteController(context);
            this._vendedorController = new VendedorController(context);
            this._productoController = new ProductoController(context);
            this._ccController = new CuentaCorrienteController(context);
        }

        private void LoadCombos()
        {
            // Clientes
            var clientes = _clienteController.ListarClientes();
            cbCliente.DataSource = null;
            cbCliente.Items.Clear();
            cbCliente.Items.Add("-- Seleccione --");
            foreach (var c in clientes)
                cbCliente.Items.Add($"[{c.Id}] {c.Nombre}");
            cbCliente.SelectedIndex = 0;

            // Vendedores
            var vendedores = _vendedorController.ListarVendedores();
            cbVendedor.DataSource = null;
            cbVendedor.Items.Clear();
            cbVendedor.Items.Add("-- Seleccione --");
            foreach (var v in vendedores)
                cbVendedor.Items.Add($"[{v.Id}] {v.Nombre}");
            cbVendedor.SelectedIndex = 0;

            // Productos
            var productos = _productoController.ListarProductos();
            cbProducto.DataSource = null;
            cbProducto.Items.Clear();
            cbProducto.Items.Add("-- Seleccione --");
            foreach (var p in productos)
                cbProducto.Items.Add($"[{p.Id}] {p.Nombre} (Stock: {p.StockActual})");
            cbProducto.SelectedIndex = 0;

            // Método de Pago
            cbMetodoPago.Items.Clear();
            cbMetodoPago.Items.Add("Efectivo");
            cbMetodoPago.Items.Add("Tarjeta");
            cbMetodoPago.Items.Add("Transferencia");
            cbMetodoPago.Items.Add("Cuenta Corriente");
            cbMetodoPago.SelectedIndex = 0;
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex <= 0)
            {
                _descuento = 0;
                lblDescuento.Text = "Descuento: 0%";
            lblSaldoCC.Visible = false;
                return;
            }

            string selected = cbCliente.SelectedItem.ToString();
            string id = ComboBoxHelper.ExtraerId(selected);
            var clientes = _clienteController.ListarClientes();
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente != null)
            {
                _descuento = cliente.ObtenerLimiteDescuento();
                lblDescuento.Text = $"Descuento: {(_descuento * 100):0}%";
            }
            else
            {
                _descuento = 0;
                lblDescuento.Text = "Descuento: 0%";
            lblSaldoCC.Visible = false;
            }

            UpdateTotal();
            ActualizarSaldoCC();
        }

        private void cbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarSaldoCC();
        }

        private void ActualizarSaldoCC()
        {
            bool esCuentaCorriente = cbMetodoPago.SelectedIndex == (int)MetodoPago.CuentaCorriente;

            if (esCuentaCorriente && cbCliente.SelectedIndex > 0)
            {
                string clienteStr = cbCliente.SelectedItem.ToString();
                string clienteId = ComboBoxHelper.ExtraerId(clienteStr);
                decimal saldo = _ccController.ObtenerSaldo(clienteId);
                lblSaldoCC.Text = $"Saldo pendiente: {saldo:C2}";
                lblSaldoCC.ForeColor = saldo > 0 ? Color.Red : Color.Green;
                lblSaldoCC.Visible = true;
            }
            else
            {
                lblSaldoCC.Visible = false;
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selected = cbProducto.SelectedItem.ToString();
            string id = ComboBoxHelper.ExtraerId(selected);
            var productos = _productoController.ListarProductos();
            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            var detalle = new DetalleVenta(producto, cantidad, producto.PrecioUnitario);
            _detalles.Add(detalle);

            RefreshGrid();
            UpdateTotal();
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow == null || dgvDetalles.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccione un detalle para quitar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dgvDetalles.CurrentRow.Index;
            if (index >= 0 && index < _detalles.Count)
            {
                _detalles.RemoveAt(index);
                RefreshGrid();
                UpdateTotal();
            }
        }

        private void RefreshGrid()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _detalles.Select(d => new
            {
                Producto = d.Producto.Nombre,
                d.Cantidad,
                PrecioUnit = d.PrecioUnitario,
                d.Subtotal
            }).ToList();
        }

        private void UpdateTotal()
        {
            decimal total = _detalles.Sum(d => d.Subtotal) * (1 - _descuento);
            lblTotal.Text = $"Total: {total:C}";
        }

        private void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbVendedor.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un vendedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener cliente
            string clienteStr = cbCliente.SelectedItem.ToString();
            string clienteId = ComboBoxHelper.ExtraerId(clienteStr);
            var cliente = _clienteController.ObtenerClientePorId(clienteId);

            // Obtener vendedor
            string vendedorStr = cbVendedor.SelectedItem.ToString();
            string vendedorId = ComboBoxHelper.ExtraerId(vendedorStr);
            var vendedor = _vendedorController.ObtenerVendedorPorId(vendedorId);

            // Obtener método de pago
            MetodoPago metodo = (MetodoPago)cbMetodoPago.SelectedIndex;

            // Validación para Cuenta Corriente
            if (metodo == MetodoPago.CuentaCorriente)
            {
                decimal saldoActual = _ccController.ObtenerSaldo(clienteId);
                decimal totalVenta = _detalles.Sum(d => d.Subtotal) * (1 - _descuento);
                decimal nuevoSaldo = saldoActual + totalVenta;

                var confirmResult = MessageBox.Show(
                    $"El cliente tiene un saldo pendiente de {saldoActual:C2}.\n" +
                    $"Esta venta sumará {totalVenta:C2} a su cuenta corriente.\n" +
                    $"Saldo total resultante: {nuevoSaldo:C2}.\n\n" +
                    "¿Desea continuar?",
                    "Confirmar Cuenta Corriente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;
            }

            string resultado = _ventaController.RegistrarVenta(cliente, vendedor, _detalles, metodo);
            MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (resultado.ToLower().Contains("éxito") || resultado.ToLower().Contains("exito") || resultado.ToLower().Contains("registrada"))
            {
                ClearAll();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearAll();
            this.Hide();
        }

        private void ClearAll()
        {
            _detalles.Clear();
            _descuento = 0;
            dgvDetalles.DataSource = null;
            lblTotal.Text = "Total: $0.00";
            lblDescuento.Text = "Descuento: 0%";
            lblSaldoCC.Visible = false;
            nudCantidad.Value = 1;
            if (cbCliente.Items.Count > 0) cbCliente.SelectedIndex = 0;
            if (cbVendedor.Items.Count > 0) cbVendedor.SelectedIndex = 0;
            if (cbProducto.Items.Count > 0) cbProducto.SelectedIndex = 0;
            if (cbMetodoPago.Items.Count > 0) cbMetodoPago.SelectedIndex = 0;
        }

        private void FrmRegistrarVenta_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadCombos();
                _detalles.Clear();
                dgvDetalles.DataSource = null;
                lblTotal.Text = "Total: $0.00";
                lblDescuento.Text = "Descuento: 0%";
            lblSaldoCC.Visible = false;
                _descuento = 0;
            }
        }
    }
}
