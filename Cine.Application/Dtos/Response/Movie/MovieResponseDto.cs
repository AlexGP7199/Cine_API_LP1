using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Dtos.Response.Movie
{
    public class MovieResponseDto
    {
        public int PeliculaId { get; set; }

        public string? Titulo { get; set; }

        public DateTime? FechaEstreno { get; set; }

        public string? Descripcion { get; set; }
        public int? StateInCinema { get; set; }
        public string? StateMovieDescription { get; set; }
    }
}
