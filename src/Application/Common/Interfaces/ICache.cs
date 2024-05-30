namespace Assignment.Application.Common.Interfaces;
public interface ICache
{
    void AddValue<T>(string key, T value);

    T? GetValue<T>(string key, DateTime timeStamp);
}
