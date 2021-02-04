using SimpleMath.BusinessLogic.Math.Calculators;

namespace SimpleMath.BusinessLogic.Math
{
    public interface ICalculatorResolver
    {
        IOperationCalculator Resolve(MathOperationType operationType);
    }
}