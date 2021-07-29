using System.Collections.Generic;

namespace Benday.YamlJsonUtil.Api
{
    public interface IYamlParent
    {
        int LineNumber { get; }
        YamlElement Parent { get; set; }
        List<YamlElement> Children { get; }
    }
}
