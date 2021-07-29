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
            Console.WriteLine($"JsonToYaml()");

            var command = new JsonToYamlCommand();

            var input = ReadAppSettingsJsonAsString();

            command.Convert(input);
        }

        private static void YamlToJson()
        {
            Console.WriteLine($"YamlToJson()");

            var command = new YamlToJsonCommand();

            var input = ReadSampleYamlAsString();

            var output = command.Convert(input);

            Console.WriteLine($"Output length: {output.Length}");

            Console.WriteLine(output);
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
