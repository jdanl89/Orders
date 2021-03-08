// <copyright file="IOrderStore.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Stores
{
    using System.Collections.Generic;
    using Orders.Models;
    using Orders.Services.Options;

    /// <summary>
    /// The interface for the Order Store class.
    /// </summary>
    public interface IOrderStore
    {
        /// <summary>
        /// Adds an order.
        /// </summary>
        /// <param name="order">The order to be added.</param>
        void AddOrder(Order order);

        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to be retrieved.</param>
        /// <returns>The order.</returns>
        Order GetOrder(int orderId);

        /// <summary>
        /// Gets a list of orders.
        /// </summary>
        /// <param name="options">The optional parameters for filtering the results.</param>
        /// <returns>The filtered list of orders.</returns>
        List<Order> GetOrders(GetOrdersOptions options);
    }
}