using System;
using System.Collections.Generic;
using Cine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cine.Infrastructure.Persistence.Context;

public partial class CineDbContext : DbContext
{
    public CineDbContext()
    {
    }

    public CineDbContext(DbContextOptions<CineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.PeliculaId).HasName("PK__Pelicula__5AC6F32CC1052ECB");

            entity.Property(e => e.PeliculaId).HasColumnName("PeliculaID");
            entity.Property(e => e.Clasificacion).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Director).HasMaxLength(255);
            entity.Property(e => e.FechaEstreno).HasColumnType("date");
            entity.Property(e => e.Genero).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
