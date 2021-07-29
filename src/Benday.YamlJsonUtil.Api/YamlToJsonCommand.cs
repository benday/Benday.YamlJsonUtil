using System;
using System.Text.Json;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlToJsonCommand
    {
        public void Convert(string yamlAsString)
        {
            var parser = new YamlParser(yamlAsString);

            var doc = JsonDocument.Parse("{}");
            var root = doc.RootElement;

            // Write(yamlDoc, root);
        }

        
    }
}
