/**
 * Coder: Gui Miranda, Joao Marques
 * Date: 06/05/2023
 */

namespace Anniversary_Minder
{
    /**
	 * Class Name: Program
	 * Purpose: Handles Anniversary Minder menu functionality
     */
    internal class Program
    {
        public const string JsonFile = @"../../../../anniversary.json";
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

                        FileHandler.WriteAnniversaryToJsonFile(anniversaryList, JsonFile);
                        cm.RedirectToMainMenu(anniversaryList);
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