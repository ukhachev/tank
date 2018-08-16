using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Networking;

public class TankControls : NetworkBehaviour
{
	public float Speed;
	public float TurnSpeed;

	private float _forwardAxis;
	private float _sideAxis;

	private Rigidbody _rigidbody;
	
	void Start ()
	{
        Manager.Instance.Players.Add(gameObject);
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
        if (isLocalPlayer)
        {
            Move();
            Turn();
        }
		
	}

    void Move()
	{
		var shift = Speed * transform.forward * Time.deltaTime * _forwardAxis;
		_rigidbody.MovePosition(_rigidbody.position + shift);
	}

	void Turn()
	{
		var turn = TurnSpeed * Time.deltaTime * _sideAxis;
		var turnY = Quaternion.Euler(0, turn, 0);
		
		_rigidbody.MoveRotation(_rigidbody.rotation * turnY);
	}


	void Update ()
	{
        _forwardAxis = Input.GetAxis("Vertical");
        _sideAxis = Input.GetAxis("Horizontal");
    }
}
