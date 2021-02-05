using Microsoft.AspNetCore.Mvc;
using SimpleMath.BusinessLogic;
using SimpleMath.BusinessLogic.Math;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.Web.Controllers
{
    [Route("math")]
    [Controller]
    public class MathController
    {
        private readonly IMathSolver solver;

        public MathController(IMathSolver solver)
        {
            this.solver = solver;
        }

        [Route("calc")]
        [HttpGet, HttpPost]
        public IMathOperationResult Solve(MathOperation operation)
        {
            return solver.Solve(operation);
        }
    }
}