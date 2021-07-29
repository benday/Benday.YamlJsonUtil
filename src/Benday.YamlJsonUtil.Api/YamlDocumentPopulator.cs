using System;
using System.Collections.Generic;

namespace Benday.YamlJsonUtil.Api
{
    public class YamlDocumentPopulator
    {
        private readonly List<YamlElement> _lines;
        private readonly YamlDocument _doc;
        private int _index = 0;
        private readonly int _lineCount;
        private IYamlParent _currentParent;
        private bool _verbose;

        public YamlDocumentPopulator(YamlDocument doc, List<YamlLine> lines,
            bool verbose = false)
        {
            _verbose = verbose;
            _lines = ToElements(lines) ?? throw new ArgumentNullException(nameof(lines));
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
            _lineCount = _lines.Count;
        }

        private List<YamlElement> ToElements(List<YamlLine> lines)
        {
            var returnValues = new List<YamlElement>();

            foreach (var line in lines)
            {
                returnValues.Add(new YamlElement(line));
            }

            return returnValues;
        }

        public void Populate()
        {
            _currentParent = _doc;

            MoveFirst();

            while (Current != null)
            {
                if (Previous != null)
                {
                    if (Current.Line.IndentCount > Previous.Line.IndentCount)
                    {
                        _currentParent = Previous;
                    }
                    else if (Previous.Line.IndentCount > Current.Line.IndentCount)
                    {
                        if (Current.Line.IndentCount == 0)
                        {
                            _currentParent = _doc;
                        }
                        else
                        {
                            _currentParent = Previous.Parent;
                        }
                    }
                    else if (
                        Previous.Line.IsStartOfArray == true &&
                        Current.Line.IsArrayValue == true)
                    {
                        _currentParent = Previous;
                    }
                    else if (Previous.Line.IsArrayValue == true &&
                        Current.Line.IsArrayValue == false)
                    {
                        _currentParent = Previous.Parent.Parent;
                    }
                }

                if (_currentParent == null)
                {
                    throw new InvalidOperationException($"Nope.");
                }
                else
                {
                    if (_verbose == true)
                    {
                        Console.WriteLine(
                            $"Adding child line # {Current.Line.LineNumber} to parent line # {_currentParent.LineNumber}");
                    }
                    
                    Current.Parent = _currentParent as YamlElement;
                    _currentParent.Children.Add(Current);
                }

                MoveNext();
            }
        }

        private YamlElement Current
        {
            get
            {
                return TryGetLine(_index);
            }
        }

        private YamlElement Previous
        {
            get
            {
                return TryGetLine(_index - 1);
            }
        }

        private YamlElement Next
        {
            get
            {
                return TryGetLine(_index + 1);
            }
        }

        private YamlElement TryGetLine(int index)
        {
            if (IsInRange(index) == false)
            {
                return null;
            }
            else
            {
                return _lines[index];
            }
        }

        private bool IsInRange(int desiredIndex)
        {
            if (_lineCount == 0)
            {
                return false;
            }
            else
            {
                if (desiredIndex < 0)
                {
                    return false;
                }
                else if (desiredIndex >= _lineCount)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void MoveFirst()
        {
            _index = 0;
        }

        private void MoveNext()
        {
            _index++;
        }

        private void MovePrevious()
        {
            _index--;

            if (_index < 0)
            {
                _index = -1;
            }
        }
    }
}
