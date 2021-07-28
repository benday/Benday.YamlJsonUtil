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
            // arrange
            var expectedName = "Message";
            var expectedValue = "hi!";
            var expectedIsArrayValue = false;

            // act
            _SystemUnderTest = new YamlLine("Message: hi!");

            // assert
            Assert.AreEqual<string>(expectedName, SystemUnderTest.Name, "Property name was wrong.");
            Assert.AreEqual<string>(expectedValue, SystemUnderTest.Value, "Property value was wrong.");
            Assert.AreEqual<bool>(expectedIsArrayValue, SystemUnderTest.IsArrayValue, "IsArrayValue value was wrong");
        }

        [TestMethod]
        public void ParseArrayValue()
        {
            // arrange
            string expectedName = null;
            var expectedValue = "one";
            var expectedIsArrayValue = true;

            // act
            _SystemUnderTest = new YamlLine("- one");

            // assert
            Assert.AreEqual<string>(expectedName, SystemUnderTest.Name, "Property name was wrong.");
            Assert.AreEqual<string>(expectedValue, SystemUnderTest.Value, "Property value was wrong.");
            Assert.AreEqual<bool>(expectedIsArrayValue, SystemUnderTest.IsArrayValue, "IsArrayValue value was wrong");
        }

    }
}
