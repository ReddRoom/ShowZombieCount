using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace ShowZombieCount
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public string text { get; set; } = "<color=red>Zombies:</color> %zombiecount";

        [Description("Determines the position of the text on screen (-15 = Below, 0 = Middle, 32 = Top)")]
        public int text_position { get; set; } = 15;
    }
}
