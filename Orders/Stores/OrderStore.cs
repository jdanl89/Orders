// <copyright file="OrderStore.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Orders.Models;
    using Orders.Services.Options;

    /// <summary>
    /// The service level method for interacting with Orders.
    /// </summary>
    public class OrderStore : IOrderStore
    {
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <remarks>
        /// Normally, this would be retrieved from a database.
        /// </remarks>
        private List<Order> Orders { get; } = new();

        /// <summary>
        /// Adds an order.
        /// </summary>
        /// <param name="order">The order to be added.</param>
        public void AddOrder(Order order)
        {
            order.OrderId =
                this.Orders.Count == 0
                    ? 1
                    : this.Orders.Max(x => x.OrderId) + 1;

            this.Orders.Add(order);
        }

        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to be retrieved.</param>
        /// <returns>The order.</returns>
        public Order GetOrder(int orderId)
        {
            return this.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }

        /// <summary>
        /// Gets a list of orders.
        /// </summary>
        /// <param name="options">The optional parameters for filtering the results.</param>
        /// <returns>The filtered list of orders.</returns>
        public List<Order> GetOrders(GetOrdersOptions options)
        {
            int skip = (options.PageNumber - 1) * options.PageSize;
            int take = options.PageSize;

            if (string.IsNullOrWhiteSpace(options.SearchText))
            {
                return this.Orders
                    .OrderBy(o => o.OrderId)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }

            return this.Orders
                .Where(o => o.Description.Contains(options.SearchText, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(o => o.OrderId)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}