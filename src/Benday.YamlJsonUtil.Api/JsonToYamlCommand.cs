using System;
using System.Dynamic;
using System.Text.Json;

namespace Benday.YamlJsonUtil.Api
{
    public class JsonToYamlCommand
    {
        public void ConvertJsonToYaml(string jsonAsString)
        {
            if (jsonAsString == null)
            {
                throw new ArgumentNullException(nameof(jsonAsString), "Argument cannot be null.");
            }

            using var doc = JsonDocument.Parse(jsonAsString);

            var element = doc.RootElement;

            var builder = new IndentStringBuilder();

            WriteElement(builder, element);

            Console.WriteLine($"{builder}");
        }

        private void WriteElement(IndentStringBuilder builder, JsonElement element)
        {
            JsonElement.ObjectEnumerator properties = element.EnumerateObject();
            WriteProperties(builder, properties);
        }

        private void WriteProperties(IndentStringBuilder builder, JsonElement.ObjectEnumerator properties)
        {
            foreach (var prop in properties)
            {
                if (prop.Value.ValueKind == JsonValueKind.String ||
                    prop.Value.ValueKind == JsonValueKind.Number)
                {
                    AppendProperty(builder, prop);
                    builder.AppendLine();
                }
                else if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    AppendPropertyName(builder, prop);                    
                    builder.AppendLine();
                    builder.IncreaseIndent();

                    WriteProperties(builder, prop.Value.EnumerateObject());

                    builder.DecreaseIndent();
                }
                else if (prop.Value.ValueKind == JsonValueKind.Array)
                {
                    AppendPropertyName(builder, prop);
                    builder.AppendLine();

                    WriteArrayValues(builder, prop.Value.EnumerateArray());
                }
            }
        }

        private void WriteArrayValues(IndentStringBuilder builder, JsonElement.ArrayEnumerator arrayEnumerator)
        {
            foreach (var item in arrayEnumerator)
            {
                builder.Append("- ");
                builder.AppendLine(item.ToString());
            }
        }

        private void AppendPropertyName(IndentStringBuilder builder, JsonProperty prop)
        {
            builder.Append(prop.Name);
            builder.Append(": ");
        }

        private void AppendProperty(IndentStringBuilder builder, JsonProperty prop)
        {
            AppendPropertyName(builder, prop);

            builder.Append(prop.Value.ToString());
        }
    }
}
