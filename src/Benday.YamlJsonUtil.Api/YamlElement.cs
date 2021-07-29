using System.Collections.Generic;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlElement : IYamlParent
    {
        public YamlElement(YamlLine line)
        {
            Line = line ?? throw new System.ArgumentNullException(nameof(line));

            Children = new List<YamlElement>();
        }

        public bool HasChildren
        {
            get
            {
                if (Children.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int LineNumber
        {
            get 
            {
                return Line.LineNumber;
            }
        }

        public YamlLine Line { get; private set; }
        public List<YamlElement> Children { get; private set; }
        public YamlElement Parent { get; set; }
    }
}
