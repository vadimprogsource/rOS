using System;
using System.Collections.Concurrent;
using System.Text;

namespace System.CoreLib.Lockers;

public class AsyncLocker : IDisposable, IAsyncDisposable
{

    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _cache = new();

    private readonly string _key;
    private readonly SemaphoreSlim m_handle;

    public AsyncLocker(string name, params object[] args)
    {

        StringBuilder key = new(name);

        foreach (object arg in args)
        {
            key.Append(arg.GetType().FullName);
            key.Append(arg.GetHashCode());
        }
        m_handle = _cache.GetOrAdd(_key = key.ToString(), x => new SemaphoreSlim(10));
    }

    public void Dispose() => m_handle.Release();

    public async Task LockAsync()
    {
        await Task.Delay(1);
        await m_handle.WaitAsync();
    }

    public ValueTask DisposeAsync()
    {
        m_handle.Release();
        return ValueTask.CompletedTask;
    }

    ~AsyncLocker()
    {
        if (_cache.Remove(_key, out SemaphoreSlim? handle))
        {
            handle?.Release();
        }

    }

}


public class AsyncLocker<T> : AsyncLocker
{
    public AsyncLocker(params object[] args) : base(typeof(T).FullName ?? "NULL_TYPE", args) { }
}