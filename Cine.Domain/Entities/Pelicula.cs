using System;
using System.Collections.Generic;

namespace Cine.Domain.Entities;

public partial class Pelicula : BaseEntity
{
    //public int PeliculaId { get; set; }

    public string? Titulo { get; set; }

    public int? Duracion { get; set; }

    public string? Director { get; set; }

    public string? Genero { get; set; }

    public string? Clasificacion { get; set; }

    public DateTime? FechaEstreno { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public int? StateInCinema { get; set; }
}
