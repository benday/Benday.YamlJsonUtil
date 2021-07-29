using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.YamlJsonUtil.Api
{

    public class YamlDocument
    {
        public YamlDocument()
        {
            Children = new List<YamlElement>();
        }

        public YamlDocument(List<YamlLine> lines) 
        {
            Children = new List<YamlElement>();

            PopulateElements(lines);
        }

        private void PopulateElements(List<YamlLine> lines)
        {
            var rootLevelLines = (from temp in lines
            where temp.IndentCount == 0
            select temp);

            foreach (var line in rootLevelLines)
            {
                Children.Add(new YamlElement(line));
            }
        }

        public List<YamlElement> Children { get; private set; }
    }
}
