using System;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlLine
    {
        private string _line;
        public YamlLine(string line)
        {
            _line = line;

            Populate();
        }

        private void Populate()
        {
            var tokens = _line.Split(":");

            if (tokens.Length == 1)
            {
                PropertyName = tokens[0].Trim();
            }
            else if (tokens.Length == 2)
            {
                PropertyName = tokens[0].Trim();
                PropertyValue = tokens[1].Trim();
            }
        }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

    }
}
