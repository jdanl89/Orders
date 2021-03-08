// <copyright file="BaseModel.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Models
{
    using System;

    /// <summary>
    /// The model for an order.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the datetime on which the object was created.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the datetime on which the object was last modified.
        /// </summary>
        public DateTimeOffset? LastModOn { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public Status Status { get; set; }
    }
}