using System;
using System.Collections.Generic;
using LabReserva.Model;
using Microsoft.EntityFrameworkCore;

namespace LabReserva.Data;

public partial class ReservaLabContext : DbContext
{
    public ReservaLabContext()
    {
    }

    public ReservaLabContext(DbContextOptions<ReservaLabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Laboratorio> TbLaboratorios { get; set; }

    public virtual DbSet<Reserva> TbReservas { get; set; }

    public virtual DbSet<TipoUsuario> TbTipoUsuarios { get; set; }

    public virtual DbSet<Usuario> TbUsuarios { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorio).HasName("PK__tb_labor__781B42E2DDF37099");

            entity.ToTable("tb_laboratorio");

            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");
            entity.Property(e => e.AndarLaboratorio).HasColumnName("andar_laboratorio");
            entity.Property(e => e.DescricaoLaboratorio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao_laboratorio");
            entity.Property(e => e.IsActivate).HasColumnName("is_activate");
            entity.Property(e => e.NomeLaboratorio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nome_laboratorio");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__tb_reser__423CBE5D5894399F");

            entity.ToTable("tb_reserva");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.DiaHorarioReserva)
                .HasColumnType("smalldatetime")
                .HasColumnName("dia_horario_reserva");
            entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdLaboratorioNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.IdLaboratorio)
                .HasConstraintName("FK_LAB");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_USER");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__tb_tipo___B17D78C874024819");

            entity.ToTable("tb_tipo_usuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
            entity.Property(e => e.Descricao)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__tb_usuar__4E3E04ADC636ECA8");

            entity.ToTable("tb_usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.CpfCnpjUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cpf_cnpj_usuario");
            entity.Property(e => e.EmailUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_usuario");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
            entity.Property(e => e.IsActivate).HasColumnName("is_activate");
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_usuario");
            entity.Property(e => e.SenhaUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senha_usuario");
            entity.Property(e => e.TelefoneUsuario)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("telefone_usuario");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.TbUsuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .HasConstraintName("FK_USER_TYPE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
