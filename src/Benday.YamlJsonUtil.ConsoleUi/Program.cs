using System;
using System.IO;
using Benday.YamlJsonUtil.Api;

namespace Benday.YamlJsonUtil.ConsoleUi
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new JsonToYamlCommand();

            var jsonAsString = ReadAppSettingsJsonAsString();

            command.ConvertJsonToYaml(jsonAsString);
        }

        private static string ReadAppSettingsJsonAsString()
        {
            return File.ReadAllText("/Users/benday/code/temp/Benday.YamlJsonUtil/src/Benday.YamlJsonUtil.ConsoleUi/appsettings.json");
        }
    }
}
