// <copyright file="OrderUpdateDto.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.DTOs
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The DTO for updating an order.
    /// </summary>
    public class OrderUpdateDto
    {
        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public int? OrderId { get; set; }
    }
}