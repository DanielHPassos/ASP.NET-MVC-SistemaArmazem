using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SistemaArmazem.Models.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SistemaArmazem.Models.Context
{
    public class Contexto : DbContext
    {
        public Contexto():base("SistemaArmazem")
        {

        }

        public DbSet<Cliente>       tbCliente       { get; set; }
        public DbSet<Classe>        tbClasse        { get; set; }
        public DbSet<SubClasse>     tbSubClasse     { get; set; }
        public DbSet<Armazenagem>   tbArmazenagem   { get; set; }
        public DbSet<Armazem>       tbArmazem       { get; set; }
        public DbSet<Pedido>        tbPedido        { get; set; }
        public DbSet<TamanhoArmazem> tbTamanhoArmazem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            modelBuilder.Entity<Cliente>().HasKey(x => x.clienteId);
            modelBuilder.Entity<Classe>().HasKey(x => x.classeId);
            modelBuilder.Entity<SubClasse>().HasKey(x => x.subclasseId);
            modelBuilder.Entity<Armazenagem>().HasKey(x => x.armazenagemId);
            modelBuilder.Entity<Armazem>().HasKey(x => x.armazemId);
            modelBuilder.Entity<Pedido>().HasKey(x => x.pedidoId);
            modelBuilder.Entity<TamanhoArmazem>().HasKey(x => x.tamanhoArmazemId);

            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar")
                    .HasMaxLength(50));

            base.OnModelCreating(modelBuilder);
        }
    }
}