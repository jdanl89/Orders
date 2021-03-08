// <copyright file="Status.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace Orders.Models
{
    /// <summary>
    /// The valid statuses of an object.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The active status.
        /// </summary>
        Active = 0,

        /// <summary>
        /// The canceled status.
        /// </summary>
        Canceled = 1,
    }
}