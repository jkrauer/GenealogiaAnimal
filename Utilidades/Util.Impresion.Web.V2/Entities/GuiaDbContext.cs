﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Util.Impresion.Web.V2.Entities {
    public class GuiaDbContext : DbContext {
        public virtual DbSet<Guias> Guias { get; set; }
        public virtual DbSet<GuiasDet> GuiasDet { get; set; }
        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<ProveeClientes> ProveeClientes { get; set; }

        public GuiaDbContext(DbContextOptions opciones) : base(opciones) {
            
        }
        public GuiaDbContext() {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Guia;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Guias>(entity => {
                entity.HasKey(e => e.GuiaId)
                    .HasName("Key1");

                entity.ToTable("Guias", "guias");

                entity.Property(e => e.GuiaId).HasColumnName("GuiaID");

                entity.Property(e => e.Fecha).HasColumnType("date");
            });

            modelBuilder.Entity<GuiasDet>(entity => {
                entity.HasKey(e => e.GuiaDetId)
                    .HasName("Pk_GuiasDet");

                entity.ToTable("GuiasDet", "guias");

                entity.HasIndex(e => e.ClienteId)
                    .HasName("IX_GuasDet_ClienteID");

                entity.HasIndex(e => e.GuiaId)
                    .HasName("IX_GuiasDet_GuiaID");

                entity.HasIndex(e => e.ImagenId)
                    .HasName("IX_GuiasDet_ImagenID");

                entity.Property(e => e.GuiaDetId).HasColumnName("GuiaDetID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.GuiaId).HasColumnName("GuiaID");

                entity.Property(e => e.ImagenId).HasColumnName("ImagenID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.GuiasDet)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("Fk_GuiasDet-ProveeClientes");

                entity.HasOne(d => d.Guia)
                    .WithMany(p => p.GuiasDet)
                    .HasForeignKey(d => d.GuiaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Fk_Guias-GuiasDet");

                entity.HasOne(d => d.Imagen)
                    .WithMany(p => p.GuiasDet)
                    .HasForeignKey(d => d.ImagenId)
                    .HasConstraintName("fk_GuiasDet-Imagenes");
            });

            modelBuilder.Entity<Imagenes>(entity => {
                entity.HasKey(e => e.ImagenId)
                    .HasName("pk_Imagenes");

                entity.ToTable("Imagenes", "guias");

                entity.HasIndex(e => e.ProveedorId)
                    .HasName("IX_Imagenes_ProveedorID");

                entity.Property(e => e.ImagenId)
                    .HasColumnName("ImagenID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Imagenes)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Fk_Imagenes-ProveeClientes");
            });

            modelBuilder.Entity<ProveeClientes>(entity => {
                entity.HasKey(e => e.ProveeClienteId)
                    .HasName("pk_Estancias");

                entity.ToTable("ProveeClientes", "guias");

                entity.Property(e => e.ProveeClienteId)
                    .HasColumnName("ProveeClienteID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClienteSn).HasDefaultValueSql("0");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ProveedorSn).HasDefaultValueSql("0");
            });
        }

    }
}
