using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using NUnit.Framework;
using QAToolKit.Core.Models;
using QAToolKit.Engine.Bombardier;
using QAToolKit.Engine.Bombardier.Models;

namespace SimpleMath.Web.Tests
{
    public class LoadTests : WebTestBase
    {
        private const int MaxParallelRequestsCount = 10;

        protected override void ModifyAppConfiguration(IApplicationBuilder app)
        {
            base.ModifyAppConfiguration(app);
            app.Use(async (_, next) =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1)); //slowing down server to simulate difficult job
                await next();
            });
        }

        [Test]
        // TODO this test is kinda random, need to fix this
        public async Task Return503_WhenLimitReached()
        {
            const int concurrentUsersCount = MaxParallelRequestsCount * 5;
            const int requestsCount = concurrentUsersCount * 5;

            var bombardierTest = CreateBombardierTest(concurrentUsersCount, requestsCount);

            var runResults = (await new BombardierTestsRunner(new[] {bombardierTest}).Run())
                .Single();
            PrintProgress(runResults.ToString());

            runResults.Counter5xx
                .Should()
                .BeInRange(1, requestsCount);

            runResults.Counter2xx
                .Should()
                .BeGreaterThan(0);
        }

        private BombardierTest CreateBombardierTest(int concurrentUsersCount, int totalRequestsCount)
        {
            var testsGenerator = new BombardierTestsGenerator(
                CreateRequest(),
                options =>
                {
                    options.BombardierConcurrentUsers = concurrentUsersCount;
                    options.BombardierNumberOfTotalRequests = totalRequestsCount;
                    options.BombardierInsecure = true;
                }
            );
            var bombardierTest = testsGenerator.Generate().Result.Single();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                bombardierTest.Command = bombardierTest.Command.Replace("&", "^&"); //screening HTTP-Query arg delimiter
            PrintProgress(bombardierTest.Command);
            return bombardierTest;
        }

        private static HttpRequest[] CreateRequest() => new[]
        {
            new HttpRequest
            {
                Method = HttpMethod.Get,
                BasePath = "https://localhost:5001",
                Path = "/math/calc",
                Description = "Addition operation",
                Summary = "Add left to right",
                Parameters = new List<Parameter>
                {
                    new Parameter {Location = Location.Query, Name = "LeftOperand", Value = 1.ToString()},
                    new Parameter {Location = Location.Query, Name = "RightOperand", Value = (-1).ToString()},
                    new Parameter {Location = Location.Query, Name = "OperationType", Value = "Add"}
                }
            }
        };
    }
}