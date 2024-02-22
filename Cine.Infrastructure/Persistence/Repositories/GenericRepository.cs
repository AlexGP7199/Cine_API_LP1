using Cine.Domain.Entities;
using Cine.Infrastructure.Commons.Bases.Request;
using Cine.Infrastructure.Helpers;
using Cine.Infrastructure.Persistence.Context;
using Cine.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Cine.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CineDbContext _appDbContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(CineDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entity = _appDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity.AsNoTracking().ToListAsync();
            // Aplicar condicional
            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.AsNoTracking().FirstOrDefaultAsync(x=> x.Id.Equals(id));
            return getById;
        }

        public async Task<bool> RegisterAsync(T entity)
        {
            await _appDbContext.AddAsync(entity);
            var recordsAffected = await _appDbContext.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> EditAsync(T entity)
        {
            _appDbContext.Update(entity);
            // Con esto podemos evitar la actualizacion de algunos campos;
            //_dbConext.Entry(movie).Property(x => x.Director).IsModified = false ;
            var recordAffected = await _appDbContext.SaveChangesAsync();
            return recordAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            _appDbContext.Remove(entity);

            var recordsAffeced = await _appDbContext.SaveChangesAsync();
            return recordsAffeced > 0;
        }

        public IQueryable<T> GetEntryQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

            
        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination)
            {
                queryDto = queryDto.Paginate(request);
            }
            return queryDto;
        }

    }
}
