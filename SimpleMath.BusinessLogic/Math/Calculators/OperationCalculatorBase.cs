using System;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic.Math.Calculators
{
    public abstract class OperationCalculatorBase : IOperationCalculator
    {
        public abstract MathOperationType ValidFor { get; }

        public IMathOperationResult Solve(MathOperation operation)
        {
            if (!operation.LeftOperand.HasValue || !operation.RightOperand.HasValue)
                return FailWithMessage("Invalid operation: operands cannot be null");
            try
            {
                return new MathOperationResultSuccessful(operation,
                    SolveOrThrow(operation.LeftOperand.Value, operation.RightOperand.Value));
            }
            catch (OverflowException)
            {
                return FailWithMessage("Arithmetic overflow");
            }
            catch (DivideByZeroException)
            {
                return FailWithMessage("Divide by zero");
            }
            catch (Exception e)
            {
                return FailWithMessage(e.Message);
            }

            MathOperationResultFailed FailWithMessage(string message) => new(operation, message);
        }

        protected abstract double SolveOrThrow(int leftOperand, int rightOperand);
    }
}