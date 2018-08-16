using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Manager : Singleton<Manager>
{
    public List<GameObject> _players = new List<GameObject>();
    public List<GameObject> Players
    {
        get
        {
            return _players;
        }
    }
}

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            var objects = FindObjectsOfType<T>();

            if (objects.Length == 0) {
                var gameObject = Instantiate(new GameObject(typeof(T).Name.ToString()));
                _instance = gameObject.AddComponent<T>();
                return _instance;
            }
            if (objects.Length == 1)
            {
                _instance = objects[0];
                return _instance;
            }
            Debug.LogError("There can be only one " + typeof(T).Name.ToString());
            throw new System.Exception();
        }
    }
}