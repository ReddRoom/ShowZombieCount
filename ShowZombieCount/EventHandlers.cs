using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace ShowZombieCount
{
    class EventHandlers
    {
        public HashSet<string> scp049 = new HashSet<string>();
        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();
        StringBuilder message = new StringBuilder();
        private IEnumerator<float> ZombieCountMessage()
        {
            for (; ; )
            {
                    message.Clear();
                    foreach (Player ply in Player.List)
                {
                    var ZombieCount = Player.Get(RoleType.Scp0492).Count();
                    if (Plugin.Singleton.Config.text_position != 0 && Plugin.Singleton.Config.text_position < 0)
                    {
                        for (int i = Plugin.Singleton.Config.text_position; i < 0; i++)
                        {
                            message.Append("\n");
                        }
                    }
                    else if (Plugin.Singleton.Config.text_position != 0 && Plugin.Singleton.Config.text_position > 0)
                    {
                        for (int i = 0; i < Plugin.Singleton.Config.text_position; i++)
                        {
                            message.Append("\n");
                        }
                    }
                    message.Append(Plugin.Singleton.Config.text);
                    message.Replace("%zombiecount", $"{ZombieCount}");
                    ply.ShowHint(message.ToString(), 1f);
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }

            public void OnChangingRole(ChangingRoleEventArgs ev)
            {
              if(ev.NewRole == RoleType.Scp049 && !scp049.Contains(ev.Player.UserId))
              {
                scp049.Add(ev.Player.UserId);
                coroutines.Add(Timing.RunCoroutine(ZombieCountMessage()));
              }

              else if (scp049.Contains(ev.Player.UserId) && !(ev.NewRole == RoleType.Scp049))
              {
                scp049.Remove(ev.Player.UserId);
                foreach (CoroutineHandle coroutine in coroutines)
                {
                    Timing.KillCoroutines(coroutine);
                }
                coroutines.Clear();
              }
            }
        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (scp049.Contains(ev.Target.UserId))
            {
                scp049.Remove(ev.Target.UserId);
                foreach (CoroutineHandle coroutine in coroutines)
                {
                    Timing.KillCoroutines(coroutine);
                }
                coroutines.Clear();
            }
        }
    }
}
