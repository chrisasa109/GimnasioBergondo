using Gimnasio.Models;
using Microsoft.EntityFrameworkCore;

/*
 * Para generar las tablas de la base de datos:
 * 1. Escribir la cadena de conexión a base de datos en appsettings.json
 * 2. Apuntar hacia la base de datos en el archivo ejecutable Program.cs
 */

namespace Gimnasio.Dates
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //Agregar los modelos
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarifa> Tarifa { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<UsuarioActividad> UsuarioActividad { get; set; }

        /*
         * Este método se ejecuta al añadir la migración a la base de datos.
         * Su función es generar como clave primaria la unión de varias columnas
         * cuando la clave así lo requiera.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>().HasKey(c => new {c.Id, c.UsuarioId});
            modelBuilder.Entity<Contrato>().HasKey(c => new { c.Id, c.UsuarioId });
            modelBuilder.Entity<UsuarioActividad>().HasKey(c => new { c.Id, c.ActividadId, c.UsuarioId});
            modelBuilder.Entity<DetallePedido>().HasKey(c => new { c.Id, c.PedidoId, c.ProductoId});
            modelBuilder.Entity<Usuario>().HasIndex(p => new {p.Email}).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
