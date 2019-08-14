using System.IO;
using Barcabot.Common.DataModels;
using Barcabot.Common.DataModels.Config;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Barcabot.Common
{
    public static class YamlConfiguration
    {
        public static Config Config => GetConfig();

        private static string LoadConfigFile()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var mainDir = Directory.GetParent(currentDir).ToString();
            var path = Path.Combine(mainDir, "config.yaml");
    
            return File.ReadAllText(path);
        }

        private static Config GetConfig()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            return deserializer.Deserialize<Config>(LoadConfigFile());
        }
    }
}