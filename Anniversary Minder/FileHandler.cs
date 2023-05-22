using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anniversary_Minder
{
    public class FileHandler
    {
        public static void WriteLibToJsonFile(Anniversary lib, string path)
        {
            string json = JsonConvert.SerializeObject(lib);
            File.WriteAllText(path, json);
        }
    }
}
