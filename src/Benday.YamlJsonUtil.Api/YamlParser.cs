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
            PopulateArrayStarts();
        }

        private void PopulateArrayStarts()
        {
            var reversed = new List<YamlLine>(_lines);
            reversed.Reverse();

            bool isSearchingForStartOfArray = false;

            foreach (var line in reversed)
            {
                if (line.IsArrayValue == true)
                {
                    isSearchingForStartOfArray = true;
                }

                if (isSearchingForStartOfArray == true && line.IsArrayValue == false)
                {
                    line.IsStartOfArray = true;
                    isSearchingForStartOfArray = false;
                }
            }
        }

        public List<YamlLine> Lines { get => _lines; }

        private void PopulateLines(string yaml)
        {
            using var reader = new StringReader(yaml);

            _lines = new List<YamlLine>();

            var line = reader.ReadLine();

            var lineNumber = 0;

            while (line != null)
            {
                var temp = new YamlLine(line);

                _lines.Add(temp);
                temp.LineNumber = lineNumber;

                line = reader.ReadLine();
                lineNumber++;
            }
        }
    }
}
