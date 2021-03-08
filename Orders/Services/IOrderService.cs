// <copyright file="IOrderService.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Services
{
    using System.Collections.Generic;
    using Orders.DTOs;
    using Orders.Services.Options;

    /// <summary>
    /// The interface for the Order Service class.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="orderId">The ID of the order to be canceled.</param>
        void CancelOrder(int orderId);

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="model">The order to be created.</param>
        /// <returns>The details of the order created.</returns>
        OrderDetailDto CreateOrder(OrderCreateDto model);

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="options">The options for filtering the results.</param>
        /// <returns>The filtered list of orders.</returns>
        List<OrderListDto> GetOrders(GetOrdersOptions options);

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="model">The order to be updated.</param>
        /// <returns>The details of the updated order.</returns>
        OrderDetailDto UpdateOrder(OrderUpdateDto model);
    }
}