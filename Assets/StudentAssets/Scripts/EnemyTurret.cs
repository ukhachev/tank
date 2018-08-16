using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EnemyTurret : NetworkBehaviour {

    // Use this for initialization

    public Rigidbody Shell;
    public Transform Tur;

    [SerializeField]
    private float _reloadTime = 2f;

    private float _lastShotTime;
    void Start()
    {   
        _lastShotTime = Time.time;
    }
    // Update is called once per frame
    public void Fire()
    {
        
        if (isServer && Time.time - _lastShotTime >= _reloadTime)
        {
            RpcOnFire(Tur.position + Tur.forward * 1.8f, Tur.rotation, 30);
            _lastShotTime = Time.time;
        }
    }
    [ClientRpc]
    void RpcOnFire(Vector3 pos, Quaternion rot, float force)
    {
        Rigidbody shell = ObjectPool.Instance.Take(Shell.gameObject, pos, rot).GetComponent<Rigidbody>();
        shell.AddForce(shell.transform.forward * force);
    }
  
}
