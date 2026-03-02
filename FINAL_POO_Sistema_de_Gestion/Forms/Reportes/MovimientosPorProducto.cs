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
    public partial class FrmMovimientosPorProducto : Form
    {
        private StockController _stockController;
        private ProductoController _productoController;
        private void Check()
        {
            if (_productoController.ListarTodosLosProductos().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay productos disponibles.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_stockController.ListarMovimientos().Count == 0)
            {
                this.Close();
                MessageBox.Show("Debe haber al menos un movimiento de stock para ver el reporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                UpdateMovimientos();
                UpdateProducto();
            }
        }
        private void ClearFields()
        {
            cbProducto.SelectedItem = null;
            dgvMovimientos.DataSource = null;
        }
        private void UpdateProducto()
        {
            cbProducto.Items.Clear();
            _productoController.ListarTodosLosProductos().ForEach(p =>
            {
                if (!cbProducto.Items.Contains(p.Nombre))
                {
                    cbProducto.Items.Add(p.Nombre);
                }
            });
        }
        private void UpdateMovimientos()
        {
            if (cbProducto.SelectedItem == null) return;
            dgvMovimientos.DataSource = null;
            string productoSeleccionado = cbProducto.SelectedItem.ToString();

            var gridData = _stockController.ListarMovimientosPorProductoConDetalles(productoSeleccionado);

            if (gridData.Count > 0)
            {
                dgvMovimientos.DataSource = gridData;
                dgvMovimientos.Columns["Id"].Visible = false;
                dgvMovimientos.Columns["Producto"].HeaderText = "Producto";
                dgvMovimientos.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvMovimientos.Columns["TipoMovimiento"].HeaderText = "Tipo Movimiento";
                dgvMovimientos.Columns["Proveedor"].HeaderText = "Proveedor";
                dgvMovimientos.Columns["Fecha"].HeaderText = "Fecha";
                dgvMovimientos.Columns["TipoEgreso"].HeaderText = "Tipo Egreso";
                dgvMovimientos.Columns["PrecioCompra"].HeaderText = "Precio Compra";
            }
            else
            {
                MessageBox.Show("No se encontraron movimientos para el proveedor seleccionado.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public FrmMovimientosPorProducto(TechStoreDbContext context)
        {
            InitializeComponent();
            this._stockController = new StockController(context);
            this._productoController = new ProductoController(context);
            UpdateProducto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void FrmConsultaProductos_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Check();
        }

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMovimientos();
        }
    }
}
