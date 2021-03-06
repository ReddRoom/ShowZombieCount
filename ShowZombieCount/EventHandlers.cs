namespace ShowZombieCount
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;

    public class EventHandlers
    {
        internal readonly List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        internal string CachedMessage = string.Empty;

        public void OnWaitingForPlayers()
        {
            Timing.KillCoroutines(Coroutines.ToArray());
            Coroutines.Clear();
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp049)
            {
                Coroutines.Add(Timing.RunCoroutine(ZombieCountMessage(ev.Player), ev.Player.UserId));
            }
            else
            {
                Timing.KillCoroutines(ev.Player.UserId);
            }
        }

        private IEnumerator<float> ZombieCountMessage(Player ply)
        {
            while (true)
            {
                ply.ShowHint(CachedMessage.Replace("%ZombieCount", Player.Get(RoleType.Scp0492).Count().ToString()), 1f);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}