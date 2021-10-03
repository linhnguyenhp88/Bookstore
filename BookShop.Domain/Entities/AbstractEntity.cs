using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Entities
{
    public abstract class AbstractEntity
    {
        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public virtual byte[] Version { get; set; }
    }
}
