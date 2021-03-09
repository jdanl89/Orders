// <copyright file="OrderListDto.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.DTOs
{
    using Orders.Models;

    /// <summary>
    /// The DTO for detailing an order.
    /// </summary>
    public class OrderListDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderListDto"/> class.
        /// </summary>
        public OrderListDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderListDto"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public OrderListDto(Order order)
        {
            this.OrderId = order.OrderId;
            this.Description = order.Description;
            this.Status = order.Status.ToString();
        }

        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        public string Status { get; set; }
    }
}