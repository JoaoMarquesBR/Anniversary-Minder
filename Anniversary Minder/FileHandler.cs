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
        public static void WriteLibToJsonFile(List<Anniversary> anniversaries, string path)
        {
            string json = JsonConvert.SerializeObject(anniversaries);
            File.WriteAllText(path, json);
        }

        public static List<Anniversary>? ReadJsonToAnniversary(string path, in string SchemaFile)
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

        public static bool ValidateAnniversaryData(in string jsonData, in string jsonSchema, out IList<string> messages)
        {
            JSchema schema = JSchema.Parse(jsonSchema);
            JObject anniv = JObject.Parse(jsonData);
            return anniv.IsValid(schema, out messages);
        }
    }
}
