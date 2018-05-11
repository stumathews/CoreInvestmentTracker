using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents an activity that that has been performed and persisted
    /// </summary>
    public class RecordedActivity: IReferToAnEntity
    {
        public RecordedActivity(User user, string tag, string details, DateTimeOffset atTime, int owningEntityId, EntityType owningEntityType)
        {
            User = user;
            Tag = tag;
            Details = details;
            AtTime = atTime;
            OwningEntityId = owningEntityId;
            OwningEntityType = owningEntityType;
        }

        /// <summary>
        /// The Id of the activity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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

        /// <summary>
        /// Time at which the Activity was logged
        /// </summary>
        public DateTimeOffset AtTime { get; set; }

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
