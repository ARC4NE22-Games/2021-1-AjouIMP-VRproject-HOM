using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider MoveProvider;
    public AudioClip KeyClip, AttackedClip, ExitClip, WalkClip;

    public int KeyCount, BulletCount, Health;
    public GameObject LightObject;
    
    private const int MAX_BULLET_COUNT = 10;
    private AudioSource _audioSource;
    private float coolDown;
    


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        MoveProvider = GameObject.Find("XR Rig").GetComponent<ActionBasedContinuousMoveProvider>();
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
        
        if (MoveProvider.leftHandMoveAction.action.phase == InputActionPhase.Started
            || MoveProvider.leftHandMoveAction.action.phase == InputActionPhase.Performed)
        {
            coolDown++;
        }
        else
        {
            coolDown = 0;
        }

        if (coolDown >= 3)
        {
            coolDown = 0;
            _audioSource.Stop();
            _audioSource.PlayOneShot(WalkClip);
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
        if (Health > 1)
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
