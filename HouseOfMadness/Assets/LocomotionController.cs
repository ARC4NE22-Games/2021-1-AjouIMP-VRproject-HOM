using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public ActionBasedController leftRay;
    public ActionBasedController rightRay;

    public float activationThreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (leftRay)
        {
            leftRay.gameObject.SetActive(CheckIfActivated(leftRay));
        }

        if (rightRay)
        {
            rightRay.gameObject.SetActive(CheckIfActivated(rightRay));
        }
    }
    private bool CheckIfActivated(ActionBasedController controller)
    {
        float result = controller.selectAction.action.ReadValue<float>();
        return (result > activationThreshold);
    }
}
