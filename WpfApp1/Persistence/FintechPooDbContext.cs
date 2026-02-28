using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;


namespace WpfApp1.Persistence
{
    public class FintechPooDbContext: DbContext
    {
        public DbSet<Banco> Bancos { get; set; } = null!;
        public DbSet<Calendario> Calendarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;

        // Herança: Mapeia Conta e suas filhas (ContaCorrente e Poupanca)
        public DbSet<Conta> Contas { get; set; } = null!;

        public FintechPooDbContext() { }

        public FintechPooDbContext(DbContextOptions<FintechPooDbContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verifica se o banco JÁ foi configurado (pelo App.xaml.cs que pediu SQLite)
            if (!optionsBuilder.IsConfigured)
            {
                // Só configura o SQL Server se nenhum outro tiver sido informado antes (fallback)
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FintechPOO;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Mapeia a classe base 'Conta' como a raiz da hierarquia.
            modelBuilder.Entity<Conta>()
                // 2. Mapeia a classe derivada ContaCorrente.
                .HasDiscriminator<string>("TipoConta")
                .HasValue<ContaCorrente>("ContaCorrente")
                // 3. Mapeia a classe derivada Poupanca.
                .HasValue<Poupanca>("Poupanca");

            base.OnModelCreating(modelBuilder);
        }
    }
}
