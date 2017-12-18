using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreInvestmentTracker.Models.DAL.Interfaces
{
    /// <summary>
    /// Provides specific entity type services
    /// </summary>
    /// <typeparam name="T">The type of the underlying entity that this class will manage</typeparam>
    public interface IEntityApplicationDbContext<T> where T : class, IDbInvestmentEntity
    {
        /// <summary>
        /// The underlying entities that this class will expose
        /// </summary>
        IQueryable<T> Entities { get; }

        ApplicationDbContext db { get; }

        /// <summary>
        /// The ability to save <see cref="Entities"/> 
        /// </summary>
        void SaveChanges();

        Task SaveChangesAsync();

        void Dispose();

        DatabaseFacade Database { get; }

        DbSet<T1> GetEntityByType<T1>() where T1 : class;
    }
}