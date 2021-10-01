using UnityEngine;
 
/// <summary>
/// Usage: 
/// public class MyClassName : Singleton<MyClassName> {}
/// </summary>
[DisallowMultipleComponent]
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Avoid dulpicated creation when called at the same time
    private static object _lock = new object();

    // Real instance
    private static T _instance;
 
    /// <summary>
    /// Instance.
    /// </summary>
    public static T Instance
    {
        get
        { 
            lock (_lock)
            {
                if (!_instance)
                {
                    // Search for existing instance
                    _instance = (T)FindObjectOfType(typeof(T));

                    // If cannot find, create one
                    if (!_instance)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"[SINGLETON] {typeof(T)}";
                    }
                }
 
                return _instance;
            }
        }
    }
    
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // _instance = null;
    }
}