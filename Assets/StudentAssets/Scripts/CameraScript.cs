using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraScript : NetworkBehaviour {

    private Camera _camera;

    [SerializeField]
    private Vector3 _cameraPosition;

	void Start () {
        _camera = Camera.main;
	}

	void Update () {
		if (isLocalPlayer)
        {
            _camera.transform.position = this.transform.position + _cameraPosition;
        }
	}
}
