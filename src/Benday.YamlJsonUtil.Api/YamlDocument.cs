using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlDocument : IYamlParent
    {
        public YamlDocument()
        {
            Children = new List<YamlElement>();
        }

        public YamlDocument(List<YamlLine> lines)
        {
            Children = new List<YamlElement>();

            var populator = new YamlDocumentPopulator(this, lines);

            populator.Populate();
        }

        public int LineNumber
        {
            get 
            {
                return 0;
            }
        }

        public YamlElement Parent
        {
            get
            {
                return null;
            }
            set
            {
                // nothing
                throw new InvalidOperationException($"Cannot set parent for document.");
            }
        }

        public List<YamlElement> Children { get; private set; }
    }
}
