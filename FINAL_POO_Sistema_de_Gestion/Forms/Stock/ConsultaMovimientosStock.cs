using FINAL_POO_Sistema_de_Gestion.Entidades;
using FINAL_POO_Sistema_de_Gestion.Forms.Stock;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_POO_Sistema_de_Gestion
{
    public partial class FrmConsultaMovimientosStock : Form
    {
        private StockController _stockController;
        private ProductoController _productoController;
        private CategoriaController _categoriaController;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private void Check()
        {
            if (_productoController.ListarTodosLosProductos().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay productos disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_categoriaController.ListarCategorias().Count == 0)
            {
                this.Close();
                MessageBox.Show("Debe agregar al menos un Categoria antes de consultar productos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_stockController.ListarMovimientos().Count == 0)
            {
                this.Close();
                MessageBox.Show("No hay movimientos de stock registrados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                UpdateProductos();
                UpdateTipo();
                UpdateMovimientos();
                btnSeleccionarFecha.Text = $"Seleccionar rango";
            }
        }
        private void ClearFields()
        {
            cbProductos.SelectedItem = null;
            cbTipo.SelectedItem = null;
            this.fechaInicio = DateTime.MinValue;
            this.fechaFin = DateTime.MaxValue;
            btnSeleccionarFecha.Text = $"Seleccionar rango";
            UpdateMovimientos();
        }
        private void UpdateTipo()
        {
            cbTipo.Items.Clear();
            cbTipo.Items.AddRange(Enum.GetNames(typeof(TipoMovimiento)));
        }
        private void UpdateProductos()
        {
            cbProductos.Items.Clear();
            _productoController.ListarTodosLosProductos().ForEach(p =>
            {
                if (!cbProductos.Items.Contains(p.Nombre))
                {
                    cbProductos.Items.Add(p.Nombre);
                }
            });
        }
        private void UpdateMovimientos()
        {
            dgvMovimientos.DataSource = null;
            var gridData = _stockController.ListarMovimientosConDetalles();

            // Apply filters on the already-projected data
            if (cbProductos.SelectedItem != null)
            {
                string productoSeleccionado = cbProductos.SelectedItem.ToString();
                gridData = gridData.Where(d => ((dynamic)d).Producto == productoSeleccionado).ToList();
            }
            if (cbTipo.SelectedItem != null)
            {
                gridData = gridData.Where(d => ((dynamic)d).TipoMovimiento == cbTipo.SelectedItem.ToString()).ToList();
            }

            // Date filter
            gridData = gridData.Where(d => {
                DateTime fecha;
                if (DateTime.TryParse(((dynamic)d).Fecha, out fecha))
                    return fecha >= this.fechaInicio && fecha <= this.fechaFin;
                return true;
            }).ToList();

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
                MessageBox.Show("No se encontraron movimientos para la fecha seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public FrmConsultaMovimientosStock(TechStoreDbContext context)
        {
            InitializeComponent();
            this._stockController = new StockController(context);
            this._productoController = new ProductoController(context);
            this._categoriaController = new CategoriaController(context);
            this.fechaInicio = DateTime.MinValue;
            this.fechaFin = DateTime.Now;
            UpdateTipo();
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

        private void btnSeleccionarFecha_Click(object sender, EventArgs e)
        {
            FrmSeleccionFecha frmSeleccionFecha = new FrmSeleccionFecha();
            if (frmSeleccionFecha.ShowDialog() == DialogResult.OK)
            {
                this.fechaInicio = frmSeleccionFecha.fechaInicio;
                this.fechaFin  = frmSeleccionFecha.fechaFin;
                btnSeleccionarFecha.Text = $"{this.fechaInicio.ToShortDateString()} - {this.fechaFin.ToShortDateString()}";
                UpdateMovimientos();

            }
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMovimientos();
        }

        private void cbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMovimientos();
        }
    }
}
