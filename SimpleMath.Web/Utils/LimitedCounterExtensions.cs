using System;

namespace SimpleMath.Web.Utils
{
    public static class LimitedCounterExtensions
    {
        public static IDisposable TemporaryIncrement(this IParallelRequestsCounter counter)
        {
            counter.TryIncrement();

            return new ActionDisposable(counter.Decrement);
        }
    }
}