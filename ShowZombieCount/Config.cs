// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using System.Text;
    using Exiled.API.Interfaces;
    using NorthwoodLib.Pools;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public sealed class Config : IConfig
    {
        /// <summary>
        /// Gets the configured message.
        /// </summary>
        [YamlIgnore]
        public string ConfiguredText { get; private set; }

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

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.ReloadedConfigs"/>
        public void OnReloadedConfigs() => ConfiguredText = SetupMessage();

        private static string NewLineFormatter(uint lineNumber)
        {
            StringBuilder lineBuilder = StringBuilderPool.Shared.Rent();
            for (var i = 32; i > lineNumber; i--)
                lineBuilder.Append("\n");

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