using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public ActionBasedController leftRay;
    public float activationThreshold = 0.1f;
    public float yTmp;

    // Update is called once per frame
    void Update()
    {
        if (leftRay)
        {
            leftRay.gameObject.SetActive(CheckIfActivated(leftRay));
        }

        transform.position = new Vector3(transform.position.x, yTmp + 0.5f, transform.position.z);
    }

    private bool CheckIfActivated(ActionBasedController controller)
    {
        float result = controller.selectAction.action.ReadValue<float>();
        return (result > activationThreshold);
    }
}
