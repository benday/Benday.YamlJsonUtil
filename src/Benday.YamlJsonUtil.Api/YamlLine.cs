namespace Benday.YamlJsonUtil.Api
{
    public class YamlLine
    {
        private string _line;
        public YamlLine(string line)
        {
            _line = line;
        }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        
    }
}
