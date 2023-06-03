namespace Anniversary_Minder
{
    internal class Program
    {
        private const string JsonFile = @"../../../../anniversary.json";
        private const string SchemaFile = @"../../../../anniversary_schema.json";

        static void Main(string[] args)
        {
            Commands cm = new Commands();

            List<Anniversary>? anniversaryList = cm.GetAnniversaries(JsonFile, SchemaFile);
            cm.RedirectToMainMenu(anniversaryList);

            while (true)
            {
                string command = cm.GetUserInput().ToLower();
                command = command.Replace(" ", "");

                switch (command)
                {
                    case "n":
                        Anniversary anniversary = cm.AddAnniversary(SchemaFile);
                        anniversaryList.Add(anniversary);

                        FileHandler.WriteLibToJsonFile(anniversaryList, JsonFile);
                        break;

                    case "u":
                        cm.DisplayUpcomingAnniversary(anniversaryList);
                        break;

                    case "x":
                        cm.Quit();
                        break;

                    case "#":
                        cm.DisplaySelectedAnniversary(anniversaryList);
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}