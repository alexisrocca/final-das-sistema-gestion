using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FINAL_POO_Sistema_de_Gestion.Entidades;
using System.Windows.Forms;
using FINAL_POO_Sistema_de_Gestion.Forms;

namespace FINAL_POO_Sistema_de_Gestion.Repos
{
    public class RepositorioMaestro
    {
        public RepositorioNamedEntity<Categoria> Categorias { get; private set; }
        public RepositorioNamedEntity<Producto> Inventario { get; private set; }
        public RepositorioNamedEntity<Proveedor> Proveedores { get; private set; }
        public RepositorioBaseEntity<Lote> Stock { get; private set; }
        public RepositorioMovimientoStock MovimientosStock { get; private set; }

        private readonly string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datos.json");
        public bool DatosCargados { get; private set; } = false;
        public DateTime FechaActual { get; set; } = DateTime.Now;
        public RepositorioMaestro(Welcome frmInicio)
        {
            Categorias = new RepositorioNamedEntity<Categoria>(this);
            Inventario = new RepositorioNamedEntity<Producto>(this);
            Proveedores = new RepositorioNamedEntity<Proveedor>(this);
            Stock = new RepositorioBaseEntity<Lote>(this);
            MovimientosStock = new RepositorioMovimientoStock(this, frmInicio);
            CargarDatos();
        }

        public void GuardarDatos()
        {
            var datos = new
            {
                Categorias = Categorias.Listar(),
                Productos = Inventario.Listar(),
                Proveedores = Proveedores.Listar(),
                Stock = Stock.Listar(),
                MovimientosStock = MovimientosStock.Listar(),
            };
            File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(datos, Formatting.Indented));
        }

        public void CargarDatos()
        {
            if (!File.Exists(rutaArchivo)) return;
            var archivoJson = File.ReadAllText(rutaArchivo);
            var datos = JsonConvert.DeserializeAnonymousType(archivoJson, new
            {
                Categorias = new List<Categoria>(),
                Productos = new List<Producto>(),
                Proveedores = new List<Proveedor>(),
                Stock = new List<Lote>(),
                MovimientosStock = new List<MovimientoStock>()
            });
            if (datos != null)
            {
                try
                {
                    Categorias.Importar(datos.Categorias);
                    Inventario.Importar(datos.Productos);
                    Proveedores.Importar(datos.Proveedores);
                    Stock.Importar(datos.Stock);
                    if (datos.MovimientosStock != null)
                        MovimientosStock.Importar(datos.MovimientosStock);
                    DatosCargados = true;
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show($"Hubo un error al recuperar los datos\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
