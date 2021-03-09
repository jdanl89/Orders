// <copyright file="TestValues.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Tests
{
    using System;
    using Orders.DTOs;
    using Orders.Models;

    /// <summary>
    /// Values used for testing.
    /// </summary>
    public static class TestValues
    {
        /// <summary>
        /// The description test value.
        /// </summary>
        public const string Description = "unit_test_description";

        /// <summary>
        /// The exception message test value.
        /// </summary>
        public const string ExceptionMessage = "unit_test_exception_message";

        /// <summary>
        /// The order id test value.
        /// </summary>
        public const int OrderId = -1001;

        /// <summary>
        /// Gets a test Order object.
        /// </summary>
        public static Order Order => new()
        {
            CreatedOn = DateTimeOffset.UtcNow.AddDays(-3),
            Description = Description,
            LastModOn = DateTimeOffset.UtcNow.AddDays(3),
            OrderId = OrderId,
            Status = Status.Active,
        };

        /// <summary>
        /// Gets a test OrderCreateDto object.
        /// </summary>
        public static OrderCreateDto OrderCreateDto => new()
        {
            Description = Description,
        };

        /// <summary>
        /// Gets a test OrderDetailDto object.
        /// </summary>
        public static OrderDetailDto OrderDetailDto => new()
        {
            CreatedOn = DateTimeOffset.UtcNow.AddDays(-3),
            Description = Description,
            LastModOn = DateTimeOffset.UtcNow.AddDays(3),
            OrderId = OrderId,
            Status = Status.Active.ToString(),
        };

        /// <summary>
        /// Gets a test OrderListDto object.
        /// </summary>
        public static OrderListDto OrderListDto => new()
        {
            Description = Description,
            OrderId = OrderId,
            Status = Status.Active.ToString(),
        };

        /// <summary>
        /// Gets a test OrderUpdateDto object.
        /// </summary>
        public static OrderUpdateDto OrderUpdateDto => new()
        {
            Description = Description,
            OrderId = OrderId,
        };
    }
}