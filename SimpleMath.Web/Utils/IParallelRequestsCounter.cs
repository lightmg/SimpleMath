namespace SimpleMath.Web.Utils
{
    public interface IParallelRequestsCounter
    {
        bool TryIncrement();
        void Decrement();
    }
}