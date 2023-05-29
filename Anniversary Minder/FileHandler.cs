using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


namespace Anniversary_Minder
{
    public class FileHandler
    {
        const string SchemaFile = @"../../../../anniversary_schema.json";

        public static void WriteLibToJsonFile(List<Anniversary> lib, string path)
        {
            string json = JsonConvert.SerializeObject(lib);
            File.WriteAllText(path, json);
        }

        //TODO -> figure out how to properly desearialize the Address (currently not working)
        public static List<Anniversary>? ReadJsonFileToLib(string path)
        {
            try
            {
                List<Anniversary> annivList = new List<Anniversary>();

                if (ReadFile(SchemaFile, out string jsonSchema))
                {
                    if (ReadFile(path, out string jsonData))
                    {
                        //have to cast it to a JArray so I can parse each object individually instead of the JSON Anniversary array
                        JArray annivArray = JArray.Parse(jsonData);
                        foreach (JObject annivObj in annivArray)
                        {
                            // Validate the json data against the schema
                            if (ValidateAnniversaryData(annivObj.ToString(), jsonSchema, out IList<string> messages))
                            {
                                annivList.Add(JsonConvert.DeserializeObject<Anniversary>(annivObj.ToString())!);
                            }
                            else
                            {
                                Console.WriteLine($"\nERROR:\tData file is invalid.\n");

                                // Report validation error messages
                                foreach (string msg in messages)
                                    Console.WriteLine($"\t{msg}");
                            }
                        }

                        return annivList;
                    }
                }

                return null;
            }
            catch
            {
                return new List<Anniversary>();
            }
        }

        // Attempts to read the json file specified by 'path' into the string 'json'
        // Returns 'true' if successful or 'false' if it fails
        private static bool ReadFile(string path, out string json)
        {
            try
            {
                // Read JSON file data 
                json = File.ReadAllText(path);
                return true;
            }
            catch
            {
                json = "";
                return false;
            }
        }

        //TODO -> still have to implement this when adding anniversary manually
        public static bool ValidateAnniversaryData(string jsonData, string jsonSchema, out IList<string> messages)
        {
            JSchema schema = JSchema.Parse(jsonSchema);
            JObject team = JObject.Parse(jsonData);
            return team.IsValid(schema, out messages);
        }
    }
}
