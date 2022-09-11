namespace ForzaAdviser.Helpers
{
    public interface IFactory<T>
    {
        public T Create { get; }
    }
}