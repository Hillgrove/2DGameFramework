namespace _2DGameFramework.Factories
{
    public interface IFactory<T>
    {
        void Register(string key, Func<T> creator);
        T Create(string key);
        IEnumerable<string> RegisteredKeys { get; }
    }

    public class Factory<T> : IFactory<T>
    {
        private readonly Dictionary<string, Func<T>> _creators = new();

        public void Register(string key, Func<T> creator)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (key == string.Empty)
                throw new ArgumentException("Key cannot be empty", nameof(key));

            ArgumentNullException.ThrowIfNull(creator);

            _creators[key] = creator;
        }

        public T Create(string key)
        {
            if (!_creators.TryGetValue(key, out var creator))
                throw new KeyNotFoundException($"No creator for key '{key}'");

            return creator();
        }

        public IEnumerable<string> RegisteredKeys => _creators.Keys;
    }
}
