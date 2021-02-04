using FluentAssertions;
using NUnit.Framework;
using SimpleMath.BusinessLogic.Math.Calculators;
using SimpleMath.BusinessLogic.Math.Result;

namespace SimpleMath.BusinessLogic.Tests
{
    // ReSharper disable once InconsistentNaming
    public class MultiplyOperationCalculator_Should : OperationCalculatorTestBase<MultiplyOperationCalculator>
    {
        public override void SetUp()
        {
            base.SetUp();
            Calculator = new MultiplyOperationCalculator();
        }

        [TestCase(int.MinValue, 1, int.MinValue, TestName = "Int.MinValue * 1")]
        [TestCase(int.MaxValue, 1, int.MaxValue, TestName = "Int.MaxValue * 1")]
        [TestCase(0, 1, 0, TestName = "With zero")]
        [TestCase(100, 2, 200, TestName = "Both positive")]
        [TestCase(100, -2, -200, TestName = "Positive with negative")]
        [TestCase(-100, -2, 200, TestName = "Both negative")]
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

        [TestCase(int.MinValue, 2, TestName = "Negative overflow")]
        [TestCase(int.MaxValue, 2, TestName = "Positive overflow")]
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