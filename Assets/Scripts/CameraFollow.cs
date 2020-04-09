using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    Vector3 targetFrezeY;
    Vector3 smoothedPosition;
    float SmoothSpeed = 6.0f;
    public Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {

        targetFrezeY = new Vector3(target.position.x, transform.position.y, transform.position.z) + offSet;
        smoothedPosition = Vector3.Lerp(transform.position, targetFrezeY, SmoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }
}
