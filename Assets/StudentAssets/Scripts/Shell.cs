using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
public class Shell : NetworkBehaviour {

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        var particles = ObjectPool.Instance.Take(_explosionPrefab,
                transform.position, transform.rotation, 2f);
        
            
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Hit(_damage);
        }
        
        particles.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;


    }
}
