// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using System.ComponentModel;
    using System.Text;
    using Exiled.API.Interfaces;
    using NorthwoodLib.Pools;

    /// <inheritdoc />
    public sealed class Config : IConfig
    {
        private readonly string configuredText;

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        public Config() => configuredText = SetupMessage();

        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the message to show to all Scp049 instances.
        /// </summary>
        [Description("The message to show to all Scp049 instances.")]
        public string Text { get; set; } = "<color=red>Zombies:</color> {0}";

        /// <summary>
        /// Gets or sets the vertical position to display the text.
        /// </summary>
        [Description("The vertical position to display the text.")]
        public uint VerticalOffset { get; set; } = 3;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, before showing the hint for the first time.
        /// </summary>
        [Description("The amount of time, in seconds, before showing the hint for the first time.")]
        public float Delay { get; set; } = 0f;

        /// <summary>
        /// Gets the configured message to show to all Scp049 instances.
        /// </summary>
        /// <returns>The prebuilt configured message.</returns>
        public string GetConfiguredText() => configuredText;

        private static string NewLineFormatter(uint lineNumber)
        {
            StringBuilder lineBuilder = StringBuilderPool.Shared.Rent();
            for (int i = 32; i > lineNumber; i--)
                lineBuilder.AppendLine();

            return StringBuilderPool.Shared.ToStringReturn(lineBuilder);
        }

        private string SetupMessage()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(Text);
            stringBuilder.AppendLine(NewLineFormatter(VerticalOffset));
            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}