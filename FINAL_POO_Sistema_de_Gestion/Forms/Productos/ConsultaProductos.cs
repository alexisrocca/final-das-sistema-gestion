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
    public partial class FrmConsultaProductos : Form
    {
        private ProductoController _productoController;
        private CategoriaController _categoriaController;
        private SucursalController _sucursalController;
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
            else
            {
                UpdateProductos();
                UpdateCategorias();
                UpdateSucursales();
            }
        }
        private void ClearFields()
        {
            txtNombreCodigo.Clear();
            cbCategoria.SelectedItem = null;
            cbSucursal.SelectedItem = null;
        }
        private void UpdateCategorias()
        {
            cbCategoria.Items.Clear();
            _categoriaController.ListarCategorias().ForEach(categoria =>
            {
                if (!cbCategoria.Items.Contains(categoria.Nombre))
                {
                    cbCategoria.Items.Add(categoria.Nombre);
                }
            });
        }
        private void UpdateSucursales()
        {
            cbSucursal.Items.Clear();
            _sucursalController.ListarSucursales().ForEach(sucursal =>
            {
                cbSucursal.Items.Add($"[{sucursal.Id}] {sucursal.Nombre}");
            });
        }
        private void UpdateProductos()
        {
            dgvProductos.DataSource = null;
            List<Producto> productos = _productoController.ListarTodosLosProductos();

            var productosVencidos = new List<Producto>() /* Reemplazar con query real */;
            var productosProximosAVencer = new List<Producto>() /* Reemplazar con query real */;

            if (!string.IsNullOrWhiteSpace(txtNombreCodigo.Text))
            {
                productos = (from p in productos
                            where p.Nombre.ToLower().Contains(txtNombreCodigo.Text.ToLower().Trim()) ||
                                  p.Id.ToLower().Contains(txtNombreCodigo.Text.ToLower().Trim())
                                  select p).ToList();

            }
            if (cbCategoria.SelectedItem != null)
            {
                productos = (from p in productos
                            where p.Categoria == cbCategoria.SelectedItem.ToString()
                            select p).ToList();
            }

            // Filtro por sucursal
            if (cbSucursal.SelectedItem != null)
            {
                string sucursalId = ComboBoxHelper.ExtraerId(cbSucursal.SelectedItem.ToString());
                var productosSucursal = _productoController.ListarProductosPorSucursal(sucursalId);
                var idsSucursal = productosSucursal.Select(p => p.Id).ToHashSet();
                productos = productos.Where(p => idsSucursal.Contains(p.Id)).ToList();
            }

            // Obtener mapa de sucursales por producto
            var mapaSucursales = _productoController.ObtenerMapaSucursalesPorProducto();

            var productosGrid = productos.Select(p => new
            {
                p.Id,
                p.Nombre,
                p.Descripcion,
                p.PrecioUnitario,
                p.Categoria,
                Sucursal = mapaSucursales.ContainsKey(p.Id) ? mapaSucursales[p.Id] : "Sin sucursal",
                p.StockActual,
                FechaVencimiento = p.FechaVencimiento?.ToShortDateString() ?? "Sin vencimiento",
                EstadoVencimiento = productosVencidos.Any(pv => pv.Id == p.Id) ? "Vencido" :
                           productosProximosAVencer.Any(ppv => ppv.Id == p.Id) ? "Próximo a vencer" : "En fecha",
                p.Activo
            }).ToList();

            dgvProductos.DataSource = productosGrid;
            if (dgvProductos.DataSource != null)
            {
                dgvProductos.Columns["Id"].HeaderText = "ID";
                dgvProductos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvProductos.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                dgvProductos.Columns["Sucursal"].HeaderText = "Sucursal";
                dgvProductos.Columns["StockActual"].HeaderText = "Stock";
                dgvProductos.Columns["FechaVencimiento"].HeaderText = "Próximo Vencimiento";
                dgvProductos.Columns["EstadoVencimiento"].HeaderText = "Estado";
                dgvProductos.Columns["Activo"].HeaderText = "Activo";

                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    string estado = row.Cells["EstadoVencimiento"].Value?.ToString();
                    if (estado == "Vencido")
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    else if (estado == "Próximo a vencer")
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }
        public FrmConsultaProductos(TechStoreDbContext context)
        {
            InitializeComponent();
            this._productoController = new ProductoController(context);
            this._categoriaController = new CategoriaController(context);
            this._sucursalController = new SucursalController(context);
            UpdateCategorias();
            UpdateSucursales();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
            this.Close();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ClearFields();
            UpdateProductos();
        }

        private void FrmConsultaProductos_VisibleChanged(object sender, EventArgs e)
        {
            ClearFields();
            if (!this.Visible) return;
            Check();
        }

        private void txtNombreCodigo_TextChanged(object sender, EventArgs e)
        {
            UpdateProductos();
        }

        private void cbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProductos();
        }

        private void cbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProductos();
        }
    }
}
