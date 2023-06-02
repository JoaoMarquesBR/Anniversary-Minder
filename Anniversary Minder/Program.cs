namespace Anniversary_Minder
{
    internal class Program
    {
        const string JsonFile = @"../../../../anniversary.json";

        static void Main(string[] args)
        {
            Commands cm = new Commands();
            
            while (true)
            {
                Console.WriteLine("-----------------------------------------------------------------------\n");
                Console.WriteLine("\t\tANNIVERSARY MINDER ~ All Anniversaries");
                Console.WriteLine("\n-----------------------------------------------------------------------");
                cm.DisplayAnniversaries(JsonFile);
                cm.PrintCommandOptions();
                Console.WriteLine("\n-----------------------------------------------------------------------");

                string command = cm.GetUserInput().ToLower();
                command = command.Replace(" ", "");

                switch (command)
                {
                    case "n":
                        Anniversary anniversary =  cm.AddAnniversary();
                        Address address =  cm.AddAddress();
                        anniversary.Address = address;

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