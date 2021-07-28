using System;
using System.IO;
using Benday.YamlJsonUtil.Api;

namespace Benday.YamlJsonUtil.ConsoleUi
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine($"Provide a command: json-to-yaml or yaml-to-json");
            }
            else if (args[0] == "json-to-yaml")
            {
                JsonToYaml();
            }
            else if (args[0] == "yaml-to-json")
            {
                YamlToJson();
            }
        }

        private static void JsonToYaml()
        {
            var command = new JsonToYamlCommand();

            var jsonAsString = ReadAppSettingsJsonAsString();

            command.Convert(jsonAsString);
        }

        private static void YamlToJson()
        {
            var command = new YamlToJsonCommand();

            var jsonAsString = ReadSampleYamlAsString();

            command.Convert(jsonAsString);
        }

        private static string ReadAppSettingsJsonAsString()
        {
            // return File.ReadAllText("/Users/benday/code/temp/Benday.YamlJsonUtil/src/Benday.YamlJsonUtil.ConsoleUi/appsettings.json");
            return File.ReadAllText("appsettings.json");
        }

        private static string ReadSampleYamlAsString()
        {
            // return File.ReadAllText("/Users/benday/code/temp/Benday.YamlJsonUtil/src/Benday.YamlJsonUtil.ConsoleUi/sample.yaml");
            return File.ReadAllText("sample.yaml");
        }
    }
}
