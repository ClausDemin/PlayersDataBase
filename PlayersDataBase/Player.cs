namespace PlayersDatabase
{
    internal class Player
    {
        public Player(uint identifier, string name, uint level)
        {
            Id = identifier;
            Name = name;
            Level = level;
            IsBanned = false;
        }

        public uint Id { get; }
        public string Name { get; private set; }
        public uint Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }
}
