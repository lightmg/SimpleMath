using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic.Math.Calculators
{
    public interface IOperationCalculator
    {
        MathOperationType ValidFor { get; }
        IMathOperationResult Solve(MathOperation operation);
    }
}