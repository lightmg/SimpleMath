using SimpleMath.BusinessLogic.Math;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic
{
    public interface IMathSolver
    {
        IMathOperationResult Solve(MathOperation operation);
    }
}