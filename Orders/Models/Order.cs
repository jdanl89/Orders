// <copyright file="Order.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Models
{
    using System;
    using Orders.DTOs;

    /// <summary>
    /// The model for an order.
    /// </summary>
    public class Order : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="model">The model to be converted from DTO to data model.</param>
        public Order(OrderCreateDto model)
        {
            this.Description = model.Description;
            this.CreatedOn = DateTimeOffset.UtcNow;
            this.Status = Status.Active;
        }

        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public int? OrderId { get; set; }
    }
}