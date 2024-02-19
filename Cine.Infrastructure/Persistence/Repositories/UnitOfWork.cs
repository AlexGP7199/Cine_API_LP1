using Cine.Infrastructure.Persistence.Interfaces;
using Cine.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CineDbContext _dbContext;
        public IMovieRepository movie { get; private set; }

        public UnitOfWork(CineDbContext dbContext)
        {
            _dbContext = dbContext;
            movie = new MovieRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsyncs()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
