using Benday.YamlJsonUtil.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.YamlJsonUtil.UnitTests
{
    [TestClass]
    public class YamlParserFixture
    {
        private const string INDENT_STRING = "  ";
        [TestInitialize]
        public void OnTestInitialize()
        {
            _SystemUnderTest = null;
        }

        private YamlParser _SystemUnderTest;

        private YamlParser SystemUnderTest
        {
            get
            {
                Assert.IsNotNull(_SystemUnderTest, "SUT not initialized");

                return _SystemUnderTest;
            }
        }

        [TestMethod]
        public void Parse_RemovesCommentsAndBlankLines()
        {
            // arrange
            var yaml = GetYamlWithCommentsAndBlankLines();
            var expectedLineCount = 7;

            // act
            _SystemUnderTest = new YamlParser(yaml);

            // assert
            PrintLines();
            Assert.AreEqual<int>(expectedLineCount, SystemUnderTest.Lines.Count, "line count was wrong");
        }

        [TestMethod]
        public void ParsePopulatesStartsOfArray_NoArrays()
        {
            // arrange
            var yaml = GetYamlWithNoArrays();
            var expectedArrayStartCount = 0;

            // act
            _SystemUnderTest = new YamlParser(yaml);

            // assert
            var actualArrayStartCount = (from temp in _SystemUnderTest.Lines
                                         where temp.IsStartOfArray == true
                                         select temp).Count();

            Assert.AreEqual<int>(expectedArrayStartCount, actualArrayStartCount, "Array start count was wrong.");
        }

        [TestMethod]
        public void ParsePopulatesStartsOfArray_OneArray()
        {
            // arrange
            var yaml = GetYamlWithOneArray();
            var expectedArrayStartCount = 1;
            var expectedLineNumberForArray0 = 4;

            // act
            _SystemUnderTest = new YamlParser(yaml);

            // assert
            var actualArrayStarts = (from temp in _SystemUnderTest.Lines
                                     where temp.IsStartOfArray == true
                                     select temp).ToList();

            var actualArrayStartCount = actualArrayStarts.Count;

            Assert.AreEqual<int>(expectedLineNumberForArray0, actualArrayStarts[0].LineNumber, "Array start 0 line number is wrong");
            Assert.AreEqual<int>(expectedArrayStartCount, actualArrayStartCount, "Array start count was wrong.");
        }

        [TestMethod]
        public void ParsePopulatesStartsOfArray_OneArrayAsFirstElement()
        {
            // arrange
            var yaml = GetYamlWithOneArrayAsFirstElement();
            var expectedArrayStartCount = 1;
            var expectedLineNumberForArray0 = 0;

            // act
            _SystemUnderTest = new YamlParser(yaml);

            // assert
            var actualArrayStarts = (from temp in _SystemUnderTest.Lines
                                     where temp.IsStartOfArray == true
                                     select temp).ToList();

            var actualArrayStartCount = actualArrayStarts.Count;

            Assert.AreEqual<int>(expectedLineNumberForArray0, actualArrayStarts[0].LineNumber, "Array start 0 line number is wrong");
            Assert.AreEqual<int>(expectedArrayStartCount, actualArrayStartCount, "Array start count was wrong.");
        }

        [TestMethod]
        public void ParsePopulatesStartsOfArray_ThreeArrays()
        {
            // arrange
            var yaml = GetYamlWithThreeArrays();
            var expectedArrayStartCount = 3;
            var expectedLineNumberForArray0 = 4;
            var expectedLineNumberForArray1 = 8;
            var expectedLineNumberForArray2 = 15;

            // act
            _SystemUnderTest = new YamlParser(yaml);

            // assert
            var actualArrayStarts = (from temp in _SystemUnderTest.Lines
                                     where temp.IsStartOfArray == true
                                     select temp).ToList();

            var actualArrayStartCount = actualArrayStarts.Count;

            Assert.AreEqual<int>(expectedArrayStartCount, actualArrayStartCount, "Array start count was wrong.");
            Assert.AreEqual<int>(expectedLineNumberForArray0, actualArrayStarts[0].LineNumber, "Array start 0 line number is wrong");
            Assert.AreEqual<int>(expectedLineNumberForArray1, actualArrayStarts[1].LineNumber, "Array start 1 line number is wrong");
            Assert.AreEqual<int>(expectedLineNumberForArray2, actualArrayStarts[2].LineNumber, "Array start 2 line number is wrong");
        }

        public string GetYamlWithCommentsAndBlankLines()
        {
            var yaml = @"# this is a comment
              # this is a comment with leading spaces
Message: hi!

Values: 
  Property1: test
  Property2: 1234
  Property4: 
    Property4a: asdf
    Property4b: qwer
";

            return yaml;
        }


        private string GetYamlWithNoArrays()
        {
            var yaml = @"Message: hi!
Values: 
  Property1: test
  Property2: 1234
  Property4: 
    Property4a: asdf
    Property4b: qwer
";

            return yaml;
        }

        private string GetYamlWithOneArrayAsFirstElement()
        {
            var yaml = @"Property3: 
- one
- two
- three
Property4: 
  Property4a: asdf
  Property4b: qwer
";

            return yaml;
        }

        private string GetYamlWithOneArray()
        {
            var yaml = @"Message: hi!
Values: 
  Property1: test
  Property2: 1234
  Property3: 
  - one
  - two
  - three
  Property4: 
    Property4a: asdf
    Property4b: qwer
";

            return yaml;
        }

        private string GetYamlWithThreeArrays()
        {
            var yaml = @"Message: hi!
Values: 
  Property1: test
  Property2: 1234
  Property3: 
  - one
  - two
  - three
  Property5: 
  - one
  - two
  - three
  Property4: 
    Property4a: asdf
    Property4b: qwer
    Property4c: 
    - one
    - two
    - three
AnotherMessage: hola!
";

            return yaml;
        }

        [TestMethod]
        public void GetYamlDocument()
        {
            // arrange
            var yaml = GetYamlWithThreeArrays();
            _SystemUnderTest = new YamlParser(yaml);
            PrintLines();
            var expectedRootLevelItems = 3;

            // act
            var actual = SystemUnderTest.GetYamlDocument();

            // assert
            Assert.AreEqual<int>(expectedRootLevelItems,
                actual.Children.Count, "Child count was wrong.");

            AssertYamlElement(actual.Children, 0, "Message", 0, 0);
            AssertYamlElement(actual.Children, 1, "Values", 1, 5);
            AssertYamlElement(actual.Children, 2, "AnotherMessage", 19, 0);
        }

        private void PrintLines()
        {
            Console.WriteLine($"*************************");
            Console.WriteLine($"Line count: {SystemUnderTest.Lines.Count}");

            string lineText;

            foreach (var line in SystemUnderTest.Lines)
            {
                if (line.LineNumber < 10)
                {
                    lineText = $"00{line.LineNumber}: '{line.RawValue}'";
                }
                else if (line.LineNumber < 100)
                {
                    lineText = $"0{line.LineNumber}: '{line.RawValue}'";
                }
                else
                {
                    lineText = $"{line.LineNumber}: '{line.RawValue}'";
                }

                Console.Write(lineText);

                var lineTextWithPaddingLen = 50;
                var requiredPaddingLen = lineTextWithPaddingLen - lineText.Length;
                var padding = new string(' ', requiredPaddingLen);

                Console.Write(padding);

                Console.Write($"IsStartOfArray: {line.IsStartOfArray}");
                Console.Write($"\t\tIsArrayValue: {line.IsArrayValue}");
                Console.Write($"\t\tHasValue: {line.HasValue}");
                Console.WriteLine();
            }

            Console.WriteLine($"*************************");
        }

        private void AssertYamlElement(List<YamlElement> actuals, int index, string expectedName, int expectedLineNumber, int expectedChildCount)
        {
            var actual = actuals[index];

            AssertYamlElement(actual, expectedName, expectedLineNumber, expectedChildCount);
        }

        private void AssertYamlElement(YamlElement actual, string expectedName, int expectedLineNumber, int expectedChildCount)
        {
            Assert.AreEqual<string>(expectedName, actual.Line.Name, "Name");
            Assert.AreEqual<int>(expectedLineNumber, actual.Line.LineNumber, "Line number");
            Assert.AreEqual<int>(expectedChildCount, actual.Children.Count, "Child count");

            if (expectedChildCount == 0)
            {
                Assert.IsFalse(actual.HasChildren, "HasChildren was wrong.");
            }
            else
            {
                Assert.IsTrue(actual.HasChildren, "HasChildren was wrong.");
            }
        }
    }
}
