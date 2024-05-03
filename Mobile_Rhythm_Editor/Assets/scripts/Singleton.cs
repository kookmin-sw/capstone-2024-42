using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }

            if(instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                instance = obj.AddComponent<T>();
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        Init_Singleton();
    }

    protected virtual void Init_Singleton()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
