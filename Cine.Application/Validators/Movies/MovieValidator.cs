using Cine.Application.Dtos.Request.Movie;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Validators.Movies
{
    public class MovieValidator : AbstractValidator<MovieRequestDto>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Titulo)
                .NotNull().WithMessage("El campo Titulo no puede ser nulo")
                .NotEmpty().WithMessage("El campo Titulo no puede estar vacio");
        }
    }
}
