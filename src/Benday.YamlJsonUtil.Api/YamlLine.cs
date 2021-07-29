using System;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlLine
    {
        private readonly string _line;
        public YamlLine(string line)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line), "Argument cannot be null.");

            Populate();
        }

        private void Populate()
        {
            PopulateIndent();

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

            HasValue = !string.IsNullOrWhiteSpace(Value);
        }

        private void PopulateIndent()
        {
            var startTrimmed = _line.TrimStart();

            if (startTrimmed == _line)
            {
                IndentCount = 0;
            }
            else
            {
                var amountTrimmed = _line.Length - startTrimmed.Length;

                IndentCount = amountTrimmed / 2;
            }
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
        public bool IsArrayValue { get; private set; }
        public bool HasValue { get; private set; }
        public int IndentCount { get; private set; }
    }
}
