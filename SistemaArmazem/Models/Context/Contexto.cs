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
            modelBuilder.Conventions.Remove(new PluralizingEntitySetNameConvention());
            modelBuilder.Entity<Cliente>().HasKey(x => x.clienteId);
            modelBuilder.Entity<Classe>().HasKey(x => x.classeId);

            modelBuilder.Entity<SubClasse>().HasKey(x => x.subclasseId);//primary key
            modelBuilder.Entity<SubClasse>().HasRequired(x => x.classe);//foreign key

            modelBuilder.Entity<Armazenagem>().HasKey(x => x.armazenagemId);//primary key

            modelBuilder.Entity<Armazem>().HasKey(x => x.armazemId);//primary key
            modelBuilder.Entity<Armazem>().HasRequired(x => x.cliente);//foreign key
            modelBuilder.Entity<Armazem>().HasRequired(x => x.tamanhoArmazem);//foreigh key

            modelBuilder.Entity<Pedido>().HasKey(x => x.pedidoId);//primary key
            modelBuilder.Entity<Pedido>().HasRequired(x => x.armazem).WithMany().WillCascadeOnDelete(false);//foreigh key
            modelBuilder.Entity<Pedido>().HasRequired(x => x.armazenagem).WithMany().WillCascadeOnDelete(false);//foreigh key
            modelBuilder.Entity<Pedido>().HasRequired(x => x.classe).WithMany().WillCascadeOnDelete(false);//foreigh key
            modelBuilder.Entity<Pedido>().HasRequired(x => x.cliente).WithMany().WillCascadeOnDelete(false);//foreigh key
            modelBuilder.Entity<Pedido>().HasRequired(x => x.subclasse).WithMany().WillCascadeOnDelete(false);//foreigh key

            modelBuilder.Entity<TamanhoArmazem>().HasKey(x => x.tamanhoArmazemId);

            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar")
                    .HasMaxLength(50));

            base.OnModelCreating(modelBuilder);
        }
    }
}