using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace SimpleMath.Web.Tests
{
    public abstract class WebTestBase
    {
        private IHost host;

        [SetUp]
        public virtual void SetUp()
        {
            host = WebServer.CreateHost();
            host.StartAsync();
        }

        [TearDown]
        public virtual void TearDown()
        {
            host.Dispose();
            TestContext.Progress.WriteLine("Host successfully disposed");
        }
    }
}