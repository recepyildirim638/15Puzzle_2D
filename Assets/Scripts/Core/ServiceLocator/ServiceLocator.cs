using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();
    public static void Initalize() { Current = new ServiceLocator(); }
    public static ServiceLocator Current { get; private set; }

    public void Register<T>(T service) where T : IGameService
    {
        string key = typeof(T).Name;

        if (services.ContainsKey(key))
            return;

        services.Add(key, service);
    }

    public T Get<T>() where T : IGameService
    {
        string key = typeof(T).Name;

        if (!services.ContainsKey(key))
        {
            throw new InvalidOperationException();
        }

        return (T)services[key];
    }

    public void Unregister<T>() where T : IGameService
    {
        string key = typeof(T).Name;

        if (!services.ContainsKey(key))
        {
            return;
        }

        services.Remove(key);
    }
}
