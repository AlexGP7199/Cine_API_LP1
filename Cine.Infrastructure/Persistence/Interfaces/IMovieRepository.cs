using Cine.Domain.Entities;
using Cine.Infrastructure.Commons.Bases.Request;
using Cine.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Infrastructure.Persistence.Interfaces
{
    public interface IMovieRepository
    {
        Task<BaseEntityResponse<Pelicula>> ListMovies(BaseFiltersRequest request);
        Task<IEnumerable<Pelicula>> ListSelectedMovies();
        Task<Pelicula> MovieByID(int id);
        Task<bool> AddMovie(Pelicula movie);
        Task<bool> EditMovie(Pelicula movie);
        Task<bool> RemoveMovie(int id);
        
    }
}
