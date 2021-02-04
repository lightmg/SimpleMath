namespace SimpleMath.BusinessLogic.Math.Calculators
{
    public class AddOperationCalculator : OperationCalculatorBase
    {
        public override MathOperationType ValidFor => MathOperationType.Add;

        protected override double SolveOrThrow(int leftOperand, int rightOperand) =>
            checked(leftOperand + rightOperand);
    }
}