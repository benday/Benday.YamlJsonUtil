using System;
using System.Collections.Generic;
using System.IO;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlParser
    {
        private List<YamlLine> _lines;
        public YamlParser(string yaml)
        {
            if (yaml == null)
            {
                throw new ArgumentNullException(nameof(yaml), "Argument cannot be null.");
            }

            PopulateLines(yaml);
        }


        public List<YamlLine> Lines { get => _lines; }

        private void PopulateLines(string yaml)
        {
            using var reader = new StringReader(yaml);

            _lines = new List<YamlLine>();

            var line = reader.ReadLine();

            while (line != null)
            {
                _lines.Add(new YamlLine(line));

                line = reader.ReadLine();
            }
        }
    }
}
