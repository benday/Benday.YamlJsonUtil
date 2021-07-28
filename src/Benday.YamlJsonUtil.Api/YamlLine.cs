using System;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlLine
    {
        private string _line;
        public YamlLine(string line)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line), "Argument cannot be null.");

            Populate();
        }

        private void Populate()
        {
            var trimmed = _line.Trim();

            if (trimmed.StartsWith("- "))
            {
                IsArrayValue = true;
                Name = null;
                Value = trimmed[2..];
            }
            else
            {
                IsArrayValue = false;

                var tokens = _line.Split(":");

                if (tokens.Length == 1)
                {
                    Name = tokens[0].Trim();
                }
                else if (tokens.Length == 2)
                {
                    Name = tokens[0].Trim();
                    Value = tokens[1].Trim();
                }
            }
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsArrayValue { get; set; }
    }
}
