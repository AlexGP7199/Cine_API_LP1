using Cine.Domain.Entities;
using Cine.Infrastructure.Commons.Bases.Request;
using Cine.Infrastructure.Commons.Bases.Response;
using Cine.Infrastructure.Persistence.Interfaces;
using Cine.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Infrastructure.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Pelicula>, IMovieRepository
    {
       
        public MovieRepository(CineDbContext dbConext) : base(dbConext) { }
         
        public async Task<BaseEntityResponse<Pelicula>> ListMovies(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Pelicula>();

            //var movies = (from c in _dbConext.Peliculas select c).AsNoTracking().AsQueryable();

            var movies = GetEntryQuery();

            if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch(filters.NumFilter)
                {
                    case 1:
                        movies = movies.Where(x=> x.Titulo !.Contains(filters.TextFilter)); 
                        break;
                    case 2:
                        movies = movies.Where(x=> x.Descripcion !.Contains(filters.TextFilter));
                        break;
                }
            }

            
            if(filters.StateFilter is not null)
            {
                movies = movies.Where(x => x.Estado.Equals(filters.StateFilter));
            } 
            if(!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate)) 
            {
                movies = movies.Where(x => x.FechaEstreno >= Convert.ToDateTime(filters.StartDate) && x.FechaEstreno <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "PeliculaId";

            response.TotalRecords = await movies.CountAsync();
            response.Items = await Ordering(filters, movies, true).ToListAsync();
            return response;
        }

        /*
        public async Task<IEnumerable<Pelicula>> ListSelectedMovies()
        {
            var movies = await _dbConext.Peliculas.Where(x => x.FechaEstreno <= DateTime.Today).AsNoTracking().ToListAsync();
            return movies;
        }

        public async Task<Pelicula> MovieByID(int id)
        {
           var movie = await _dbConext.Peliculas.FirstOrDefaultAsync(x => x.Id.Equals(id));
           return movie!;
        }

        public async Task<bool> AddMovie(Pelicula movie)
        {
            await _dbConext.AddAsync(movie);
            var recordAffected = await _dbConext.SaveChangesAsync();
            return recordAffected > 0;
        }

        public async Task<bool> EditMovie(Pelicula movie)
        {
            _dbConext.Update(movie);
            // Con esto podemos evitar la actualizacion de algunos campos;
            //_dbConext.Entry(movie).Property(x => x.Director).IsModified = false ;
            var recordAffected = await _dbConext.SaveChangesAsync();
            return recordAffected > 0;
        }

        public async Task<bool> RemoveMovie(int id)
        {
            var movie = await _dbConext.Peliculas.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id));

            _dbConext.Remove(movie);
            
            var recordsAffeced = await _dbConext.SaveChangesAsync();
            return recordsAffeced > 0;
        }*/
    }
}
