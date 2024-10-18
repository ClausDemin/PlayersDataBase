using PlayersDatabase;

namespace PlayerDatabaseApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Database database = new Database();

            PrintCommandsList(new AvailableCommands());

            string commandRequest = "please enter command from list you wish to execute: ";

            ParseCommand(AskForUserInput(commandRequest), out var command);

            while (command != Commands.Exit)
            {
                switch (command)
                {
                    case Commands.Add:
                        HandleAddCommand(database);
                        break;

                    case Commands.Remove:
                        HandleRemoveCommand(database);
                        break;

                    case Commands.Ban:
                        HandleBanCommand(database);
                        break;

                    case Commands.Unban:
                        HandleUnbanCommand(database);
                        break;

                    case Commands.Print:
                        HandlePrintCommand(database);
                        break;
                }

                ParseCommand(AskForUserInput(commandRequest), out command);
            }
        }

        private static void PrintCommandsList(AvailableCommands availableCommands)
        {
            foreach (var command in availableCommands.AvailableCommandsList)
            {
                Console.WriteLine("print " + command);
            }
            Console.WriteLine();
        }

        private static void PrintErrorMessage(string message, ConsoleColor errorTextColor = ConsoleColor.Red)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = errorTextColor;

            Console.WriteLine($"Error: {message}");

            Console.ForegroundColor = defaultColor;
        }

        private static void ParseCommand(string input, out Commands command)
        {
            if (Enum.TryParse(input, true, out command) == false)
            {
                PrintErrorMessage($"command {input} not exist");
                command = Commands.Unknown;
            }
        }

        private static string AskForUserInput(string request)
        {
            Console.Write(request);

            return Console.ReadLine();
        }

        private static void HandleAddCommand(Database database)
        {
            string request = "please enter a username: ";
            string name = AskForUserInput(request);

            request = "please enter player's level: ";
            bool isParsed = uint.TryParse(AskForUserInput(request), out uint level);

            if (isParsed)
            {
                if (database.TryAddPlayer(name, level) == false)
                {
                    string message = "\"name\" can't be empty";

                    PrintErrorMessage(message);
                }
            }
            else
            {
                PrintErrorMessage("level must be integer greater than zero value");
            }
        }

        private static void HandleRemoveCommand(Database database)
        {
            string request = "please enter player ID you wish to remove: ";

            uint playerId = AskUserForPlayerId(request);

            if (database.TryRemovePlayer(playerId) == false)
            {
                string message = $"player with Id: {playerId} not exist";

                PrintErrorMessage(message);
            }
        }

        private static void HandleBanCommand(Database database)
        {
            string request = "please enter player ID you wish to ban: ";

            uint playerId = AskUserForPlayerId(request);

            if (database.TryBanPlayer(playerId) == false)
            {
                string message = $"player with Id: {playerId} not exist";

                PrintErrorMessage(message);
            }
        }

        private static void HandleUnbanCommand(Database database)
        {
            string request = "please enter player ID you wish to unban: ";

            uint playerId = AskUserForPlayerId(request);

            if (database.TryUnbanPlayer(playerId) == false)
            {
                string message = $"player with Id: {playerId} not exist";

                PrintErrorMessage(message);
            }
        }

        private static void HandlePrintCommand(Database database)
        {
            Console.WriteLine();

            foreach (var player in database.Players)
            {
                foreach (var property in player)
                {
                    Console.WriteLine(property);
                }
                Console.WriteLine();
            }
        }

        private static uint AskUserForPlayerId(string request)
        {
            bool isParsed = uint.TryParse(AskForUserInput(request), out uint playerId);

            if (isParsed)
            {
                return playerId;
            }
            else
            {
                PrintErrorMessage("player ID must be integer greater than zero value");
            }

            return AskUserForPlayerId(request);
        }
    }
}
