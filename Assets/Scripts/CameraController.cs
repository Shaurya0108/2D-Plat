using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 postionOffset;

    [Header("Axis Limit")]
    public Vector2 xLimit;
    public Vector2 yLimit;
    

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        Vector3 targetPosition = target.position + postionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
