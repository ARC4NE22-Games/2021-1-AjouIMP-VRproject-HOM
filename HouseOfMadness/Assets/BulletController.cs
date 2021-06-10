using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().Attacked();
            Destroy(gameObject);
        }
    }
}
