using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyControl : NetworkBehaviour {


    [SerializeField]
    private GameObject _turret;

    [SerializeField]
    private float _fov = 60f;

    [SerializeField]
    private float _viewDistance = 30f;

    [SerializeField]
    private float _fireDistance = 18f;
    [SerializeField]
    private float _reCalcRouteDistance = 8f;


    private GameObject _target = null;
    private NavMeshAgent _agent;
    private EnemyTurret _turretScript;


    private bool hasTarget = false;
	
	void Start () {
        _agent = gameObject.GetComponent<NavMeshAgent>();
       
        _turretScript = GetComponent<EnemyTurret>();
        if (!isServer)
        {
            _agent.enabled = false;
            this.enabled = false;
        }
        
    }

    void FindTarget()
    {
        float minDistance = float.MaxValue;
        foreach (var player in Manager.Instance.Players)
        {
            if (player == null)
            {
                continue;
            }
            float dist = Vector3.Distance(player.transform.position, this.transform.position);
            if (minDistance > dist && dist <= _viewDistance)
            {
                var angle = Vector3.Angle(_turret.transform.forward, player.transform.position - _turret.transform.position);
                if (angle >= -_fov & angle <= _fov)
                {
                    RaycastHit hit;
                    Physics.Raycast(_turret.transform.position + _turret.transform.forward * 1.5f, player.transform.position - _turret.transform.position, out hit);
                    if (hit.transform.tag == "Player")
                    {
                        minDistance = dist;
                        _target = player;
                    }
                }
            }
        }
    }
    void Fire()
    {
        _turretScript.Fire();
    }
    void FolowTarget()
    {
       _turret.transform.LookAt(_target.transform.position + _target.transform.up * 1.5f);
        if (Vector3.Distance(_target.transform.position, _agent.destination) > _reCalcRouteDistance)
        {
            _agent.SetDestination(_target.transform.position);
        }
       

        if (Vector3.Distance(_target.transform.position, transform.position) <= _fireDistance)
        {
           Fire();
        }
    }
    Vector3 GetPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _reCalcRouteDistance * Random.Range(3, 8);
        randomDirection += transform.position;
        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, _reCalcRouteDistance, 1);
        return hit.position;
    }
    void Patrol()
    {

        if (_agent.remainingDistance <= 4)
        { 
            _agent.SetDestination(GetPatrolPoint());
        }
    }

    void Update () {
        if (_target == null)
        {
            if (hasTarget)
            {
                _agent.SetDestination(GetPatrolPoint());
                hasTarget = false;
            }
            Patrol();
            FindTarget();
        }
        else
        {
            hasTarget = true;
            FolowTarget();
        }
	}
}
