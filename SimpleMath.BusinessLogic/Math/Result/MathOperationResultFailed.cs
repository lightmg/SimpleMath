namespace SimpleMath.BusinessLogic.Math.Result
{
    public class MathOperationResultFailed : IMathOperationResult
    {
        public MathOperationResultFailed(MathOperation source, string? errorMessage = null)
        {
            Source = source;
            ErrorMessage = errorMessage;
        }

        public MathOperation Source { get; }
        public string? ErrorMessage { get; }
    }
}