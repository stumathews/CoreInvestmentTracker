﻿using System;
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
        private bool IsAnyOfTypes<T>(Type[] types)
        {
            foreach( Type t in types)
            {
                return (typeof(T) == typeof(T)) ? true: false;
            }
            return false;
        }
        public virtual DbSet<T> Entities
        {
            get
            {
                /*
                 * We want to eager collect some of the members but we dont know what type we are when we 
                 * are dealing with generics as we are here. So this will determine the type,
                 * add the entity framework include() statements and return the generic type(set) again.
                 * This was we dont have to do any include() statements in the controllers what are 
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
                        filtered.AddRange(_db.Set<InvestmentRisk>().Include(x => x.Investments).Select(o=> ChangeType<T>(o)).ToList());                        
                    }
                    if (typeof(T) == typeof(InvestmentGroup))
                    {
                        filtered.AddRange(_db.Set<InvestmentGroup>().Include(x => x.Investments).Select(o => ChangeType<T>(o)).ToList());                        
                    }
                    if (typeof(T) == typeof(InvestmentInfluenceFactor))
                    {
                        filtered.AddRange(_db.Set<InvestmentInfluenceFactor>().Include(x => x.Investments).Select(o => ChangeType<T>(o)).ToList());                        
                    }
                    if (typeof(T) == typeof(Region))
                    {
                        filtered.AddRange(_db.Set<Region>().Include(x => x.Investments).Select(o => ChangeType<T>(o)).ToList());                        
                    }
                    if (typeof(T) == typeof(Investment))
                    {
                        filtered.AddRange(_db.Set<Investment>().Include(a => a.Risks).Include(b => b.Factors).Include(c => c.Groups).Include(d => d.Regions).Select(o => ChangeType<T>(o)).ToList());                        
                    }
                    var t = _db.Set<T>();
                    t.AddRange(filtered);
                    return t;
                }

                // Return the entity type so no eager loading applied to T
                return _db.Set<T>();
            }
        }

        public virtual DatabaseFacade Database => _db.Database;

        public virtual DbSet<T1> GetEntityByType<T1>() where T1 : class => _db.Set<T1>();

        public virtual void SaveChanges() => _db.SaveChanges();

        public virtual async Task SaveChangesAsync() => await _db.SaveChangesAsync();              

        public virtual void Dispose() => _db.Dispose();

        private T ChangeType<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}