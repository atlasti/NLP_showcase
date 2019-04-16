using NLog;
using Xunit;

namespace Tests
{
    public class HelloWorldTests
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [Theory]
        [InlineData(1970, "mcmlxx")]
        [InlineData(2019, "mmxix")]
        [InlineData(2005, "mmv")]
        [InlineData(21, "xxi")]
        private void TestRoman(int value, string roman)
        {
            var expected = $"{roman}.\t";
            var returned = value.ToString();
            Assert.NotEqual(expected, returned);
        }

        [Fact]
        private void BasicTest()
        {
            Assert.True(true);
        }
    }
}
