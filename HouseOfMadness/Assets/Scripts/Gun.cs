using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float speed = 40;
    public GameObject bullet;
    public Transform bulletHole;
    public TextMeshProUGUI tmPro;
    public AudioClip audioClip;
    
    private AudioSource audioSource;
    private GameManager _gameManager;
    private const int MAX_BULLET_COUNT = 10;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("XR Rig").transform.Find("Camera Offset").Find("PlayerBody")
            .GetComponent<GameManager>();
        tmPro.text = _gameManager.BulletCount + " / " + MAX_BULLET_COUNT;
    }

    void Update()
    {
        tmPro.text = _gameManager.BulletCount + " / " + MAX_BULLET_COUNT;
    }

    public void Fire()
    {
        if (_gameManager.BulletCount > 0)
        {
            GameObject spawnedBullet = Instantiate(bullet, bulletHole.position, bulletHole.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * bulletHole.up;
            _gameManager.BulletCount--;

            audioSource.Stop();
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 1);
        }
    }
}