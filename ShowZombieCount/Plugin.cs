// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using System;
    using Exiled.API.Features;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        /// <inheritdoc />
        public override string Author => "Build";

        /// <inheritdoc />
        public override string Name => "ShowZombieCount";

        /// <inheritdoc />
        public override string Prefix => "ShowZombieCount";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new(5, 2, 2);

        /// <inheritdoc />
        public override Version Version { get; } = new(2, 0, 3);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            PlayerHandlers.ChangingRole += eventHandlers.OnChangingRole;
            PlayerHandlers.Spawned += eventHandlers.OnSpawned;
            ServerHandlers.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            eventHandlers.KillDisplays();
            PlayerHandlers.ChangingRole -= eventHandlers.OnChangingRole;
            PlayerHandlers.Spawned -= eventHandlers.OnSpawned;
            ServerHandlers.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}