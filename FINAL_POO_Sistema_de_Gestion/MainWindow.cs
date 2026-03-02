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
using FINAL_POO_Sistema_de_Gestion.Forms;
using FINAL_POO_Sistema_de_Gestion.Forms.Clientes;
using FINAL_POO_Sistema_de_Gestion.Controllers;
using FINAL_POO_Sistema_de_Gestion.Data;

namespace FINAL_POO_Sistema_de_Gestion
{
    public partial class FrmMainWindow : Form
    {
        public FrmAbout aboutForm;
        public TechStoreDbContext _context;
        private Form lastWindow;
        private Welcome welcomeForm;
        private ServicioCheckeoVencimientos _servicioCheckeoVencimientos;
        public DateTime FechaActual { get; set; } = DateTime.Now;
        public FrmMainWindow()
        {
            InitializeComponent();
            _context = new TechStoreDbContext();
            welcomeForm = new Welcome();
            try
            {
                var productoController = new ProductoController(_context);
                var stockController = new StockController(_context);
                _servicioCheckeoVencimientos = new ServicioCheckeoVencimientos(
                    stockController, productoController, () => FechaActual);
                _servicioCheckeoVencimientos.ProductosVencidosProcesados += OnProductosVencidos;

                RefreshDashboard();
                welcomeForm.AgregarNotificacion("Bienvenido al Sistema de Gestion de Ventas - TechStore S.A.");
            }
            catch
            {
                welcomeForm.UpdateFieldsError();
            }
            welcomeForm.MdiParent = this;
            welcomeForm.FormBorderStyle = FormBorderStyle.None;
            welcomeForm.Dock = DockStyle.Fill;
            welcomeForm.UpdateFecha(FechaActual);
            welcomeForm.Show();
        }
        private void RefreshDashboard()
        {
            try
            {
                var prodCtrl = new ProductoController(_context);
                var catCtrl = new CategoriaController(_context);
                var provCtrl = new ProveedorController(_context);
                var cliCtrl = new ClienteController(_context);
                var sucCtrl = new SucursalController(_context);
                var venCtrl = new VendedorController(_context);
                var vtaCtrl = new VentaController(_context);
                var ccCtrl = new CuentaCorrienteController(_context);

                var resumenCC = ccCtrl.ObtenerResumenGeneral();
                decimal saldoPendiente = resumenCC.Where(r => r.Saldo > 0).Sum(r => r.Saldo);

                welcomeForm.UpdateDashboard(
                    prodCtrl.ListarTodosLosProductos().Count,
                    catCtrl.ListarCategorias().Count,
                    provCtrl.ListarProveedores().Count,
                    cliCtrl.ListarClientes().Count,
                    sucCtrl.ListarSucursales().Count,
                    venCtrl.ListarVendedores().Count,
                    vtaCtrl.ListarVentas().Count,
                    saldoPendiente
                );
            }
            catch { }
        }

        private void MenuStripHelper<T>() where T : Form
        {
            try
            {
                openChildForm<T>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void openChildForm<T>() where T : Form
        {
            Form exists = Application.OpenForms
                .OfType<T>()
                .FirstOrDefault();

            if (exists == null)
            {
                T form = (T)Activator.CreateInstance(typeof(T), _context);
                lastWindow?.Hide();
                lastWindow = form;
                form.MdiParent = this;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.VisibleChanged += ChildForm_VisibleChanged;
                form.Show();
            }
            else
            {
                exists.BringToFront();
            }
        }

        private void ChildForm_VisibleChanged(object sender, EventArgs e)
        {
            if (sender is Form f && !f.Visible)
            {
                RefreshDashboard();
            }
        }

        private void altaDeToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaProducto>(); }
        private void modificacionDeProductoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarProducto>(); } 
        private void bajaDeProductoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaProducto>(); }
        private void darDeAltaToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaCategoria>(); }
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaProductos>(); }
        private void modificacionToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarCategoria>(); }
        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaCategoria>(); }
        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaCategorias>(); }
        private void consultarProveedoresToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaProveedor>(); }
        private void darDeAltaProveedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaProveedor>(); }
        private void modificarProveedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarProveedor>(); }
        private void darDeBajaProveedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaProveedor>(); }
        private void tiempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSimTiempo simTiempo = new FrmSimTiempo();
            simTiempo.ShowDialog();
            if (simTiempo.DialogResult == DialogResult.OK)
            {
                FechaActual = simTiempo.FechaSeleccionada;
                welcomeForm.UpdateFecha(FechaActual);
                MessageBox.Show($"Fecha simulada a: {FechaActual.ToShortDateString()}", "Fecha", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ejecutar chequeo de vencimientos inmediatamente con la nueva fecha
                _servicioCheckeoVencimientos?.Check();
            }
        }

        private void OnProductosVencidos(List<string> nombresProductos)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<List<string>>(OnProductosVencidos), nombresProductos);
                return;
            }

            foreach (var nombre in nombresProductos)
            {
                welcomeForm.AgregarNotificacion($"VENCIDO: Producto \"{nombre}\" - Se registro merma y se desconto del stock.");
            }

            RefreshDashboard();

            string lista = string.Join("\n- ", nombresProductos);
            MessageBox.Show(
                $"Se procesaron {nombresProductos.Count} producto(s) vencido(s):\n- {lista}\n\nSe registro merma y se actualizo el stock.",
                "Productos Vencidos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void ingresarStockToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmIngresoStock>(); }

        private void registrarEgresoStockToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmEgresoStock>(); }

        private void consultarMovimientosToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaMovimientosStock>(); }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm == null) {
                aboutForm = new FrmAbout();
            }
            aboutForm.ShowDialog();
        }

        private void stockActualPorProductoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmStockProductos>(); }

        private void stockPorCategoriaToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmStockPorCategoria>(); }

        private void movimientosPorProveedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmMovimientosPorProveedor>(); }

        private void productosConBajoStockToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmProductosBajoStock>(); }

        private void historialDeMovimientosDeUnProductoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmMovimientosPorProducto>(); }

        // Clientes
        private void consultarClientesToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaClientes>(); }
        private void altaClienteToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaCliente>(); }
        private void modificarClienteToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarCliente>(); }
        private void bajaClienteToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaCliente>(); }
        private void estadoCuentaCorrienteToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmEstadoCuentaCorriente>(); }
        // Sucursales
        private void consultarSucursalesToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaSucursales>(); }
        private void altaSucursalToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaSucursal>(); }
        private void modificarSucursalToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarSucursal>(); }
        private void bajaSucursalToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaSucursal>(); }
        // Vendedores
        private void consultarVendedoresToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaVendedores>(); }
        private void altaVendedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmAltaVendedor>(); }
        private void modificarVendedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmModificarVendedor>(); }
        private void bajaVendedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmBajaVendedor>(); }
        // Ventas
        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmRegistrarVenta>(); }
        private void consultarVentasToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmConsultaVentas>(); }
        // Reportes nuevos
        private void ventasPorPeriodoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmVentasPorPeriodo>(); }
        private void productosMasVendidosToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmProductosMasVendidos>(); }
        private void ventasPorProductoToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmVentasPorProducto>(); }
        private void ventasPorSucursalToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmVentasPorSucursal>(); }
        private void ventasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e) { MenuStripHelper<FrmVentasPorVendedor>(); }
    }
}
