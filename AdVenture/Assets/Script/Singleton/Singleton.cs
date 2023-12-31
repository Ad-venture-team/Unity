using System;

public class Singleton<T> where T : new()
{
    private static readonly Lazy<T> Lazy = new Lazy<T>(() => new T());

    public static T Instance => Lazy.Value;

    protected Singleton() { }
}
