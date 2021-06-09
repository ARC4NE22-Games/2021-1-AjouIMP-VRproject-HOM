using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationInteraction : MonoBehaviour
{
    public void HandleHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Hovering over an enemy: " + args.interactable.gameObject.name);
    }
    
    public void HandleSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactable.CompareTag("Teleport"))
        {
            GameObject.Find("XR Rig").GetComponent<LocomotionController>().yTmp =
                args.interactable.GetComponent<Transform>().position.y;
        }
    }
}
