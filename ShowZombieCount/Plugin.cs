using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using System.Reflection;

using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using MEC;

namespace ShowZombieCount
{
    public class Plugin : Plugin<Config>
    {
        public override PluginPriority Priority => PluginPriority.Default;

        public override string Name { get; } = "ShowZombieCount";
        public override string Author { get; } = "Cwaniak U.G";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(2, 3, 4);

        private EventHandlers handlers;

        public override void OnEnabled()
        {
            handlers = new EventHandlers();
            Plugin.Singleton = this;
            PlayerEv.ChangingRole += handlers.OnChangingRole;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            handlers = null;
            PlayerEv.ChangingRole -= handlers.OnChangingRole;
            base.OnDisabled();
        }

        public static Plugin Singleton;
    }
}
