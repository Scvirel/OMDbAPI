using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMDbAPI.Components
{
    internal class JsonBehaviour
    {
        public static string JsonFileName { get { return _jsonFileName; } set { _jsonFileName = value; } }

        private static string _jsonFileName = "filmList.json";

        public static void SaveFilmsToJson(List<Film> list)
        {
            string jsonContent = JsonConvert.SerializeObject(list, Formatting.None);
            File.WriteAllText(JsonFileName, jsonContent);
        }

        public static void SaveFilmsToJson(string content)
        {
            File.WriteAllText(JsonFileName, content);
        }

        public static List<Film> LoadFilmsFromJson(string fileName)
        {
            var filmList = new List<Film>();

            using (StreamReader file = File.OpenText(fileName))
            {
                var serializer = new JsonSerializer();
                filmList = (List<Film>)serializer.Deserialize(file, typeof(List<Film>));
            }

            return filmList;
        }

        public static List<Film> LoadFilmsFromJson()
        {
            List<Film> filmList = new List<Film>();

            using (StreamReader file = File.OpenText(_jsonFileName))
            {
                try
                {
                    var serializer = new JsonSerializer();
                    filmList = (List<Film>)serializer.Deserialize(file, typeof(List<Film>));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return filmList;
        }
    }
}
