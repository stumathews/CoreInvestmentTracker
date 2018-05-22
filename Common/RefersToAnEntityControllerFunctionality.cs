﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// A controller that gives IReferToAnEntity entities common specific functionality along with generic CRUD functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RefersToAnEntityControllerFunctionality<T> : BaseEntityControllerFunctionality<T> where T : class, IReferToAnEntity, new()
    {
        /// <inheritdoc />
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityRepository"></param>
        public RefersToAnEntityControllerFunctionality(IEntityApplicationDbContext<T> entityRepository) : base(entityRepository)
        {
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="owningEntityId"></param>
        /// <param name="owningEntityType"></param>
        /// <returns>item</returns>
        [HttpGet("{owningEntityId}/{owningEntityType}")]
        public async Task<IEnumerable<T>> GetAllEntitiesForOwner(int owningEntityId, int owningEntityType)
        {
            return await Task.Run(() =>
            {
                var type = GetUnderlyingEntityType<T>();
                return EntityRepository.GetOneOrAllEntities().Where(o => o.OwningEntityId == owningEntityId
                                                                  && o.OwningEntityType == (EntityType)owningEntityType).ToList();
                
            });
        }
        

        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <param name="owningEntityId">The id of the type you want the entity for</param>
        /// <param name="owningEntityType">The type of the owning entity</param>
        /// <returns>NoContentResult</returns>
        [HttpDelete("{owningEntityId}/{owningEntityType}/{id}")]
        public IActionResult Delete(int owningEntityId, int owningEntityType, int id)
        {
            var entity = EntityRepository.Db.Find<InvestmentNote>(owningEntityId, (EntityType)owningEntityType, id);
            if (entity == null) return NotFound();
            EntityRepository.Db.Remove(entity);

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Delete.ToString(), "Deletes an exisitng entity", GetUser(), "", $"Deleted entity with id of '{id}' owning entityId of '{owningEntityId}' and owning type of '{(EntityType)owningEntityType}'", DateTimeOffset.UtcNow, entity.Id, GetUnderlyingEntityType<T>()));
            EntityRepository.SaveChanges();
            return new NoContentResult();

        }
    }
}