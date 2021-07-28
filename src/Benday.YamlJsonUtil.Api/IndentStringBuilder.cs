using System.Text;

namespace Benday.YamlJsonUtil.Api
{
    public class IndentStringBuilder
    {
        private const char INDENT_CHAR = ' ';
        private int _indentCount = 0;
        private bool _needsIndentTokenOnLine = false;
        private readonly StringBuilder _builder = new();

        public void IncreaseIndent()
        {
            _indentCount++;
        }

        public void DecreaseIndent()
        {
            if (_indentCount > 0)
            {
                _indentCount--;
            }
        }

        public void Append(string text)
        {
            WriteIndentIfNeeded();

            _builder.Append(text);
        }

        private void WriteIndentIfNeeded()
        {
            if (_needsIndentTokenOnLine == true && _indentCount > 0)
            {
                _builder.Append(new string(INDENT_CHAR, _indentCount * 2));
                _needsIndentTokenOnLine = false;
            }
        }

        public void AppendLine()
        {
            _builder.AppendLine();

            _needsIndentTokenOnLine = true;
        }

        public void AppendLine(string text)
        {
            WriteIndentIfNeeded();

            _builder.AppendLine(text);

            _needsIndentTokenOnLine = true;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
