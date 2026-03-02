using System;
using System.Data.Entity;
using FINAL_POO_Sistema_de_Gestion.Entidades;

namespace FINAL_POO_Sistema_de_Gestion.Data
{
    public class TechStoreDbContext : DbContext
    {
        public TechStoreDbContext() : base("name=TechStoreDB")
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<MovimientoStock> MovimientosStock { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<MovimientoCuentaCorriente> MovimientosCuentaCorriente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
