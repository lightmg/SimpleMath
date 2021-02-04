using SimpleMath.BusinessLogic.Math;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic
{
    public class MathSolver : IMathSolver
    {
        private readonly ICalculatorResolver calculatorResolver;

        public MathSolver(ICalculatorResolver calculatorResolver)
        {
            this.calculatorResolver = calculatorResolver;
        }

        public IMathOperationResult Solve(MathOperation operation) =>
            calculatorResolver
                .Resolve(operation.OperationType)
                .Solve(operation);
    }
}