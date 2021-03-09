// <copyright file="OrdersControllerTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Orders.Controllers;
    using Orders.DTOs;
    using Orders.Services;
    using Orders.Services.Options;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="OrdersController"/>.
    /// </summary>
    public class OrdersControllerTests
    {
        /// <summary>
        /// The mock of the order service.
        /// </summary>
        private readonly Mock<IOrderService> _orderServiceMock;

        /// <summary>
        /// The OrdersController used for testing.
        /// </summary>
        private readonly OrdersController _sut;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersControllerTests"/> class.
        /// </summary>
        public OrdersControllerTests()
        {
            Mock<ILogger<OrdersController>> loggerMock = new();
            this._orderServiceMock = new Mock<IOrderService>();
            this._sut = new OrdersController(loggerMock.Object, this._orderServiceMock.Object);
        }

        /// <summary>
        /// Tests that OrdersController.CancelOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CancelOrder_Fail()
        {
            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.CancelOrder(
                    It.Is<int>(oId => oId == TestValues.OrderId)))
                .Throws(new ArgumentException(TestValues.ExceptionMessage))
                .Verifiable();

            // Execute SUT.
            IActionResult result = this._sut.CancelOrder(TestValues.OrderId);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            string message = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(TestValues.ExceptionMessage, message);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.CancelOrder returns the expected result.
        /// </summary>
        /// <param name="orderId">The order to test.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(-1001)]
        public void CancelOrder_Pass(int orderId)
        {
            // Execute SUT.
            IActionResult result = this._sut.CancelOrder(orderId);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            string value = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"Order {orderId} canceled successfully.", value);

            this._orderServiceMock
                .Verify(
                    m => m.CancelOrder(It.Is<int>(oId => oId == orderId)),
                    Times.Once);
        }

        /// <summary>
        /// Tests that the OrdersController constructor returns the expected result.
        /// </summary>
        [Fact]
        public void Constructor_Pass()
        {
            // Execute SUT.
            Assert.NotNull(this._sut);
        }

        /// <summary>
        /// Tests that OrdersController.CreateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CreateOrder_Fail_InvalidModelState()
        {
            // Setup Fixtures.
            const string key = "key";
            const string errorMessage = TestValues.ExceptionMessage;

            // Execute SUT.
            this._sut.ModelState.AddModelError(key, errorMessage);
            IActionResult result = this._sut.CreateOrder(TestValues.OrderCreateDto);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            SerializableError error = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Single(error);
            string[] messages = Assert.IsType<string[]>(error[key]);
            Assert.Single(messages);
            Assert.Equal(errorMessage, messages[0]);
        }

        /// <summary>
        /// Tests that OrdersController.CreateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CreateOrder_Fail_Null()
        {
            // Execute SUT.
            IActionResult result = this._sut.CreateOrder(null);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            string message = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("The request object cannot be null.", message);
        }

        /// <summary>
        /// Tests that OrdersController.CreateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void CreateOrder_Pass()
        {
            // Setup Fixtures.
            OrderCreateDto model = TestValues.OrderCreateDto;

            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.CreateOrder(
                    It.Is<OrderCreateDto>(dto => dto.Description.Equals(model.Description))))
                .Returns(TestValues.OrderDetailDto)
                .Verifiable();

            // Execute SUT.
            IActionResult result = this._sut.CreateOrder(model);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            OrderDetailDto details = Assert.IsType<OrderDetailDto>(okResult.Value);
            Assert.Equal(model.Description, details.Description);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass_NullOptions()
        {
            // Setup Fixtures.
            GetOrdersOptions options = new();

            List<OrderListDto> searchResult = new()
            {
                TestValues.OrderListDto,
            };

            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.GetOrders(
                    It.Is<GetOrdersOptions>(opt =>
                        opt.IncludeCanceled == options.IncludeCanceled &&
                        opt.PageNumber == options.PageNumber &&
                        opt.PageSize == options.PageSize &&
                        opt.SearchText == null)))
                .Returns(searchResult)
                .Verifiable();

            // Execute SUT.
            IActionResult results = this._sut.GetOrders(null);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<OrderListDto> value = Assert.IsType<List<OrderListDto>>(okResult.Value);
            Assert.Equal(searchResult.Count, value.Count);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass_Options()
        {
            // Setup Fixtures.
            GetOrdersOptions options = new()
            {
                IncludeCanceled = true,
                PageNumber = 5,
                PageSize = 1,
                SearchText = string.Empty,
            };

            List<OrderListDto> searchResult = new()
            {
                TestValues.OrderListDto,
            };

            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.GetOrders(
                    It.Is<GetOrdersOptions>(opt =>
                        opt.IncludeCanceled == options.IncludeCanceled &&
                        opt.PageNumber == options.PageNumber &&
                        opt.PageSize == options.PageSize &&
                        opt.SearchText.Equals(options.SearchText))))
                .Returns(searchResult)
                .Verifiable();

            // Execute SUT.
            IActionResult results = this._sut.GetOrders(options);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<OrderListDto> value = Assert.IsType<List<OrderListDto>>(okResult.Value);
            Assert.Equal(searchResult.Count, value.Count);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.GetOrders returns the expected result.
        /// </summary>
        [Fact]
        public void GetOrders_Pass_Options_ResetToDefaults()
        {
            // Setup Fixtures.
            GetOrdersOptions options = new()
            {
                IncludeCanceled = true,
                PageNumber = -1,
                PageSize = -1,
                SearchText = string.Empty,
            };

            List<OrderListDto> searchResult = new()
            {
                TestValues.OrderListDto,
            };

            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.GetOrders(
                    It.Is<GetOrdersOptions>(opt =>
                        opt.IncludeCanceled == options.IncludeCanceled &&
                        opt.PageNumber == 1 &&
                        opt.PageSize == 10 &&
                        opt.SearchText.Equals(options.SearchText))))
                .Returns(searchResult)
                .Verifiable();

            // Execute SUT.
            IActionResult results = this._sut.GetOrders(options);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<OrderListDto> value = Assert.IsType<List<OrderListDto>>(okResult.Value);
            Assert.Equal(searchResult.Count, value.Count);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOder_Fail_ArgumentException()
        {
            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.UpdateOrder(
                    It.IsAny<OrderUpdateDto>()))
                .Throws(new ArgumentException(TestValues.ExceptionMessage))
                .Verifiable();

            // Execute SUT.
            IActionResult result = this._sut.UpdateOrder(null, TestValues.OrderUpdateDto);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            string message = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(TestValues.ExceptionMessage, message);

            this._orderServiceMock.Verify();
        }

        /// <summary>
        /// Tests that OrdersController.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Fail_InvalidModelState()
        {
            // Setup Fixtures.
            const string key = "key";
            const string errorMessage = TestValues.ExceptionMessage;

            // Execute SUT.
            this._sut.ModelState.AddModelError(key, errorMessage);
            IActionResult result = this._sut.UpdateOrder(null, TestValues.OrderUpdateDto);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            SerializableError error = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Single(error);
            string[] messages = Assert.IsType<string[]>(error[key]);
            Assert.Single(messages);
            Assert.Equal(errorMessage, messages[0]);
        }

        /// <summary>
        /// Tests that OrdersController.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Fail_Null()
        {
            // Execute SUT.
            IActionResult result = this._sut.UpdateOrder(null, null);

            // Verify Results.
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            string message = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("The request object cannot be null.", message);
        }

        /// <summary>
        /// Tests that OrdersController.UpdateOrder returns the expected result.
        /// </summary>
        [Fact]
        public void UpdateOrder_Pass()
        {
            // Setup Fixtures.
            OrderUpdateDto model = TestValues.OrderUpdateDto;

            // Setup Mocks.
            this._orderServiceMock
                .Setup(m => m.UpdateOrder(
                    It.Is<OrderUpdateDto>(dto => dto.Description.Equals(model.Description))))
                .Returns(TestValues.OrderDetailDto)
                .Verifiable();

            // Execute SUT.
            IActionResult result = this._sut.UpdateOrder(model.OrderId, model);

            // Verify Results.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            OrderDetailDto details = Assert.IsType<OrderDetailDto>(okResult.Value);
            Assert.Equal(model.Description, details.Description);

            this._orderServiceMock.Verify();
        }
    }
}