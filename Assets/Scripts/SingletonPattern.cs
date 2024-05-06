using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPattern<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                {
                    _instance = objs[0];
                }
                if(objs.Length > 1)
                {
                    Debug.LogError("Hay mas de un " + typeof(T).Name + " en la escena");
                }

                if (_instance == null)
                {

                    GameObject obj = new GameObject();
                    //obj.name = typeof(T).Name;
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();

                }
            }
            
            return _instance;
        }
    }

}
