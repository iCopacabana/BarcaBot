using System.Collections.Generic;
using System.IO;
using Barcabot.Common.DataModels;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Barcabot.Common
{
    public static class NameConverter
    {
        public static Dictionary<string, string> Names => GetNames();
        
        private static string LoadNamesFile()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var mainDir = Directory.GetParent(currentDir).ToString();
            var pathToContainingDir = Path.Combine(mainDir, "ImageManipulationMicroservice");
            var pathToFile = Path.Combine(pathToContainingDir, "names.yaml");
    
            return File.ReadAllText(pathToFile);
        }

        private static Dictionary<string, string> GetNames()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            var deserializedObj = deserializer.Deserialize<NamesYaml>(LoadNamesFile());

            return deserializedObj.Names;
        }

        public static string ConvertName(string name)
        {
            return Names.ContainsKey(name) ? Names[name] : name;
        }
    }
}