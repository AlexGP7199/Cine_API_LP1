using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaracion de nuestras interfaces a nivel de repository
        IMovieRepository movie { get; }


        void SaveChanges();

        Task SaveChangesAsyncs();
    }
}
