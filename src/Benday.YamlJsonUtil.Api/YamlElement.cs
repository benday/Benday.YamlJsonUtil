using System.Collections.Generic;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlElement
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

        public YamlLine Line { get; private set; }
        public List<YamlElement> Children { get; private set; }
    }
}
