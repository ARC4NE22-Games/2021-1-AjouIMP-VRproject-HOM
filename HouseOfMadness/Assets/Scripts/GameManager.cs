using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip KeyClip;

    public int KeyCount = 0;
    public int BulletCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            _audioSource.PlayOneShot(KeyClip);
            BulletCount += 1;
            Debug.Log("User's bullet count : " + BulletCount);
        }
    }
}
