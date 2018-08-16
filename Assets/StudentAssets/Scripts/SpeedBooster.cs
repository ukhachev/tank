using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class SpeedBooster: NetworkBehaviour {
    [SerializeField]
    private float _duration;
    [SerializeField]
    private float _boost;
    [SerializeField]
    private string _playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _playerTag)
        {
            var controls = other.GetComponent<TankControls>();
            var trail = other.GetComponent<TrailRenderer>();
            controls.Speed += _boost;
            trail.enabled = true;
            Observable.Timer(System.TimeSpan.FromSeconds(_duration)).Subscribe(t =>
            {
                controls.Speed -= _boost;
                trail.enabled = false;
                
            });
            Destroy(gameObject);
        }
        
    }
}
