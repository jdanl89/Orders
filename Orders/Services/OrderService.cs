// <copyright file="OrderService.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Orders.DTOs;
    using Orders.Models;
    using Orders.Services.Options;
    using Orders.Stores;

    /// <summary>
    /// The service level method for interacting with Orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        /// <summary>
        /// The order store.
        /// </summary>
        private readonly IOrderStore _orderStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderStore">The order store.</param>
        public OrderService(IOrderStore orderStore)
        {
            this._orderStore = orderStore;
        }

        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="orderId">The ID of the order to be canceled.</param>
        /// <exception cref="ArgumentException">No order found with given OrderId.</exception>
        public void CancelOrder(int orderId)
        {
            Order order = this._orderStore.GetOrder(orderId);

            if (order == null)
            {
                throw new ArgumentException("No order found with given OrderId.");
            }

            // Note that since this is a reference type, there is no need to call any further store-level methods.
            order.Status = Status.Canceled;
            order.LastModOn = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="model">The order to be created.</param>
        /// <returns>The details of the order created.</returns>
        public OrderDetailDto CreateOrder(OrderCreateDto model)
        {
            Order order = new(model);

            // The OrderId is set at the store level. Since it is a reference type, the ID carries over.
            this._orderStore.AddOrder(order);

            return new OrderDetailDto(order);
        }

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="options">The options for filtering the results.</param>
        /// <returns>The filtered list of orders.</returns>
        public List<OrderListDto> GetOrders(GetOrdersOptions options)
        {
            List<Order> orders = this._orderStore.GetOrders(options);

            return orders.Select(x => new OrderListDto(x)).ToList();
        }

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="model">The order to be updated.</param>
        /// <returns>The details of the updated order.</returns>
        /// <exception cref="ArgumentException">OrderId is required.</exception>
        /// <exception cref="ArgumentException">No Order found with given OrderId.</exception>
        public OrderDetailDto UpdateOrder(OrderUpdateDto model)
        {
            if (model.OrderId == null)
            {
                throw new ArgumentException("OrderId is required.");
            }

            Order order = this._orderStore.GetOrder(model.OrderId.Value);

            if (order == null)
            {
                throw new ArgumentException("No Order found with the given OrderId.");
            }

            // Note that since this is a reference type, there is no need to call any further store-level methods.
            order.Description = model.Description;
            order.LastModOn = DateTimeOffset.UtcNow;

            return new OrderDetailDto(order);
        }
    }
}