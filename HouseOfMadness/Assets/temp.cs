using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private Animator _animator;

    private Transform _transform;
    private float _horzontal = 0.0f;
    private float _vertical = 0.0f;

    public float movespeed = 5.0f;
    public float rotatespd = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _horzontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Vector3 moveDirect = (Vector3.forward * _vertical)+(Vector3.right * _horzontal);
        //_transform.Rotate(Vector3.up * Time.deltaTime * rotatespd * Input.GetAxis("Mouse x"));
        if (_vertical >= 0.1f)
        {
            _animator.SetBool("isMove", true);
        }
        else if (_vertical <= -0.1f)
        {
            _animator.SetBool("isMove", true);
        }
        else if (_horzontal >= 0.1f)
        {
            _animator.SetBool("isMove", true);
        }
        else if (_horzontal <= -0.1f)
        {
            _animator.SetBool("isMove", true);
        }
        else
        {
            _animator.SetBool("isMove", false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            _animator.SetBool("isAttack", true);
        }
        else
        {
            _animator.SetBool("isAttack", false);
        }

    }
}
