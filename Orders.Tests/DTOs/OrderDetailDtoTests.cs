// <copyright file="OrderDetailDtoTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests.DTOs
{
    using System;
    using Orders.DTOs;
    using Orders.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the OrderDetailDto class.
    /// </summary>
    public class OrderDetailDtoTests
    {
        /// <summary>
        /// Tests that the OrderDetailDto constructor returns the expected result.
        /// </summary>
        [Fact]
        public void OrderDetailDto_Pass_Constructor1()
        {
            // Execute SUT.
            OrderDetailDto sut = new();

            // Verify Results.
            Assert.Null(sut.Description);
            Assert.Null(sut.OrderId);
            Assert.Null(sut.Status);
        }

        /// <summary>
        /// Tests that the OrderDetailDto constructor returns the expected result.
        /// </summary>
        [Fact]
        public void OrderDetailDto_Pass_Constructor2()
        {
            // Setup Fixtures.
            Order order = new()
            {
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-3),
                Description = TestValues.Description,
                LastModOn = DateTimeOffset.UtcNow.AddDays(3),
                OrderId = TestValues.OrderId,
                Status = Status.Active,
            };

            // Execute SUT.
            OrderDetailDto sut = new(order);

            // Verify Results.
            Assert.Equal(order.Description, sut.Description);
            Assert.Equal(order.OrderId, sut.OrderId);
            Assert.Equal(order.Status.ToString(), sut.Status);
            Assert.Equal(order.CreatedOn, sut.CreatedOn);
            Assert.Equal(order.LastModOn, sut.LastModOn);
        }
    }
}