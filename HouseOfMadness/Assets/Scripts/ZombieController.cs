using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public List<AudioClip> audioList;   // 0: SFX_Roar, 1: SFX_ZombieAttack, 2: SFX_ZombieAttacked
    public int uid;
    
    private GameObject _player;
    private FadeController _fade;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector3 _basePos;

    private float _distance, _detectionDist;
    private float _slowCoolDown, _regenCoolDown, _speed;
    private int _health;
    private bool _hasTarget;
    
    private void Start()
    {
        _player = GameObject.Find("XR Rig").transform.Find("Camera Offset").Find("PlayerBody").gameObject;
        //_player = GameObject.Find("Capsule");
        _fade = GameObject.Find("XR Rig").transform.Find("Camera Offset").Find("FadeInOut")
            .GetComponent<FadeController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _basePos = transform.position;
        switch (uid)
        {
            case 0:
                _speed = 1.0f;
                _health = 4;
                _detectionDist = 7; // scaled x1.3, real detection range (9.1)
                break;
            case 1:
                _speed = 2.0f;
                _health = 2;
                _detectionDist = 6; // scaled x1.1, real detection range (6.6)
                break;
            case 2:
                _speed = 0.8f;
                _health = 6;
                _detectionDist = 13; // scaled x0.9, real detection range (11.7)
                break;
        }
    }

    float dist2(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2);
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
            _distance = dist2(transform.position, _player.transform.position);
            _navMeshAgent.SetDestination(_player.transform.position);

            float limit = Mathf.Pow((_detectionDist * transform.localScale.x) + 1, 2);
            if (_distance > limit || Mathf.Abs(transform.position.y - _player.transform.position.y) > 1.5f)
            {
                _hasTarget = false;
            }
        }
        else
        {
            _navMeshAgent.SetDestination(_basePos);
            _distance = dist2(transform.position, _basePos);
        }

        if (_fade.isHidden)
        {
            _hasTarget = false;
        }
    }

    void SetAnimation()
    {
        if (_hasTarget)
        {
            if (_distance < 6.0f)
            {
                // start attack
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
            if (_distance < 1f)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("isMove", false);
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _animator.SetBool("isMove", true);
                _animator.SetBool("isAttack", false);
            }
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
            
            if (_slowCoolDown > 0)
            {
                _slowCoolDown -= Time.deltaTime;
                _navMeshAgent.speed = _speed * 0.5f;
            }
        }
        else
        {
            _navMeshAgent.speed = _speed;
        }
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_hasTarget)
        {
            if (Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                _hasTarget = true;
                _audioSource.Stop();
                _audioSource.PlayOneShot(audioList[0]);   
            }
        }
    }
    
    

    public void Attacked()
    {
        if (_health > 1)
        {
            _health--;
            _audioSource.Stop();
            _audioSource.PlayOneShot(audioList[2]);
            
            if (_slowCoolDown <= 0)             // 0.5 sec slow
                _slowCoolDown = 30f;
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
            // Damage handling if distance from player is close
            _audioSource.Stop();
            _audioSource.PlayOneShot(audioList[1]);
            _player.GetComponent<GameManager>().Attacked();
        }
    }
}
