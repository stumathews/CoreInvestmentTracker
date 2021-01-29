using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Models.DAL.Interfaces
{
    /// <summary>
    /// Provides specific entity type services
    /// </summary>
    /// <typeparam name="T">The type of the underlying entity that this class will manage</typeparam>
    public interface IEntityApplicationDbContext<T> where T : class
    {
        /// <summary>
        /// The underlying entities that this class will expose. Retrieves all children by default unless withChildren = false
        /// </summary>
        IQueryable<T> GetOneOrAllEntities(bool withChildren = true, int? specificId = null);
        
            /// <summary>
        /// Expose our application db context
        /// </summary>
        InvestmentDbContext Db { get; }

        /// <summary>
        /// The ability to save <see cref="GetOneOrAllEntities"/> 
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Get type of entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> GetEntityByType<T>() where T : class;
    }
}