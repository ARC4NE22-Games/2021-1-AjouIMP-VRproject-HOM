using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().Attacked();
            Debug.Log("Zombie Trigger");
            Debug.Log(Vector3.Distance(other.transform.position, transform.position));
        }



    }

}
