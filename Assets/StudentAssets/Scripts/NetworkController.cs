using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkController : MonoBehaviour {
    private NetworkManager _netManager;

	// Use this for initialization
	void Start () {
        _netManager = NetworkManager.singleton;
        
        if (NetworkSettings.isServer)
        {
            _netManager.StartHost();
        }
        else
        {
            _netManager.networkAddress = NetworkSettings.IP;
            _netManager.networkPort = 7777;
            _netManager.StartClient();
        }
	}

	// Update is called once per frame
	void Update () {
		
	}
}
