/**
 * Coder: Gui Miranda, Joao Marques
 * Date: 06/05/2023
 */

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
    /**
	 * Class Name: FileHandler
	 * Purpose: Handles File input from the user
     */
    public static class FileHandler
    {
        /*
         * Method Name: WriteAnniversaryToJsonFile
         * Purpose: Outputs the Anniversary List into a json file
         * Accepts: Anniversary List, json file path string
         * Returns: Nothing
         */
        public static void WriteAnniversaryToJsonFile(List<Anniversary> anniversaries, string path)
        {
            string json = JsonConvert.SerializeObject(anniversaries);
            File.WriteAllText(path, json);
        }

        /*
         * Method Name: ReadJsonToAnniversary
         * Purpose: Outputs a json file data into an Anniversary List
         * Accepts: Json file path string, json schema path
         * Returns: Anniversary List
         */
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
                                Console.WriteLine($"\nERROR:\tFile data is invalid.\n");

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

        /*
         * Method Name: ReadFile
         * Purpose: Attempts to read the json file specified by 'path' into the string 'json'
         * Accepts: File path string, json output string
         * Returns: True if successful or False if it fails
         */
        public static bool ReadFile(string path, out string json)
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

        /*
         * Method Name: ValidateAnniversaryData
         * Purpose: Validates Anniversary objects coming from json file
         * Accepts: A jsonData string, json schema string, messages List string
         * Returns: True if valid or False if not valid
         */
        public static bool ValidateAnniversaryData(in string jsonData, in string jsonSchema, out IList<string> messages)
        {
            JSchema schema = JSchema.Parse(jsonSchema);
            JObject anniv = JObject.Parse(jsonData);
            return anniv.IsValid(schema, out messages);
        }
    }
}
