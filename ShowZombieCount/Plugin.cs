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
        private EventHandlers handlers;

        /// <inheritdoc />
        public override string Name => "ShowZombieCount";

        /// <inheritdoc />
        public override string Author => "Build";

        /// <inheritdoc />
        public override Version Version { get; } = new Version(2, 0, 2);

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            handlers = new EventHandlers(this);
            PlayerHandlers.ChangingRole += handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers += handlers.OnWaitingForPlayers;

            ServerHandlers.ReloadedConfigs += Config.OnReloadedConfigs;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            ServerHandlers.ReloadedConfigs -= Config.OnReloadedConfigs;

            handlers.KillDisplays();
            PlayerHandlers.ChangingRole -= handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers -= handlers.OnWaitingForPlayers;
            handlers = null;

            base.OnDisabled();
        }
    }
}