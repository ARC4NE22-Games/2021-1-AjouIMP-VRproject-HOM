using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip KeyClip, AttackedClip, ExitClip;

    public int KeyCount, BulletCount, Health;
    public GameObject LightObject;
    
    private const int MAX_BULLET_COUNT = 10;
    private AudioSource _audioSource;
    


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyCount == 3)
        {
            LightObject.SetActive(true);
            //KeyCount = 0;
        }
    }

    public void GrabSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactable.CompareTag("Key"))
        {

            Destroy(args.interactable.gameObject);
            _audioSource.PlayOneShot(KeyClip);
            KeyCount += 1;
            Debug.Log("User's key count : " + KeyCount);
        }

        else if (args.interactable.CompareTag("Bullet"))
        {
            Destroy(args.interactable.gameObject);
            if (BulletCount < MAX_BULLET_COUNT)
            {
                _audioSource.PlayOneShot(KeyClip);
                BulletCount++;
                Debug.Log("User's bullet count : " + BulletCount);
            }
        }

        else if (args.interactable.CompareTag("Exit"))
        {
            _audioSource.PlayOneShot(ExitClip);
            SceneManager.LoadScene("MenuScene");
        }
    }
    
    public void Attacked()
    {
        if (Health > 0)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(AttackedClip);
            Health--;
        }
        else
        {
            // Game Over
            SceneManager.LoadScene("MenuScene");
        }
    }
}
