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
    /// <inheritdoc />
    /// <summary>
    /// Class implementation that will expose the underlying entity framework entities without having to name the entity collection memeber
    /// on the dbcontext.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EntityApplicationDbContext<T> : IEntityApplicationDbContext<T> where T : class, IDbInvestmentEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EntityApplicationDbContext(ApplicationDbContext context)
        {
            Db = context;
        }

        /// <inheritdoc />
        /// <summary>
        /// ApplicationDbContext
        /// </summary>
        public ApplicationDbContext Db { get; }

        private static bool IsAnyOfTypes<T1>(Type[] types)
        {
            return types.Select(t => t == typeof(T1)).FirstOrDefault();
        }

        /// <summary>
        /// Read only access to the type Entities
        /// </summary>
        public IQueryable<T> GetAllEntities(bool withChildren = true)
        {
            
                /*
                 * We want to eager collect some of the members but we dont know what type we are when we 
                 * are dealing with generics as we are here. So this will determine the type,
                 * add the entity framework include() statements and return the generic type(set) again.
                 * This way we dont have to do any include() statements in the controllers what are 
                 * using the generic entity controller base class. This keeps the functionality in one place: here.
                 */

            // We only want to do special handling of some types
            if (!IsAnyOfTypes<T>(new[] {
                typeof(InvestmentRisk),
                typeof(InvestmentGroup),
                typeof(InvestmentInfluenceFactor),
                typeof(Region),
                typeof(Investment)
            }))
            {
                return Db.Set<T>();
            }

            var entities = new List<T>();

            /* In most cases we want to go down further in the object graph to return more
               decendants than just the top level members of the entity. Here is where we do it
             */
            
            if (typeof(T) == typeof(InvestmentRisk))
            {
                entities.AddRange(withChildren
                    ? Db.Set<InvestmentRisk>()
                        .Include(x => x.Investments)
                        .Select(o => ChangeType<T>(o))
                        .ToList()
                    : Db.Set<InvestmentRisk>()
                        .Select(o => ChangeType<T>(o))
                        .ToList());
            }
            if (typeof(T) == typeof(InvestmentGroup))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<InvestmentGroup>()
                        .Include(x => x.Children)
                        .ThenInclude(x => x.Parent)
                        .Include(x => x.Investments)
                        .Select(o => ChangeType<T>(o))
                        .ToList() 
                    : Db.Set<InvestmentGroup>().Select(o => ChangeType<T>(o)).ToList());
                        
            }
            if (typeof(T) == typeof(InvestmentInfluenceFactor))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<InvestmentInfluenceFactor>()
                        .Include(x => x.Investments)
                        .Select(o => ChangeType<T>(o))
                        .ToList()
                    : Db.Set<InvestmentInfluenceFactor>()
                        .Select(o => ChangeType<T>(o))
                        .ToList());                        
            }
            if (typeof(T) == typeof(Region))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<Region>()
                        .Include(x => x.Investments)
                        .Select(o => ChangeType<T>(o))
                        .ToList()
                    : Db.Set<Region>()
                        .Select(o => ChangeType<T>(o))
                        .ToList());                        
            }
            if (typeof(T) == typeof(Investment))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<Investment>().Include(a => a.Risks)
                        .Include(b => b.Factors)
                        .Include(c => c.Groups)
                        .Include(d => d.Regions)
                        .Select(o => ChangeType<T>(o)).ToList()
                    : Db.Set<Investment>()
                        .Select(o => ChangeType<T>(o))
                        .ToList());                        
            }

            // Return the entity type so no eager loading applied to T
            var ret = Db.Set<T>();                    
                ret.AddRange(entities);
            return ret;
        }

        /// <summary>
        /// Underlying database
        /// </summary>
        public DatabaseFacade Database => Db.Database;

        /// <inheritdoc />
        /// <summary>
        /// Get specific type entities other than the current entity type
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public DbSet<T1> GetEntityByType<T1>() where T1 : class => Db.Set<T1>();

        /// <inheritdoc />
        /// <summary>
        /// Save the db changes
        /// </summary>
        public void SaveChanges() => Db.SaveChanges();
        
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