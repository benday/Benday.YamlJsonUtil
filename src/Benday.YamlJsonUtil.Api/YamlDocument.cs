using System.Collections.Generic;

namespace Benday.YamlJsonUtil.Api
{

    public class YamlDocument
    {
        public YamlDocument()
        {
            Children = new List<YamlElement>();
        }

        public List<YamlElement> Children { get; private set; }
    }
}
