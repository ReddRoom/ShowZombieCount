namespace ShowZombieCount
{
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string Text { get; set; } = "<color=red>Zombies:</color> %ZombieCount";
        public uint VerticalOffset { get; set; } = 3;
    }
}