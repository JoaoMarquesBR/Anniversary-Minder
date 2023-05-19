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
                cm.printCommandOptions();
                Console.WriteLine("\n-------------------------------");
                string command = cm.getUserInput().ToLower();
                command = command.Replace(" ", "");
                switch (command)
                {
                    case "n":
                        cm.addNewAnniversary();
                        break;

                    case "u":
                        cm.listUpcomingAnniversary();
                        break;

                    case "x":
                        cm.quit();
                        break;

                    case "#":
                        cm.quit();
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}