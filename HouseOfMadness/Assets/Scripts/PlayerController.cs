using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _health, _bullet;
    
    void Init()
    {
        _health = 5;
        _bullet = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attacked()
    {
        if (_health > 0)
        {
            _health--;
            Debug.Log(_health);
        }
        else
        {
            // game over
        }
    }

}
