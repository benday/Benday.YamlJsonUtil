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

            // act
            _SystemUnderTest = new YamlLine("Message: hi!");

            // assert
            Assert.AreEqual<string>(expectedName, SystemUnderTest.PropertyName, "Property name was wrong.");
            Assert.AreEqual<string>(expectedValue, SystemUnderTest.PropertyValue, "Property value was wrong.");
        }

    }
}
