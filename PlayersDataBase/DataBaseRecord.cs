namespace PlayersDatabase
{
    internal class DataBaseRecord<T>
        where T : class
    {
        public DataBaseRecord(T dataBaseObject)
        {
            var type = dataBaseObject.GetType();
            var properties = type.GetProperties();

            Data = new string[properties.Length];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = $"{properties[i].Name} {properties[i].GetValue(dataBaseObject)}";
            }
        }

        public string[] Data { get; }
    }
}
