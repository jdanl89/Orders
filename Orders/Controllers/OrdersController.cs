// <copyright file="OrdersController.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Orders.DTOs;
    using Orders.Services;
    using Orders.Services.Options;

    /// <summary>
    /// The controller for all things orders.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// The order service.
        /// </summary>
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="orderService">The order service.</param>
        public OrdersController(
            ILogger<OrdersController> logger,
            IOrderService orderService)
        {
            this._logger = logger;
            this._orderService = orderService;
        }

        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="orderId">The ID of the order to be canceled.</param>
        /// <returns>A confirmation message that the order was canceled.</returns>
        [HttpPut]
        [Route("/orders/{orderId}/cancel")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                this._orderService.CancelOrder(orderId);
                return this.Ok($"Order {orderId} canceled successfully.");
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="model">The order creation model.</param>
        /// <returns>The order that was created.</returns>
        [HttpPost]
        [Route("/orders")]
        public IActionResult CreateOrder(OrderCreateDto model)
        {
            if (model == null)
            {
                return this.BadRequest("The request object cannot be null.");
            }

            this._logger.LogInformation("Creating Order.", model);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            OrderDetailDto order = this._orderService.CreateOrder(model);
            return this.Ok(order);
        }

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="options">The request filtration options.</param>
        /// <returns>A paginated list of orders meeting the filter criteria.</returns>
        [HttpGet]
        [Route("/orders")]
        public IActionResult GetOrders([FromQuery] GetOrdersOptions options)
        {
            options ??= new GetOrdersOptions();

            if (options.PageNumber < 1)
            {
                options.PageNumber = 1;
            }

            if (options.PageSize < 0)
            {
                options.PageSize = 10;
            }

            List<OrderListDto> orders = this._orderService.GetOrders(options);
            return this.Ok(orders);
        }

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="orderId">The ID of the order to be updated. Can be retrieved from body.</param>
        /// <param name="model">The order to be updated.</param>
        /// <returns>The order created or the validation result.</returns>
        [HttpPut]
        [Route("/orders/{orderId}")]
        public IActionResult UpdateOrder(int? orderId, [FromBody] OrderUpdateDto model)
        {
            if (model == null)
            {
                return this.BadRequest("The request object cannot be null.");
            }

            if (orderId != null)
            {
                model.OrderId = orderId;
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                OrderDetailDto order = this._orderService.UpdateOrder(model);
                return this.Ok(order);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}