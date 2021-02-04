namespace SimpleMath.BusinessLogic.Math
{
    public record MathOperation(
        int? LeftOperand,
        int? RightOperand,
        MathOperationType OperationType
    );
}