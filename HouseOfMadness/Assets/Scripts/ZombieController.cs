using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public int uid;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _basePos;
    private float _distance, _coolDown, _speed;
    private int _health, _detectionDist;
    private bool _hasTarget;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _basePos = transform.position;
        _speed = (uid == 0) ? 1.0f : 1.5f;
        _health = (uid == 0) ? 4 : 2;
        GetComponent<SphereCollider>().radius = _detectionDist = (uid == 0) ? 9 : 5;
    }

    float dist2(Vector3 pos1, Vector3 pos2)
    {
        float result = Mathf.Pow(pos1.x - pos2.x, 2)
                       + Mathf.Pow(pos1.y - pos2.y, 2) + Mathf.Pow(pos1.z - pos2.z, 2);
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        SetAnimation();
        SetSpeed();
    }

    void FindPlayer()
    {
        if (_hasTarget)
        {
            _distance = dist2(transform.position, player.transform.position);
            _navMeshAgent.SetDestination(player.transform.position);
            
            if (_distance > (_detectionDist + 1) * (_detectionDist + 1))
                _hasTarget = false;
        }
        else
        {
            _navMeshAgent.SetDestination(_basePos);
            _distance = dist2(transform.position,_basePos);
        }
    }

    void SetAnimation()
    {
        if (_hasTarget)
        {
            if (_distance < 6.0f)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("isMove", false);
                _animator.SetBool("isAttack", true);
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _animator.SetBool("isMove", true);
                _animator.SetBool("isAttack", false);
            }
        }
        else
        {
            if(_distance < 1.0f)
                _animator.SetBool("isMove", false);
        }
    }

    void SetSpeed()
    {
        if (_hasTarget)
        {
            if (_distance <= 25.0f)
                _navMeshAgent.speed = _speed * 2;
            else
                _navMeshAgent.speed = _speed;
            
            if (_coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
                _navMeshAgent.speed = _speed * 0.5f;
            }
        }
        else
        {
            _navMeshAgent.speed = _speed;
        }
    }

    public void Attacked()
    {
        if (_health > 0)
        {
            // 1 sec slow
            _health--;
            if (_coolDown <= 0)
            {
                _coolDown = 30f;
            }
        }
        else
        {
            // zombie dead
            Destroy(gameObject);
        }
    }

    // Animation Event (Zombie_swiping)
    public void AttackPlayer()
    {
        if (_distance < 6.0f)
        {
            player.GetComponent<PlayerController>().Attacked();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _hasTarget = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision2");
    }
}
