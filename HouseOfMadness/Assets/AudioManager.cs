using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource _audioSource;

    public AudioClip BackGroundClip;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = BackGroundClip;
        _audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
