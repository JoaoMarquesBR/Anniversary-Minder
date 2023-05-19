namespace Anniversary_Minder
{
    internal class Program
    {
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
                        cm.AddNewAnniversary();
                        break;

                    case "u":
                        cm.ListUpcomingAnniversary();
                        break;

                    case "x":
                        cm.Quit();
                        break;

                    case "#":
                        cm.Quit();
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}