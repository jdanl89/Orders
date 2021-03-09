// <copyright file="OrderStoreTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests.Stores
{
    using System.Collections.Generic;
    using Orders.Models;
    using Orders.Services.Options;
    using Orders.Stores;
    using Xunit;

    /// <summary>
    /// Unit tests for the OrderStore class.
    /// </summary>
    public class OrderStoreTests
    {
        private readonly OrderStore _sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStoreTests"/> class.
        /// </summary>
        public OrderStoreTests()
        {
            this._sut = new OrderStore();
        }

        /// <summary>
        /// Tests that OrderStore.AddOrder returns the expected result.
        /// </summary>
        [Fact]
        public void AddOrder_Pass()
        {
            // Setup Fixtures.
            Order order1 = new()
            {
                Description = "first order.",
            };

            Order order2 = new()
            {
                Description = "second order.",
            };

            // Execute SUT.
            this._sut.AddOrder(order1);
            this._sut.AddOrder(order2);
            List<Order> results = this._sut.GetOrders();

            // Verify Results.
            Assert.Equal(2, results.Count);
            Assert.Contains(results, x => x.OrderId == 1 && x.Description.Equals(order1.Description));
            Assert.Contains(results, x => x.OrderId == 2 && x.Description.Equals(order2.Description));
        }

        /// <summary>
        /// Tests that OrderStore.GetOrder returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrder_Pass()
        {
            // Setup Fixtures.
            Order order1 = new()
            {
                Description = "first order.",
            };

            Order order2 = new()
            {
                Description = "second order.",
            };

            // Execute SUT.
            this._sut.AddOrder(order1);
            this._sut.AddOrder(order2);
            Order result = this._sut.GetOrder(1);

            // Verify Results.
            Assert.NotNull(result);
            Assert.Equal(order1.Description, result.Description);
        }

        /// <summary>
        /// Tests that OrderStore.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass_NullOptions()
        {
            // Setup Fixtures.
            Order order1 = new()
            {
                Description = "first order.",
            };

            Order order2 = new()
            {
                Description = "second order.",
            };

            // Execute SUT.
            this._sut.AddOrder(order1);
            this._sut.AddOrder(order2);
            List<Order> results = this._sut.GetOrders();

            Assert.Contains(results, x => x.OrderId == 1 && x.Description.Equals(order1.Description));
            Assert.Contains(results, x => x.OrderId == 2 && x.Description.Equals(order2.Description));
        }

        /// <summary>
        /// Tests that OrderStore.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass_Options()
        {
            // Setup Fixtures.
            Order order1 = new()
            {
                Description = "first order.",
            };

            Order order2 = new()
            {
                Description = "found second order.",
                Status = Status.Canceled,
            };

            Order order3 = new()
            {
                Description = "third FOUND order.",
            };

            Order order4 = new()
            {
                Description = "fourth order.found",
            };

            GetOrdersOptions options = new()
            {
                IncludeCanceled = false,
                PageNumber = 1,
                PageSize = 2,
                SearchText = "found",
            };

            // Execute SUT.
            this._sut.AddOrder(order1);
            this._sut.AddOrder(order2);
            this._sut.AddOrder(order3);
            this._sut.AddOrder(order4);
            List<Order> results = this._sut.GetOrders(options);

            // Verify Results.
            Assert.Equal(2, results.Count);
            Assert.Contains(results, x => x.OrderId == 3 && x.Description.Equals(order3.Description));
            Assert.Contains(results, x => x.OrderId == 4 && x.Description.Equals(order4.Description));
        }
    }
}