using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class ObjectPool : Singleton<ObjectPool> {
    private readonly Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();

    //Добавляет инстансы в пул
    public GameObject AddInstance(GameObject prefab, int count = 1)
    {
        if (!_pool.ContainsKey(prefab))
        {
            var newInstanceList = new List<GameObject>();
            _pool.Add(prefab, newInstanceList);

            for (int i = 0; i < count; ++i)
            {
                var instance = Instantiate(prefab);
                Return(instance);
                newInstanceList.Add(instance);
            }
            return newInstanceList[0];
        }

        var instanceList = _pool[prefab];

        for (int i = 0; i < count; ++i)
        {
            var instance = Instantiate(prefab);
            Return(instance);
            instanceList.Add(instance);
        }
        return instanceList[0];
    }

    //Забирает инстанстанс из пула
    public GameObject Take(GameObject prefab)
    {
        List<GameObject> instanceList;
        if (!_pool.ContainsKey(prefab))
        {
            instanceList = new List<GameObject>();
            _pool.Add(prefab, instanceList);
        }
        else
        {
            instanceList = _pool[prefab];
        }
        
        foreach (var instance in instanceList)
        {
            if (instance == null)
            {
                Debug.LogErrorFormat("Object from pool has been destroyed ({0}). Use SetActive(false). ", prefab.name);
                continue;
            }
            if (!instance.activeSelf)
            {
                    instance.SetActive(true);
                    return instance;
            }
        }

        var newInstance = AddInstance(prefab);
        Debug.Log("New instance created: " + prefab.name);
        newInstance.SetActive(true);
        return newInstance;
    }

    public GameObject Take(GameObject prefab, Transform transform)
    {
        var gameObject = Take(prefab);
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
        return gameObject;
    }

    public GameObject Take(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var gameObject = Take(prefab);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        return gameObject;
    }

    public GameObject Take(GameObject prefab, Vector3 position, Quaternion rotation, float returnTime)
    {
        var gameObject = Take(prefab, position, rotation);
        Observable.Timer(System.TimeSpan.FromSeconds(returnTime)).Subscribe(t => gameObject.SetActive(false));
        return gameObject;
    }

    public void Return(GameObject target)
    {
        target.SetActive(false);
    }

}

