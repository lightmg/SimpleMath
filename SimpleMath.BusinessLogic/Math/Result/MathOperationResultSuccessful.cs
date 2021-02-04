namespace SimpleMath.BusinessLogic.Math.Result
{
    public class MathOperationResultSuccessful : IMathOperationResult
    {
        public MathOperationResultSuccessful(MathOperation source, double calculated)
        {
            Source = source;
            Calculated = calculated;
        }

        public MathOperation Source { get; }
        public double Calculated { get; }
    }
}