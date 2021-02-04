using FluentAssertions;
using NUnit.Framework;
using SimpleMath.BusinessLogic.Math;
using SimpleMath.BusinessLogic.Math.Calculators;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic.Tests
{
    public abstract class OperationCalculatorTestBase<TCalculator> where TCalculator : OperationCalculatorBase
    {
        protected TCalculator Calculator;

        [SetUp]
        public virtual void SetUp()
        {
        }

        [TestCase(null, 10, TestName = "Left operand is null")]
        [TestCase(10, null, TestName = "Right operand is null")]
        [TestCase(null, null, TestName = "Both operands is null")]
        public void Fail_InvalidOperation(int? leftOperand, int? rightOperand)
        {
            Solve(leftOperand, rightOperand)
                .Should()
                .BeOfType<MathOperationResultFailed>();
        }

        protected IMathOperationResult Solve(int? leftOperand, int? rightOperand) =>
            Calculator.Solve(new MathOperation(leftOperand, rightOperand, Calculator.ValidFor));
    }

    // ReSharper disable once InconsistentNaming
}