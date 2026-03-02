using System;
using System.Windows.Forms;

namespace FINAL_POO_Sistema_de_Gestion.Forms
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        public void UpdateFields(int productos, int categorias)
        {
            lblEstadoDatos.Text = "Base de datos conectada";
            lblCargado.Visible = true;
            lblCargado.Text = $"Datos en BD:\n{productos} Productos\n{categorias} Categorias";
            lbNotificaciones.Items.Add("Bienvenido al Sistema de Gestion de Ventas - TechStore S.A.");
        }
        public void UpdateDashboard(int productos, int categorias, int proveedores, int clientes, int sucursales, int vendedores, int ventas, decimal saldoPendiente)
        {
            lblEstadoDatos.Text = "Base de datos conectada";
            lblCargado.Visible = true;
            lblCargado.Text =
                $"Productos: {productos}    |    Categorias: {categorias}    |    Proveedores: {proveedores}\n" +
                $"Clientes: {clientes}    |    Sucursales: {sucursales}    |    Vendedores: {vendedores}\n" +
                $"Ventas registradas: {ventas}\n" +
                $"Saldo pendiente total (Ctas. Ctes.): {saldoPendiente:C2}";
        }
        public void AgregarNotificacion(string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AgregarNotificacion), mensaje);
                return;
            }
            lbNotificaciones.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {mensaje}");
        }
        public void UpdateFieldsError()
        {
            lblEstadoDatos.Text = "Sin conexion a BD";
            lblCargado.Visible = false;
            lbNotificaciones.Items.Add("Bienvenido al Sistema de Gestion de Ventas - TechStore S.A.");
        }
        public void UpdateFecha(DateTime fecha)
        {
            lblFechaActual.Text = $"Fecha: {fecha.ToString("dd/MM/yyyy")}";
        }
    }
}
