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
    public partial class FrmEgresoStock : Form
    {
        private ProductoController _productoController;
        private StockController _stockController;
        private SucursalController _sucursalController;
        private ProveedorController _proveedorController;

        public FrmEgresoStock(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            this._stockController = new StockController(context);
            this._sucursalController = new SucursalController(context);
            this._proveedorController = new ProveedorController(context);
            cbTipoEgreso.DataSource = Enum.GetValues(typeof(TipoEgreso));
            UpdateSucursales();
        }

        private void FrmEgresoStock_VisibleChanged(object sender, EventArgs e)
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
        }

        private void UpdateProductos()
        {
            cbProducto.Items.Clear();
            cbProducto.Items.AddRange(_productoController.ListarProductos()
                .Select(p => $"[{p.Id}] {p.Nombre}")
                .ToArray());
        }

        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            cbSucursal.Items.Add("(Todas las sucursales)");
            foreach (var s in _sucursalController.ListarSucursales())
                cbSucursal.Items.Add($"[{s.Id}] {s.Nombre}");
            cbSucursal.SelectedIndex = 0;
        }
        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex == -1) return;

            string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
            var producto = _productoController.ObtenerProductoPorId(idProducto);

            if (producto != null)
            {
                numCantidad.Maximum = producto.StockActual;

                lblStockActual.Text = $"Stock actual: {producto.StockActual}";
            }
        }
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idProducto = ComboBoxHelper.ExtraerId(cbProducto.SelectedItem.ToString());
                int cantidad = (int)numCantidad.Value;
                DateTime fechaEgreso = DateTime.Now;
                TipoEgreso tipoEgreso = (TipoEgreso)cbTipoEgreso.SelectedItem;

                string sucursalId = null;
                if (cbSucursal.SelectedIndex > 0)
                    sucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());

                string resultado = _stockController.RegistrarEgresoStock(idProducto, cantidad, fechaEgreso, tipoEgreso, sucursalId);
                if (!resultado.Contains("exitosamente"))
                {
                    MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Egreso de stock registrado correctamente.\n" +
                              $"Se actualizó el stock.",
                              "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar egreso de stock:\n{ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            cbProducto.SelectedItem = null;
            cbTipoEgreso.SelectedItem = null;
            numCantidad.Value = numCantidad.Minimum;
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
    }
}
