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
    public partial class FrmIngresoStock : Form
    {
        private ProductoController _productoController;
        private StockController _stockController;
        private SucursalController _sucursalController;
        private ProveedorController _proveedorController;

        public FrmIngresoStock(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            this._stockController = new StockController(context);
            this._sucursalController = new SucursalController(context);
            this._proveedorController = new ProveedorController(context);
        }

        private void FrmAltaProducto_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Check();
        }

        private void Check()
        {
            if (_productoController.ListarProductos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar al menos un producto antes de registrar un ingreso.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_proveedorController.ListarProveedoresActivos().Count == 0)
            {
                this.Hide();
                MessageBox.Show("Debe agregar al menos un proveedor antes de registrar un ingreso.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateProductos();
            UpdateProveedores();
            UpdateSucursales();
        }

        private void UpdateProductos()
        {
            cbProducto.Items.Clear();
            cbProducto.Items.AddRange(_productoController.ListarProductos()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }

        private void UpdateProveedores()
        {
            cbProveedor.Items.Clear();
            cbProveedor.Items.AddRange(_proveedorController.ListarProveedoresActivos()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }

        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.Add("(Sin sucursal)");
            cbSucursal.Items.AddRange(_sucursalController.ListarSucursales()
                .Select(s => $"[{s.Id}] {s.Nombre}")
                .ToArray());
            cbSucursal.SelectedIndex = 0;
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex == -1) return;

            string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
            var producto = _productoController.ObtenerProductoPorId(idProducto);

            if (producto != null)
            {
                gbVencimiento.Visible = producto.Vencimiento;

                lblStockActual.Text = $"Stock actual: {producto.StockActual}";
            }
        }

        private void numPrecioCompra_ValueChanged(object sender, EventArgs e)
        {
            decimal precioVenta = numPrecioCompra.Value * 1.5m;
            lblPrecioVentaCalculado.Text = $"Precio de venta será: ${precioVenta:F2}";
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
                int cantidad = (int)numCantidad.Value;
                string idProveedor = ComboBoxHelper.ExtraerId(cbProveedor.SelectedItem.ToString());
                DateTime fechaIngreso = DateTime.Now;
                decimal precioCompra = numPrecioCompra.Value;

                string sucursalId = null;
                if (cbSucursal.SelectedIndex > 0)
                    sucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());

                var producto = _productoController.ObtenerProductoPorId(idProducto);

                if (producto.Vencimiento)
                {
                    DateTime fechaVencimiento = dtpFechaVencimiento.Value;
                    string resultado = _stockController.RegistrarIngresoStock(
                        idProducto, cantidad, idProveedor, fechaIngreso, precioCompra, fechaVencimiento, sucursalId);
                    if (!resultado.Contains("exitosamente"))
                    {
                        MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    string resultado = _stockController.RegistrarIngresoStock(
                        idProducto, cantidad, idProveedor, fechaIngreso, precioCompra, null, sucursalId);
                    if (!resultado.Contains("exitosamente"))
                    {
                        MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Ingreso de stock registrado correctamente.\n" +
                              $"Se actualizó el stock y el precio de venta del producto.",
                              "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar ingreso de stock:\n{ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            cbProducto.SelectedIndex = -1;
            cbProveedor.SelectedIndex = -1;
            numCantidad.Value = 1;
            numPrecioCompra.Value = 0;
            dtpFechaVencimiento.Value = DateTime.Now.AddDays(30);
            lblPrecioVentaCalculado.Text = "";
            lblStockActual.Text = "";
            if (cbSucursal.Items.Count > 0) cbSucursal.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void cbProducto_Click(object sender, EventArgs e)
        {
            UpdateProductos();
        }

        private void cbProveedor_Click(object sender, EventArgs e)
        {
            UpdateProveedores();
        }
    }
}
