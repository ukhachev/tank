using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TankDamageable : NetworkBehaviour, IDamageable {
    [SerializeField]
    [SyncVar]
    private float _hp = 100;

    [SerializeField]
    private GameObject _destroyed;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private Slider _hpBar;

    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        _hpBar.transform.SetParent(GameObject.Find("Canvas").transform);
    }
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    public void Hit(int dmg)
    {
        
        if (!isServer)
        {
            return;
        }
        _hp -= dmg;
        
        if (_hp <= 0)
        {
            _hp -= dmg;
            var destroyed = ObjectPool.Instance.Take(
                        _destroyed, transform.position, transform.rotation);

            RpcDestroy();
            NetworkServer.Spawn(destroyed);
        }
        
    }
    
    [ClientRpc]
    void RpcDestroy()
    {
        Destroy(_hpBar.gameObject);
        Destroy(gameObject);
        var particles = ObjectPool.Instance.Take(_explosionPrefab,
                transform.position, new Quaternion(), 2f);
        particles.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
        _hpBar.value = _hp;
        var point = _camera.WorldToScreenPoint(transform.position) + new Vector3(0, Screen.width / 20);
        _hpBar.transform.position = point;
    }
 
}
