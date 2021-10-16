// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using System;
    using System.Text;
    using Exiled.API.Features;
    using MEC;
    using NorthwoodLib.Pools;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private EventHandlers handlers;

        /// <inheritdoc />
        public override string Name { get; } = "ShowZombieCount";

        /// <inheritdoc />
        public override string Author { get; } = "Build";

        /// <inheritdoc />
        public override Version Version { get; } = new Version(2, 0, 0);

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 5);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            handlers = new EventHandlers();
            PlayerHandlers.ChangingRole += handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers += handlers.OnWaitingForPlayers;
            handlers.CachedMessage = SetupMessage();
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            handlers.KillDisplays();
            PlayerHandlers.ChangingRole -= handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers -= handlers.OnWaitingForPlayers;
            handlers = null;
            base.OnDisabled();
        }

        private static string NewLineFormatter(uint lineNumber)
        {
            var lineBuilder = StringBuilderPool.Shared.Rent();
            for (var i = 32; i > lineNumber; i--)
                lineBuilder.Append("\n");

            return StringBuilderPool.Shared.ToStringReturn(lineBuilder);
        }

        private string SetupMessage()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(Config.Text);
            stringBuilder.AppendLine(NewLineFormatter(Config.VerticalOffset));
            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}