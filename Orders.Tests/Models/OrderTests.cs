// <copyright file="OrderTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests.Models
{
    using System;
    using Orders.DTOs;
    using Orders.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the Order class.
    /// </summary>
    public class OrderTests
    {
        /// <summary>
        /// Tests that the Order constructor returns the expected result.
        /// </summary>
        [Fact]
        public void Order_Constructor1_Pass()
        {
            // Setup Fixtures.
            DateTimeOffset createdOn = DateTimeOffset.UtcNow.AddDays(-3);
            DateTimeOffset lastModOn = DateTimeOffset.UtcNow.AddDays(3);
            Status status = Status.Canceled;

            // Execute SUT.
            Order sut = new()
            {
                CreatedOn = createdOn,
                Description = TestValues.Description,
                LastModOn = lastModOn,
                OrderId = TestValues.OrderId,
                Status = status,
            };

            // Verify Results.
            Assert.Equal(createdOn, sut.CreatedOn);
            Assert.Equal(TestValues.Description, sut.Description);
            Assert.Equal(lastModOn, sut.LastModOn);
            Assert.Equal(TestValues.OrderId, sut.OrderId);
            Assert.Equal(status, sut.Status);
        }

        /// <summary>
        /// Tests that the Order constructor returns the expected result.
        /// </summary>
        [Fact]
        public void Order_Constructor2_Pass()
        {
            OrderCreateDto createDto = TestValues.OrderCreateDto;

            // Execute SUT.
            Order sut = new(createDto);

            // Verify Results.
            Assert.Null(sut.OrderId);
            Assert.True(DateTimeOffset.UtcNow - sut.CreatedOn < TimeSpan.FromSeconds(1));
            Assert.Equal(createDto.Description, sut.Description);
            Assert.Null(sut.LastModOn);
            Assert.Equal(Status.Active, sut.Status);
        }
    }
}