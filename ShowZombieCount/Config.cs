// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public sealed class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the message to show to all Scp049 instances.
        /// </summary>
        public string Text { get; set; } = "<color=red>Zombies:</color> %ZombieCount";

        /// <summary>
        /// Gets or sets the vertical position to display the text.
        /// </summary>
        public uint VerticalOffset { get; set; } = 3;
    }
}