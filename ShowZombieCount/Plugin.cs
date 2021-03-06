namespace ShowZombieCount
{
    using System;
    using System.Text;
    using Exiled.API.Features;
    using MEC;
    using NorthwoodLib.Pools;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "ShowZombieCount";
        public override string Author { get; } = "Cwaniak U.G";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 3, 4);

        private EventHandlers _handlers;
        
        public override void OnEnabled()
        {
            _handlers = new EventHandlers();
            PlayerHandlers.ChangingRole += _handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers += _handlers.OnWaitingForPlayers;
            _handlers.CachedMessage = SetupMessage();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Timing.KillCoroutines(_handlers.Coroutines.ToArray());
            PlayerHandlers.ChangingRole -= _handlers.OnChangingRole;
            ServerHandlers.WaitingForPlayers -= _handlers.OnWaitingForPlayers;
            _handlers = null;
            base.OnDisabled();
        }

        private string SetupMessage()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(Config.Text);
            stringBuilder.AppendLine(NewLineFormatter(Config.VerticalOffset));
            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        private static string NewLineFormatter(uint lineNumber)
        {
            var lineBuilder = StringBuilderPool.Shared.Rent();
            for (var i = 32; i > lineNumber; i--)
                lineBuilder.Append("\n");

            return StringBuilderPool.Shared.ToStringReturn(lineBuilder);
        }
    }
}