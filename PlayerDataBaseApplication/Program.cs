using PlayersDataBase;
using System.Formats.Asn1;

namespace PlayerDataBaseApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();

            PrintCommandsList(new AvailableCommands());

            string commandRequest = "please enter command from list you wish to execute: ";

            ParseCommand(AskForUserInput(commandRequest), out var command);

            while (command != Commands.Exit)
            {
                switch (command)
                {
                    case Commands.Add:
                        HandleAddCommand(dataBase);
                        break;

                    case Commands.Remove:
                        HandleRemoveCommand(dataBase);
                        break;

                    case Commands.Ban:
                        HandleBanCommand(dataBase);
                        break;

                    case Commands.Unban:
                        HandleUnbanCommand(dataBase);
                        break;

                    case Commands.Print:
                        HandlePrintCommand(dataBase);
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

        private static void HandleAddCommand(DataBase dataBase)
        {
            string request = "please enter a username: ";
            string name = AskForUserInput(request);

            request = "please enter player's level: ";
            bool isParsed = uint.TryParse(AskForUserInput(request), out uint level);

            if (isParsed)
            {
                try
                {
                    dataBase.AddPlayer(name, level);
                }
                catch (Exception e)
                {
                    PrintErrorMessage(e.Message);
                }
            }
            else
            {
                PrintErrorMessage("level must be integer greater than zero value");
            }
        }

        private static void HandleRemoveCommand(DataBase dataBase)
        {
            string request = "please enter player ID you wish to remove: ";

            uint playerID = AskUserForPlayerID(request);

            try
            {
                dataBase.RemovePlayer(playerID);
            }
            catch (Exception e)
            {
                PrintErrorMessage(e.Message);
            }
        }

        private static void HandleBanCommand(DataBase dataBase)
        {
            string request = "please enter player ID you wish to ban: ";

            uint playerID = AskUserForPlayerID(request);

            try
            {
                dataBase.BanPlayer(playerID);
            }
            catch (Exception e)
            {
                PrintErrorMessage(e.Message);
            }
        }

        private static void HandleUnbanCommand(DataBase dataBase)
        {
            string request = "please enter player ID you wish to unban: ";

            uint playerID = AskUserForPlayerID(request);

            try
            {
                dataBase.UnbanPlayer(playerID);
            }
            catch (Exception e)
            {
                PrintErrorMessage(e.Message);
            }
        }

        private static void HandlePrintCommand(DataBase dataBase)
        {
            Console.WriteLine();

            foreach (var player in dataBase.Players)
            {
                foreach (var property in player)
                {
                    Console.WriteLine(property);
                }
                Console.WriteLine();
            }
        }

        private static uint AskUserForPlayerID(string request)
        {
            bool isParsed = uint.TryParse(AskForUserInput(request), out uint playerID);

            if (isParsed)
            {
                return playerID;
            }
            else
            {
                PrintErrorMessage("player ID must be integer greater than zero value");
            }

            return AskUserForPlayerID(request);
        }
    }
}
