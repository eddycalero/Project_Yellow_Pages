using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using YellowPages.Models.ViewModel;

namespace YellowPages.Entities
{
    public partial class YellowPagesContext : DbContext
    {
        public YellowPagesContext()
        {
        }

        public YellowPagesContext(DbContextOptions<YellowPagesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactoEmpresa> ContactoEmpresas { get; set; } = null!;
        public virtual DbSet<DepartamentoEmpresa> DepartamentoEmpresas { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<OfertaEmpresa> OfertaEmpresas { get; set; } = null!;
        public virtual DbSet<AnuncioEmpresa> AnuncioEmpresa{ get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2AR822P\\SQLEXPRESS;Database=YellowPages;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactoEmpresa>(entity =>
            {
                entity.HasKey(e => e.ContactoId)
                    .HasName("PK__Contacto__8E0F85C89619C8D3");

                entity.ToTable("ContactoEmpresa", "Empresa");

                entity.Property(e => e.ContactoId)
                    .ValueGeneratedNever()
                    .HasColumnName("ContactoID");

                entity.Property(e => e.ContactoName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.ContactoEmpresas)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("FK__ContactoE__Empre__5629CD9C");
            });

            modelBuilder.Entity<DepartamentoEmpresa>(entity =>
            {
                entity.ToTable("DepartamentoEmpresa", "Empresa");

                entity.Property(e => e.DepartamentoEmpresaId)
                    .ValueGeneratedNever()
                    .HasColumnName("DepartamentoEmpresaID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa", "Empresa");

                entity.Property(e => e.EmpresaId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmpresaID");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DescripcionTwo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionWeb)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MunicipioId).HasColumnName("MunicipioID");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.MunicipioId)
                    .HasConstraintName("FK__Empresa__Municip__4D94879B");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.ToTable("Municipio", "Empresa");

                entity.Property(e => e.MunicipioId)
                    .ValueGeneratedNever()
                    .HasColumnName("MunicipioID");

                entity.Property(e => e.DepartamentoEmpresaId).HasColumnName("DepartamentoEmpresaID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.DepartamentoEmpresa)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.DepartamentoEmpresaId)
                    .HasConstraintName("FK__Municipio__Depar__3C69FB99");
            });

            modelBuilder.Entity<OfertaEmpresa>(entity =>
            {
                entity.ToTable("OfertaEmpresa", "Empresa");

                entity.Property(e => e.OfertaEmpresaId)
                    .ValueGeneratedNever()
                    .HasColumnName("OfertaEmpresaID");

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreOferta)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.OfertaEmpresas)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("FK__OfertaEmp__Empre__52593CB8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
