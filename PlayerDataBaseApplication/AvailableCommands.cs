namespace PlayerDatabaseApplication
{
    internal class AvailableCommands
    {
        private Dictionary<Commands, string> _commands = new Dictionary<Commands, string>()
        {
            {Commands.Add, "To add player to data base" },
            {Commands.Remove, "To remove player from data base"}, 
            {Commands.Ban, "To ban player" },
            {Commands.Unban, "To unban player" },
            {Commands.Print, "To print players list from data base" },
            {Commands.Exit, "To close application" }
        };

        public string[] AvailableCommandsList 
        {
            get 
            {
                string[] commands = new string[_commands.Count];

                for (int i = 0; i < commands.Length; i++) 
                {
                    string commandDescription = _commands[(Commands) i];

                    string output = $"\"{Enum.GetName(typeof(Commands), i)}\" " +
                        $"{commandDescription}";

                    commands[i] = output;
                }

                return commands;
            } 
        }
    }

    internal enum Commands
    {
        Add,
        Remove,
        Ban,
        Unban,
        Print,
        Exit,
        Unknown
    }
}
