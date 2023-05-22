namespace Anniversary_Minder
{
    internal class Program
    {
        const string JsonFile = @"..\..\..\..\anniversaries.json";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Commands cm = new Commands();
            
            while (true)
            {
                cm.PrintCommandOptions();
                Console.WriteLine("\n-------------------------------");
                string command = cm.GetUserInput().ToLower();
                command = command.Replace(" ", "");
                switch (command)
                {
                    case "n":
                        Anniversary anniversary =  cm.AddNewAnniversary();
                        Address address =  cm.AddNewAddress();
                        FileHandler.WriteLibToJsonFile(anniversary, JsonFile);
                        break;

                    case "u":
                        cm.ListUpcomingAnniversary();
                        break;

                    case "x":
                        cm.Quit();
                        break;

                    case "#":
                        
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}