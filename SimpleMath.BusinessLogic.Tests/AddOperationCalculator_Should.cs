using FluentAssertions;
using NUnit.Framework;
using SimpleMath.BusinessLogic.Math.Calculators;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic.Tests
{
    // ReSharper disable once InconsistentNaming
    public class AddOperationCalculator_Should : OperationCalculatorTestBase<AddOperationCalculator>
    {
        public override void SetUp()
        {
            base.SetUp();
            Calculator = new AddOperationCalculator();
        }

        [TestCase(int.MinValue, 1, int.MinValue + 1, TestName = "Int.MinValue + 1")]
        [TestCase(int.MaxValue, -1, int.MaxValue - 1, TestName = "Int.MaxValue - 1")]
        [TestCase(int.MinValue, 0, int.MinValue, TestName = "Int.MinValue + 0")]
        [TestCase(int.MaxValue, 0, int.MaxValue, TestName = "Int.MaxValue - 0")]
        [TestCase(0, 1, 1, TestName = "With zero")]
        [TestCase(100, 1, 101, TestName = "Positive only")]
        [TestCase(-100, 100, 0, TestName = "With negative")]
        public void Successful(int leftOperand, int rightOperand, int answer)
        {
            Solve(leftOperand, rightOperand)
                .Should()
                .BeOfType<MathOperationResultSuccessful>()
                .Which
                .Calculated
                .Should()
                .Be(answer);
        }

        [TestCase(int.MinValue, -1, TestName = "Negative overflow")]
        [TestCase(int.MaxValue, 1, TestName = "Positive overflow")]
        public void Fail_ArithmeticOverflow(int leftOperand, int rightOperand)
        {
            Solve(leftOperand, rightOperand)
                .Should()
                .BeOfType<MathOperationResultFailed>()
                .Which
                .ErrorMessage
                .Should()
                .Be("Arithmetic overflow");
        }
    }
}