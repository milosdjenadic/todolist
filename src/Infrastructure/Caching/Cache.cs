using Assignment.Application.Common.Interfaces;

namespace Assignment.Infrastructure.Caching;
public class Cache : ICache
{
    private record class CachedValue(object? Value, DateTime Timestamp);
    private static readonly Dictionary<string, CachedValue> dictionary = new();
    
    public void AddValue<T>(string key, T value)
    {
        dictionary.Add(key, new CachedValue(value, DateTime.UtcNow));
    }

    public T? GetValue<T>(string key, DateTime timeStamp)
    {
        if (!dictionary.TryGetValue(key, out CachedValue? value))
        {
            return default;
        }

        if (value.Value != null && value.Value is not T)
        {
            throw new Exception("Value is not of correct type.");
        }

        if (value.Timestamp < timeStamp)
        {
            dictionary.Remove(key);
            return default;
        }

        return (T?)value.Value;
    }
}
