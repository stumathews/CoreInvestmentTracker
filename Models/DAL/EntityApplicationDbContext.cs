using System;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <summary>
    /// Class implementation that will expose the underlying entity framework entities without having to name the entity collection memeber
    /// on the dbcontext.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityApplicationDbContext<T> : IEntityApplicationDbContext<T> where T : class, IDbInvestmentEntity
    {
        private readonly ApplicationDbContext _db;

        public EntityApplicationDbContext(ApplicationDbContext context)
        {
            _db = context;
        }

        public ApplicationDbContext db => _db;
        public virtual DbSet<T> Entities => _db.Set<T>();

        public virtual DatabaseFacade Database => _db.Database;

        public virtual DbSet<T1> GetEntityByType<T1>() where T1 : class => _db.Set<T1>();

        public virtual void SaveChanges() => _db.SaveChanges();

        public virtual async Task SaveChangesAsync() => await _db.SaveChangesAsync();              

        public virtual void Dispose() => _db.Dispose();
    }
}