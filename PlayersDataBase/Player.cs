namespace PlayersDataBase
{
    internal class Player
    {
        public Player(uint identifier, string name, uint level)
        {
            ID = identifier;
            Name = name;
            Level = level;
            IsBanned = false;
        }

        public uint ID { get; }
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
