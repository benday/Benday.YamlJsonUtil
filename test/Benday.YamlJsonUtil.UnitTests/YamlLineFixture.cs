using Benday.YamlJsonUtil.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlJsonUtil.UnitTests
{
    [TestClass]
    public class YamlLineFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _SystemUnderTest = null;
        }

        private YamlLine _SystemUnderTest;

        private YamlLine SystemUnderTest
        {
            get
            {
                Assert.IsNotNull(_SystemUnderTest, "SystemUnderTest not initialized");

                return _SystemUnderTest;
            }
        }

        [TestMethod]
        public void ParseProperty()
        {
            var line = "Message: hi!";
            var expectedName = "Message";
            var expectedValue = "hi!";
            var expectedIsArrayValue = false;
            var expectedHasValue = true;
            var expectedIndentValue = 0;

            ParseAndAssertLine(
                line,
                expectedName,
                expectedValue, 
                expectedIsArrayValue,
                expectedHasValue,
                expectedIndentValue);
        }

        [TestMethod]
        public void ParsePropertyForArray()
        {
            var line = "Message:";
            var expectedName = "Message";
            var expectedValue = string.Empty;
            var expectedIsArrayValue = false;
            var expectedHasValue = false;
            var expectedIndentValue = 0;

            ParseAndAssertLine(
                line,
                expectedName,
                expectedValue, 
                expectedIsArrayValue,
                expectedHasValue,
                expectedIndentValue);
        }

        [TestMethod]
        public void ParseArrayValue()
        {
            var line = "- one";
            string expectedName = null;
            var expectedValue = "one";
            var expectedIsArrayValue = true;
            var expectedHasValue = true;
            var expectedIndentValue = 0;

            ParseAndAssertLine(
                line,
                expectedName,
                expectedValue, 
                expectedIsArrayValue,
                expectedHasValue,
                expectedIndentValue);
        }

        private void ParseAndAssertLine(
            string line,
            string expectedName, 
            string expectedValue,
            bool expectedIsArrayValue, 
            bool expectedHasValue,
            int expectedIndentValue)
        {
            // arrange
            
            // act
            _SystemUnderTest = new YamlLine(line);

            // assert
            Assert.AreEqual<string>(expectedName, SystemUnderTest.Name, "Property name was wrong.");
            Assert.AreEqual<string>(expectedValue, SystemUnderTest.Value, "Property value was wrong.");
            Assert.AreEqual<bool>(expectedIsArrayValue, SystemUnderTest.IsArrayValue, "IsArrayValue value was wrong");
            Assert.AreEqual<bool>(expectedHasValue, SystemUnderTest.HasValue, "HasValue value was wrong");
            Assert.AreEqual<int>(expectedIndentValue, SystemUnderTest.IndentCount, "IndentCount value was wrong");
        }
    }
}
