using System;
using Microsoft.AspNetCore.Builder;
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
            host = Program.CreateHostBuilder(Array.Empty<string>(), ModifyAppConfiguration).Build();
            host.StartAsync();
        }

        [TearDown]
        public virtual void TearDown()
        {
            host.Dispose();
            PrintProgress("Host successfully disposed");
        }

        protected void PrintProgress(string message)
        {
            TestContext.Progress.WriteLine(message);
        }

        protected virtual void ModifyAppConfiguration(IApplicationBuilder app)
        {
        }
    }
}