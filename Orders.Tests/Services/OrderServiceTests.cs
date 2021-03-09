// <copyright file="OrderServiceTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Orders.DTOs;
    using Orders.Models;
    using Orders.Services;
    using Orders.Services.Options;
    using Orders.Stores;
    using Xunit;

    /// <summary>
    /// Unit tests for the OrderService class.
    /// </summary>
    public class OrderServiceTests
    {
        /// <summary>
        /// The mock OrderStore to use for testing.
        /// </summary>
        private readonly Mock<IOrderStore> _orderStoreMock;

        /// <summary>
        /// The service to use for testing.
        /// </summary>
        private readonly OrderService _sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderServiceTests"/> class.
        /// </summary>
        public OrderServiceTests()
        {
            this._orderStoreMock = new Mock<IOrderStore>();

            this._sut = new OrderService(this._orderStoreMock.Object);
        }

        /// <summary>
        /// Tests that OrderService.CancelOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CancelOrder_Fail_NotFound()
        {
            // Setup Fixtures.
            int orderId = TestValues.OrderId;

            // Setup Mocks.
            this._orderStoreMock
                .Setup(m => m.GetOrder(
                    It.Is<int>(oId => oId == orderId)))
                .Returns(null as Order)
                .Verifiable();

            // Execute SUT.
            ArgumentException result = Assert.Throws<ArgumentException>(() => this._sut.CancelOrder(orderId));

            // Verify Results.
            Assert.Equal("No order found with given OrderId. (Parameter 'orderId')", result.Message);
            Assert.Equal("orderId", result.ParamName);

            this._orderStoreMock.Verify();
        }

        /// <summary>
        /// Tests that OrderService.CancelOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CancelOrder_Pass()
        {
            // Setup Fixtures.
            int orderId = TestValues.OrderId;
            Order order = TestValues.Order;

            // Setup Mocks.
            this._orderStoreMock
                .Setup(m => m.GetOrder(
                    It.Is<int>(oId => oId == orderId)))
                .Returns(order)
                .Verifiable();

            // Execute SUT.
            this._sut.CancelOrder(orderId);

            // Verify Results.
            Assert.Equal(Status.Canceled, order.Status);

            this._orderStoreMock.Verify();
        }

        /// <summary>
        /// Tests that OrderService.CreateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CreateOrder_Pass()
        {
            // Setup Fixtures.
            OrderCreateDto dto = TestValues.OrderCreateDto;

            // Execute SUT.
            OrderDetailDto result = this._sut.CreateOrder(dto);

            // Verify Results.
            Assert.Equal(dto.Description, result.Description);

            this._orderStoreMock
                .Verify(
                    m => m.AddOrder(
                        It.Is<Order>(o => o.OrderId == null && o.Description.Equals(dto.Description))),
                    Times.Once);
        }

        /// <summary>
        /// Tests that OrderService.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass()
        {
            // Setup Fixtures.
            List<Order> orders = new() { TestValues.Order };
            GetOrdersOptions options = new();

            // Setup Mocks.
            this._orderStoreMock
                .Setup(m => m.GetOrders(
                    It.Is<GetOrdersOptions>(opt =>
                        opt.IncludeCanceled == options.IncludeCanceled &&
                        opt.PageNumber == options.PageNumber &&
                        opt.PageSize == options.PageSize &&
                        opt.SearchText == null)))
                .Returns(orders)
                .Verifiable();

            // Execute SUT.
            List<OrderListDto> result = this._sut.GetOrders(options);

            // Verify Results.
            Assert.Equal(orders.Count, result.Count);
            this._orderStoreMock.Verify();
        }

        /// <summary>
        /// Tests that OrderService.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Fail_NotFound()
        {
            // Setup Fixtures.
            OrderUpdateDto dto = TestValues.OrderUpdateDto;

            // Setup Mocks.
            this._orderStoreMock
                .Setup(m => m.GetOrder(
                    It.Is<int>(oId => oId == dto.OrderId)))
                .Returns(null as Order)
                .Verifiable();

            // Execute SUT.
            ArgumentException result = Assert.Throws<ArgumentException>(() => this._sut.UpdateOrder(dto));

            // Verify Results.
            Assert.Equal("No order found with given OrderId.", result.Message);

            this._orderStoreMock.Verify();
        }

        /// <summary>
        /// Tests that OrderService.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Fail_NullId()
        {
            // Setup Fixtures.
            OrderUpdateDto dto = new()
            {
                OrderId = null,
            };

            // Execute SUT.
            ArgumentException result = Assert.Throws<ArgumentException>(() => this._sut.UpdateOrder(dto));

            // Verify Results.
            Assert.Equal("OrderId is required.", result.Message);
        }

        /// <summary>
        /// Tests that OrderService.CancelOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Pass()
        {
            // Setup Fixtures.
            OrderUpdateDto dto = TestValues.OrderUpdateDto;
            dto.Description = "updated_description";
            Order order = TestValues.Order;

            // Setup Mocks.
            this._orderStoreMock
                .Setup(m => m.GetOrder(
                    It.Is<int>(oId => oId == dto.OrderId)))
                .Returns(order)
                .Verifiable();

            // Execute SUT.
            OrderDetailDto result = this._sut.UpdateOrder(dto);

            // Verify Results.
            Assert.True(DateTimeOffset.UtcNow - result.LastModOn < TimeSpan.FromSeconds(1));
            Assert.Equal(dto.Description, result.Description);

            this._orderStoreMock.Verify();
        }
    }
}