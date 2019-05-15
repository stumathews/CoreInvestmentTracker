using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
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
    public sealed class EntityApplicationDbContext<T> : IEntityApplicationDbContext<T> where T : class, IDbEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EntityApplicationDbContext(ApplicationDbContext context)
        {
            Db = context;
        }

        /// <summary>
        /// Read only access to the type Entities
        /// </summary>
        /// <param name="withChildren"></param>
        /// <param name="specificId"></param>
        /// <returns></returns>
        public IQueryable<T> GetOneOrAllEntities(bool withChildren = true, int? specificId = null)
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
                typeof(Investment),
                typeof(CustomEntity)
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
                        .Where(OneOrAll<InvestmentRisk>(specificId))
                        .Include(x => x.Investments)
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList()
                    : Db.Set<InvestmentRisk>()
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList());
            }
            if (typeof(T) == typeof(InvestmentGroup))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<InvestmentGroup>()
                        .Where(OneOrAll<InvestmentGroup>(specificId))
                        .Include(x => x.Children)
                        .ThenInclude(x => x.Parent)
                        .Include(x => x.Investments)
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList() 
                    : Db.Set<InvestmentGroup>().Select(o => Utils.ChangeType<T>(o)).ToList());
                        
            }
            if (typeof(T) == typeof(InvestmentInfluenceFactor))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<InvestmentInfluenceFactor>()
                        .Where(OneOrAll<InvestmentInfluenceFactor>(specificId))
                        .Include(x => x.Investments)
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList()
                    : Db.Set<InvestmentInfluenceFactor>()
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList());                        
            }
            if (typeof(T) == typeof(Region))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<Region>()
                        .Where(OneOrAll<Region>(specificId))
                        .Include(x => x.Investments)
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList()
                    : Db.Set<Region>()
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList());                        
            }
            if (typeof(T) == typeof(Investment))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<Investment>()
                        .Where(OneOrAll<Investment>(specificId))
                        .Include(a => a.Risks)
                        .Include(b => b.Factors)
                        .Include(c => c.Groups)
                        .Include(d => d.Regions)
                        .Include(e => e.Transactions)
                        .Select(o => Utils.ChangeType<T>(o)).ToList()
                    : Db.Set<Investment>()
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList());                        
            }

            if (typeof(T) == typeof(CustomEntity))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<CustomEntity>()
                        .Include(x=>x.Associations)
                        .Include(a=>a.OwningCustomEntity).Select(o=>Utils.ChangeType<T>(o))
                        .ToList()
                    : Db.Set<CustomEntity>()
                        .Select(o=> Utils.ChangeType<T>(o))
                        .ToList());
            }

            if (typeof(T) == typeof(InvestmentTransaction))
            {
                entities.AddRange(withChildren 
                    ? Db.Set<InvestmentTransaction>()
                        .Where(OneOrAll<InvestmentTransaction>(specificId))
                        .Select(o => Utils.ChangeType<T>(o))
                        .ToList() 
                    : Db.Set<InvestmentTransaction>().Select(o => Utils.ChangeType<T>(o)).ToList());
                        
            }

            // Return the entity type so no eager loading applied to T
            var ret = Db.Set<T>();                    
                ret.AddRange(entities);
            return ret;
        }

        /// <inheritdoc />
        /// <summary>
        /// ApplicationDbContext
        /// </summary>
        public ApplicationDbContext Db { get; }

        private static bool IsAnyOfTypes<T1>(IEnumerable<Type> types) => types.Any(type => type == typeof(T1));
        
        private static Expression<Func<T1, bool>> OneOrAll<T1>(int? specificId)
        where T1 : class, IInvestmentEntity => entity => !specificId.HasValue || entity.Id == specificId.Value;

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
    }
}