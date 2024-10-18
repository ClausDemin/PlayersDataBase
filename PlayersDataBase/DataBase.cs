namespace PlayersDatabase
{
    public class Database
    {
        private Dictionary<uint, Player> _storage;
        private Stack<uint> _freeID;

        public Database()
        {
            _storage = new Dictionary<uint, Player>();
            _freeID = new Stack<uint>();

            _freeID.Push(0);
        }

        public IEnumerable<string[]> Players => GetPlayersData();

        public bool TryAddPlayer(string name, uint level)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            uint Id = _freeID.Pop();

            _storage[Id] = new Player(Id, name, level);

            if (_storage.ContainsKey(++Id) == false) 
            {
                _freeID.Push(Id);
            }

            return true;
        }

        public bool TryRemovePlayer(uint Id)
        {
            if (_storage.ContainsKey(Id))
            {
                _storage[Id] = null;
                _storage.Remove(Id);

                _freeID.Push(Id);

                return true;
            }
            
            return false;
        }

        public bool TryBanPlayer(uint Id)
        {
            if (_storage.ContainsKey(Id))
            {
                _storage[Id].Ban();

                return true;
            }

            return false;
        }

        public bool TryUnbanPlayer(uint Id)
        {
            if (_storage.ContainsKey(Id))
            {
                _storage[Id].Unban();

                return true;
            }

            return false;
        }

        private IEnumerable<string[]> GetPlayersData()
        {
            foreach (var player in _storage.Values)
            {
                var dataBaseObject = new DataBaseRecord<Player>(player);

                yield return dataBaseObject.Data;
            }
        }
    }
}
