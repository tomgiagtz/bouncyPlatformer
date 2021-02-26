using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    public Transform target;
    public float smoothTimeMin = 0.5f;
    public float smoothTimeMax = 2f;
    public float maxDistance = 1f;
    private float currSmoothTime;
    public float smoothDelta = 10f;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        cam = GetComponent<Camera>();
        currSmoothTime = smoothTimeMin;
    }
    void FixedUpdate()
    {
        Vector3 point = new Vector3(target.position.x, target.position.y, transform.position.z);

        float distance = Vector3.Distance(point, transform.position);

        if (distance > maxDistance) {
            currSmoothTime = Mathf.Lerp(currSmoothTime, smoothTimeMax, Time.deltaTime * smoothDelta);
        } else {
            currSmoothTime = Mathf.Lerp(currSmoothTime, smoothTimeMin, Time.deltaTime * smoothDelta);
        }
        Vector3 smoothDest = Vector3.Lerp(transform.position, point, currSmoothTime * Time.deltaTime);
        // smoothDest.z = -10f;
        transform.position = smoothDest;
    }
}
