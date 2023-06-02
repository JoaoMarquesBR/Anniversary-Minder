﻿namespace Anniversary_Minder
{
    internal class Program
    {
        private const string JsonFile = @"../../../../anniversary.json";
        private const string SchemaFile = @"../../../../anniversary_schema.json";

        static void Main(string[] args)
        {
            Commands cm = new Commands();

            cm.DisplayHeader("All Anniversaries");

            List<Anniversary>? anniversaryList = cm.GetAnniversaries(JsonFile, SchemaFile);
            cm.DisplayAnniversaries(anniversaryList);

            cm.DisplayMainOptions();

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
                        cm.ListUpcomingAnniversary(anniversaryList);
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