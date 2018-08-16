using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    const string Z_AXIS_NAME = "Vertical1";
    const string X_AXIS_NAME = "Horizontal1";

    public float Speed;
    public float TurnSpeed;

    private float _zAxisInput;
    private float _xAxisInput;

    private Rigidbody _rigidbody;
    
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}

    void Update()
    {
        _zAxisInput = Input.GetAxis(Z_AXIS_NAME);
        _xAxisInput = Input.GetAxis(X_AXIS_NAME);
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        var movement = transform.forward * _zAxisInput * Speed * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    void Turn()
    {
        var turn = _xAxisInput * TurnSpeed * Time.deltaTime;
        var turnY = Quaternion.Euler(0, turn, 0);

        _rigidbody.MoveRotation(_rigidbody.rotation * turnY);
    }

}


