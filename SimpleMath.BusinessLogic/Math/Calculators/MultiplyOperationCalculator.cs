namespace SimpleMath.BusinessLogic.Math.Calculators
{
    public class MultiplyOperationCalculator : OperationCalculatorBase
    {
        public override MathOperationType ValidFor => MathOperationType.Multiply;

        protected override double SolveOrThrow(int leftOperand, int rightOperand) =>
            checked(leftOperand * rightOperand);
    }
}