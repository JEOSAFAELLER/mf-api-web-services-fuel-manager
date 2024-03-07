using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Models
{
    public class AppDbContext:DbContext
    {
        //injeção de dependencia
        public AppDbContext (DbContextOptions options) : base(options) 
        {
        }
        //precisamos criar uma sobrescrita para evitar ciclos, definindo as Fk e os relacionamento
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //chave composta
            builder.Entity<VeiculoUsuarios>()
                  .HasKey(c => new { c.VeiculoId, c.UsuarioId });

            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Veiculo).WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.VeiculoId);
            
            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Usuario).WithMany(c => c.Veiculos)
                .HasForeignKey(c => c.UsuarioId);

        }


        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Consumo> Consumos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<VeiculoUsuarios> VeiculosUsuarios { get; set; }






    }
}
