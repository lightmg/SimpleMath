using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using QAToolKit.Core.Models;
using QAToolKit.Engine.Bombardier;

namespace SimpleMath.Web.Tests
{
    public class LoadTests : WebTestBase
    {
        private const int MaxParallelRequestsCount = 100;

        [Test]
        public void Return503_WhenLimitReached()
        {
            var parallelRequestsCount = MaxParallelRequestsCount + 10;
            var testsGenerator = new BombardierTestsGenerator(CreateRequests(parallelRequestsCount), options =>
                {
                    options.BombardierConcurrentUsers = parallelRequestsCount;
                    options.BombardierNumberOfTotalRequests = parallelRequestsCount;
                    options.BombardierInsecure = true;
                }
            );

            var bombardierTest = testsGenerator.Generate().Result.First();
            var runResult = new BombardierTestsRunner(new[] {bombardierTest}).Run().Result.ToArray();
        }

        private static IEnumerable<HttpRequest> CreateRequests(int count) =>
            Enumerable.Range(1, count)
                .Select(i => new HttpRequest
                {
                    Method = HttpMethod.Get,
                    BasePath = "https://localhost:5001",
                    Path = "/math/calc",
                    Description = "Addition operation",
                    Summary = "Add left to right",
                    Parameters = new List<Parameter>
                    {
                        new Parameter {Location = Location.Query, Name = "LeftOperand", Value = i.ToString()},
                        new Parameter {Location = Location.Query, Name = "RightOperand", Value = (-i).ToString()},
                        new Parameter {Location = Location.Query, Name = "OperationType", Value = "Add"}
                    }
                })
                .ToArray();
    }
}