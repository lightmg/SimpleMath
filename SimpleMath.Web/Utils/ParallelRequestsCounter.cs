using SimpleMath.Web.SettingsProviders;

namespace SimpleMath.Web.Utils
{
    public class ParallelRequestsCounter : IParallelRequestsCounter
    {
        private object locker = new();

        private readonly int limit;
        private int current;

        public ParallelRequestsCounter(IMaxRequestLimitProvider provider)
        {
            limit = provider.GetMaxRequestsLimit();
            if (limit <= 0)
                limit = int.MaxValue;
        }

        public bool TryIncrement()
        {
            lock (locker)
            {
                if (IsLimitReached)
                    return false;

                current++;
                return true;
            }
        }

        public void Decrement()
        {
            lock (locker)
            {
                current--;
            }
        }

        private bool IsLimitReached => current >= limit;
    }
}