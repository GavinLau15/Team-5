using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton for classes that are not MonoBehaviour.
/// </summary>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
                instance.InitializeSingleton();
            }
            return instance;
        }
    }

    protected virtual void InitializeSingleton() { }
}

/// <summary>
/// A mono instance is similar to a singleton, but instead of destroying any new
/// instances, it overrides the current instance. This is handy for resetting the
/// state and saves you doing it manually.
/// </summary>
public abstract class MonoInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// This transforms the mono instance into a basic singleton. This will destroy any
/// new versions created, leaving the original instance intact.
/// </summary>
public abstract class MonoSingleton<T> : MonoInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        base.Awake();
    }
}

/// <summary>
/// Persistent version of the mono singleton. This will survive through scene loads.
/// Perfect for system classes which require stateful, persistent data, or audio
/// sources where music plays through loading screens, etc.
/// </summary>
public abstract class MonoSingletonPersistent<T> : MonoSingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
