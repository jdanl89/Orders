// <copyright file="GetOrdersOptions.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Services.Options
{
    /// <summary>
    /// Optional parameters for filtering the results of the <see cref="OrderService.GetOrders"/> method.
    /// </summary>
    public class GetOrdersOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include canceled orders.
        /// </summary>
        public bool IncludeCanceled { get; set; } = true;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        public string SearchText { get; set; }
    }
}