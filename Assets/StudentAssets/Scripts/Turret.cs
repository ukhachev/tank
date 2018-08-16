using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Turret : NetworkBehaviour {

  
    private Camera _camera;
    public Rigidbody Shell;
    public Transform TurretObject;

    void Start()
    {
        _camera = Camera.main;
    }
 
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            TurretObject.LookAt(hit.point);
        }
    }
    [Command]
    void CmdFire()
    {
        RpcFire(TurretObject.position + TurretObject.forward * 1.8f, TurretObject.rotation);
        
    }
    [ClientRpc]
    void RpcFire(Vector3 position, Quaternion rotation)
    {
        var shell = ObjectPool.Instance.Take(Shell.gameObject, position, rotation);
        shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * 0.3f, ForceMode.Impulse);
    }

    void Update () {
        
        if (Input.GetMouseButtonDown(0) && isLocalPlayer)
        {
            CmdFire();
        }
    }
}
