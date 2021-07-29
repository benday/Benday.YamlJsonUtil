using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlToJsonCommand
    {
        public string Convert(string yamlAsString)
        {
            var parser = new YamlParser(yamlAsString);
            var yamlDoc = parser.GetYamlDocument();

            using var stream = new MemoryStream();
            using var jsonWriter = new Utf8JsonWriter(stream, options: new JsonWriterOptions() {
                Indented = true
            });

            jsonWriter.WriteStartObject();

            Write(yamlDoc.Children, jsonWriter);

            jsonWriter.WriteEndObject();

            jsonWriter.Flush();

            var writeThis = stream.ToArray();
            var json = Encoding.UTF8.GetString(writeThis);

            return json;
        }

        private void Write(List<YamlElement> children, Utf8JsonWriter writer)
        {
            foreach (var item in children)
            {
                Write(item, writer);
            }
        }

        private void Write(YamlElement fromValue, Utf8JsonWriter writer)
        {
            if (fromValue.Line.HasValue == true)
            {
                writer.WriteString(fromValue.Line.Name, fromValue.Line.Value);
            }
            else if (fromValue.Line.IsStartOfArray == false &&
                fromValue.Line.HasValue == false &&
                fromValue.HasChildren == true)
            {
                writer.WriteStartObject(fromValue.Line.Name);
                Write(fromValue.Children, writer);
                writer.WriteEndObject();
            }
            else if (fromValue.Line.IsStartOfArray == true)
            {
                writer.WriteStartArray(fromValue.Line.Name);

                foreach (var childArrayItem in fromValue.Children)
                {
                    writer.WriteStringValue(childArrayItem.Line.Value);
                }

                writer.WriteEndArray();
            }
        }
    }
}
