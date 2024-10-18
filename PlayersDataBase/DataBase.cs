namespace PlayersDataBase
{
    public class DataBase
    {
        private Dictionary<uint, Player> _storage;
        private Stack<uint> _freeID;

        public DataBase()
        {
            _storage = new Dictionary<uint, Player>();
            _freeID = new Stack<uint>();

            _freeID.Push(0);
        }

        public IEnumerable<string[]> Players { get => GetData(); }

        public void AddPlayer(string name, uint level)
        {
            if (string.IsNullOrEmpty(name)) 
            { 
                throw new ArgumentNullException("name was null or empty");
            }

            uint ID = _freeID.Pop();

            _storage[ID] = new Player(ID, name, level);

            _freeID.Push(++ID);
        }

        public void RemovePlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID] = null;
                _storage.Remove(ID);

                _freeID.Push(ID);
            }
            else
            {
                throw new KeyNotFoundException($"player with ID: {ID} not exist");
            }
        }

        public void BanPlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID].Ban();
            }
            else
            {
                throw new KeyNotFoundException($"player with ID: {ID} not exist");
            }
        }

        public void UnbanPlayer(uint ID)
        {
            if (_storage.ContainsKey(ID))
            {
                _storage[ID].Unban();
            }
            else
            {
                throw new KeyNotFoundException($"player with ID: {ID} not exist");
            }
        }

        private IEnumerable<string[]> GetData()
        {
            foreach (var player in _storage.Values)
            {
                var dataBaseObject = new DataBaseRecord<Player>(player);

                yield return dataBaseObject.Data;
            }
        }
    }
}
