using Xunit.Abstractions;

namespace UnitTests
{
    public abstract class TestWithOutput
    {
        protected readonly ITestOutputHelper output;

        public TestWithOutput(ITestOutputHelper output)
        {
            this.output = output;
        }
    }
}
