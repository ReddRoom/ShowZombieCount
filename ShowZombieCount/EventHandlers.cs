// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ShowZombieCount
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;

    /// <summary>
    /// Handles all events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly List<CoroutineHandle> coroutines = new List<CoroutineHandle>();
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnWaitingForPlayers"/>
        public void OnWaitingForPlayers()
        {
            KillDisplays();
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingRole(ChangingRoleEventArgs)"/>
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Timing.KillCoroutines($"SZC#{ev.Player.UserId}");
            if (ev.NewRole == RoleType.Scp049)
                coroutines.Add(Timing.RunCoroutine(ZombieCountMessage(ev.Player), $"SZC#{ev.Player.UserId}"));
        }

        /// <summary>
        /// Terminates all coroutines associated with displaying the count of Scp0492 instances.
        /// </summary>
        public void KillDisplays()
        {
            foreach (CoroutineHandle coroutine in coroutines)
                Timing.KillCoroutines(coroutine);

            coroutines.Clear();
        }

        private IEnumerator<float> ZombieCountMessage(Player ply)
        {
            while (true)
            {
                ply.ShowHint(plugin.Config.ConfiguredText.Replace("%ZombieCount", Player.Get(RoleType.Scp0492).Count().ToString()), 1f);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}