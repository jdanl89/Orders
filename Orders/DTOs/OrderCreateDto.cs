// <copyright file="OrderCreateDto.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.DTOs
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The DTO for creating an order.
    /// </summary>
    public class OrderCreateDto
    {
        /// <summary>
        /// Gets or sets the description of the order.
        /// </summary>
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string Description { get; set; }
    }
}