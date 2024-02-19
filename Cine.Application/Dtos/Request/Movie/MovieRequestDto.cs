using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Dtos.Request.Movie
{
    public class MovieRequestDto
    {
        public string? Titulo { get; set; }

        public string? Director { get; set; }

        public string? Genero { get; set; }

        public string? Clasificacion { get; set; }

        public string? Descripcion { get; set; }
    }
}
