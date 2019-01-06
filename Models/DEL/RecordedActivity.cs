using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents an activity that that has been performed and persisted
    /// </summary>
    public class RecordedActivity: DbEntityBase, IReferToAnEntity
    {
        /// <summary>
        /// Recorded Activity
        /// </summary>
        /// <param name="Name">Name of operation</param>
        /// <param name="description"></param>
        /// <param name="user"></param>
        /// <param name="tag"></param>
        /// <param name="details"></param>
        /// <param name="atTime"></param>
        /// <param name="owningEntityId"></param>
        /// <param name="owningEntityType"></param>
        public RecordedActivity(string Name, string description, User user, string tag, string details, DateTimeOffset atTime, int owningEntityId, EntityType owningEntityType)
        {
            this.Name = Name;
            Description = description;
            User = user;
            Tag = tag;
            Details = details;
            CreatedTime = atTime;
            LastModifiedTime = atTime;
            OwningEntityId = owningEntityId;
            OwningEntityType = owningEntityType;
        }

        /// <summary>
        /// Ctor required for being used as a generic type
        /// </summary>
        public RecordedActivity() { }
        
        /// <summary>
        /// The user that performed the activity
        /// </summary>
        public User User { get;set; }

        /// <summary>
        /// A way to meta tag for custom info
        /// </summary>
        public string Tag { get;set; }

        /// <summary>
        /// Details of the activity
        /// </summary>
        public string Details { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// The activity target. The entity itself that this activity pertains to
        /// </summary>
        public int OwningEntityId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The type of a the target of this activity
        /// </summary>
        public EntityType OwningEntityType { get; set; }
        
    }
}
