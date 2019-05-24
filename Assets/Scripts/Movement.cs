using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float fudgeFactor; //max distance on y between controllers before triggering a turn.

    public bool ignoreInput;

    Vector3 leftControl;
    Vector3 rightControl;
    CycleTrail cycleTrail;

    bool hasTurned;

    private void Start()
    {
        cycleTrail = GetComponent<CycleTrail>();
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (ignoreInput) return;

        OVRInput.Update();

        leftControl = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        rightControl = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        print("LC: " + leftControl);
        print("RC: " + rightControl);

        //Find lowest control

        float distance = rightControl.y - leftControl.y; //If Dist is positive we turn left, else go right.
        int turnDirection = distance > 0 ? -1 : 1; //-1 = left, 1 = right

        if (Mathf.Abs(distance) >= fudgeFactor && !hasTurned) //Check dist to see if passed fudge factor
        {
            hasTurned = true;
            transform.rotation *= Quaternion.Euler(0, 90 * turnDirection, 0);
        }
        else if (Mathf.Abs(distance) < fudgeFactor && hasTurned)
            hasTurned = false;
    }
}
