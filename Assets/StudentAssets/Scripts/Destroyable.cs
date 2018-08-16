using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UniRx;
public class Destroyable : NetworkBehaviour, IDamageable {

    [SerializeField]
    [SyncVar]
    private int _hp;
    
    [SerializeField]
    private GameObject _explosionPrefab;

    public void Hit(int hp)
    {
        _hp -= hp;
        if (_hp <= 0)
        {
            if (isServer)
            { 
                RpcDestroy();
            }
        }
    }

    [ClientRpc]
    private void RpcDestroy()
    {
        InstantiateParticles(_explosionPrefab, gameObject.transform);
        Destroy(gameObject);
    }
    
    private void InstantiateParticles(GameObject prefab, Transform tr)
    {
        var particles = ObjectPool.Instance.Take(prefab,
                tr.position, tr.rotation, 2.0f);

        particles.GetComponent<ParticleSystem>().Play();
    }

    void Update () {
		
	}
}
