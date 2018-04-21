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
    /// <typeparam name="T"></typeparam>
    public class EntityApplicationDbContext<T> : IEntityApplicationDbContext<T> where T : class, IDbInvestmentEntity
    {
        private readonly ApplicationDbContext _db;
       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EntityApplicationDbContext(ApplicationDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// ApplicationDbContext
        /// </summary>
        public ApplicationDbContext db => _db;

        private static bool IsAnyOfTypes<T1>(Type[] types)
        {
            foreach( Type t in types)
            {
                return (typeof(T1) == typeof(T1)) ? true: false;
            }
            return false;
        }

        /// <summary>
        /// Read only access to the type Entities
        /// </summary>
        public virtual IQueryable<T> Entities(bool withChildren = true)
        {
            
                /*
                 * We want to eager collect some of the members but we dont know what type we are when we 
                 * are dealing with generics as we are here. So this will determine the type,
                 * add the entity framework include() statements and return the generic type(set) again.
                 * This way we dont have to do any include() statements in the controllers what are 
                 * using the generic entity controller base class. This keeps the functionality in one place: here.
                 */
                Type[] types = { typeof(InvestmentRisk),
                                 typeof(InvestmentGroup),
                                 typeof(InvestmentInfluenceFactor),
                                 typeof(Region),
                                 typeof(Investment) };

                if (IsAnyOfTypes<T>(types))
                {
                    var filtered = new List<T>();
                    if (typeof(T) == typeof(InvestmentRisk))
                    {
                        filtered.AddRange(withChildren
                             
                            ? _db.Set<InvestmentRisk>()
                                 .Include(x => x.Investments)
                                 .Select(o => ChangeType<T>(o))
                                 .ToList()
                            : _db.Set<InvestmentRisk>()
                                 .Select(o => ChangeType<T>(o))
                                 .ToList());
                    }
                    if (typeof(T) == typeof(InvestmentGroup))
                    {
                        filtered.AddRange(withChildren 
                            ? _db.Set<InvestmentGroup>()
                                .Include(x => x.Children)
                                .ThenInclude(x => x.Parent)
                                .Include(x => x.Investments)
                                .Select(o => ChangeType<T>(o))
                                .ToList() 
                            : _db.Set<InvestmentGroup>().Select(o => ChangeType<T>(o)).ToList());
                        
                    }
                    if (typeof(T) == typeof(InvestmentInfluenceFactor))
                    {
                        filtered.AddRange(withChildren 
                            ? _db.Set<InvestmentInfluenceFactor>()
                                .Include(x => x.Investments)
                                .Select(o => ChangeType<T>(o))
                                .ToList()
                            : _db.Set<InvestmentInfluenceFactor>()
                                .Select(o => ChangeType<T>(o))
                                .ToList());                        
                    }
                    if (typeof(T) == typeof(Region))
                    {
                        filtered.AddRange(withChildren 
                            ? _db.Set<Region>()
                                .Include(x => x.Investments)
                                .Select(o => ChangeType<T>(o))
                                .ToList()
                            : _db.Set<Region>()
                                .Select(o => ChangeType<T>(o))
                                .ToList());                        
                    }
                    if (typeof(T) == typeof(Investment))
                    {
                        filtered.AddRange(withChildren 
                            ? _db.Set<Investment>().Include(a => a.Risks)
                                .Include(b => b.Factors)
                                .Include(c => c.Groups)
                                .Include(d => d.Regions)
                                .Select(o => ChangeType<T>(o)).ToList()
                            : _db.Set<Investment>()
                                .Select(o => ChangeType<T>(o))
                                .ToList());                        
                    }
                    var t = _db.Set<T>();                    
                    t.AddRange(filtered);
                    return t; 
                }

                // Return the entity type so no eager loading applied to T
                return _db.Set<T>();
            
        }

        /// <summary>
        /// Underlying database
        /// </summary>
        public virtual DatabaseFacade Database => _db.Database;

        /// <summary>
        /// Get specific type entities other than the current entity type
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public virtual DbSet<T1> GetEntityByType<T1>() where T1 : class => _db.Set<T1>();

        /// <summary>
        /// Save the db changes
        /// </summary>
        public virtual void SaveChanges() => _db.SaveChanges();

        /// <summary>
        /// Save the db changes asynchonously
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync() => await _db.SaveChangesAsync();              

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose() => _db.Dispose();

        /// <summary>
        /// Utility function to change the type
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static T1 ChangeType<T1>(object obj)
        {
            return (T1)Convert.ChangeType(obj, typeof(T1));
        }
    }
}