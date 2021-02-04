using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMath.BusinessLogic.Math.Calculators;

namespace SimpleMath.BusinessLogic.Math
{
    public class CalculatorResolver : ICalculatorResolver
    {
        private readonly Dictionary<MathOperationType, IOperationCalculator> resolvers;

        public CalculatorResolver(IEnumerable<IOperationCalculator> resolvers)
        {
            this.resolvers = resolvers.ToDictionary(r => r.ValidFor);
        }

        public IOperationCalculator Resolve(MathOperationType operationType) =>
            resolvers.TryGetValue(operationType, out var resolver)
                ? resolver
                : throw new ArgumentException($"{nameof(IOperationCalculator)} for {operationType} is not defined");
    }
}