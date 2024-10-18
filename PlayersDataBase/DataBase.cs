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

            uint ID = _freeID.Pop();

            _storage[ID] = new Player(ID, name, level);

            _freeID.Push(++ID);

            return true;
        }

        public bool TryRemovePlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID] = null;
                _storage.Remove(ID);

                _freeID.Push(ID);

                return true;
            }
            
            return false;
        }

        public bool TryBanPlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID].Ban();

                return true;
            }

            return false;
        }

        public bool TryUnbanPlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID].Unban();

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
