using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleTrail : MonoBehaviour
{
    public TrailRenderer cycleTrail;
    public LayerMask riderLayer;

    //change to not run constantly

    private void Update()
    {
        Vector3[] points = new Vector3[cycleTrail.positionCount];
        cycleTrail.GetPositions(points);

        if (points.Length <= 1) return;
            
        for(int i = 0; i < points.Length; i++)
        {
            if (i + 1 >= points.Length)
                return;

            RaycastHit hit;
            if (Physics.Linecast(points[i], points[i + 1], out hit, riderLayer))
            {
                if(hit.transform != transform)
                    print("hit target");
            }
        }
    }
}
