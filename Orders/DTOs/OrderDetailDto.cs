// <copyright file="OrderDetailDto.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.DTOs
{
    using System;
    using Orders.Models;

    /// <summary>
    /// The DTO for detailing an order.
    /// </summary>
    public class OrderDetailDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailDto"/> class.
        /// </summary>
        public OrderDetailDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailDto"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public OrderDetailDto(Order order)
        {
            this.OrderId = order.OrderId;
            this.Description = order.Description;
            this.CreatedOn = order.CreatedOn;
            this.LastModOn = order.LastModOn;
            this.Status = order.Status.ToString();
        }

        /// <summary>
        /// Gets or sets the datetime on which the order was created.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the datetime on which the order was last modified.
        /// </summary>
        public DateTimeOffset? LastModOn { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public string Status { get; set; }
    }
}