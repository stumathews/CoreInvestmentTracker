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
        IQueryable<T> Entities(bool withChildren = true);

        /// <summary>
        /// Expose our application db context
        /// </summary>
        ApplicationDbContext db { get; }

        /// <summary>
        /// The ability to save <see cref="Entities"/> 
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Async save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();

        /// <summary>
        /// Database
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Get type of entities
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        DbSet<T1> GetEntityByType<T1>() where T1 : class;
    }
}