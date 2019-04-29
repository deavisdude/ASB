using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float target;
    float speed = 16f;
    public bool activated = false;
    public float targetHeight = 20f;
    float moveSpeed = 4f;

    void FixedUpdate()
    {
        if(activated && transform.position.y < targetHeight)
        {
            float x = moveSpeed * Time.fixedDeltaTime;
            transform.Translate(0f,moveSpeed*Time.fixedDeltaTime,0f);
        }

        if (activated && transform.eulerAngles.x < target) { 
            float angle = speed * Time.fixedDeltaTime;
            transform.Rotate(Vector3.right, angle);
        }

        if(activated && transform.position.y >= targetHeight && transform.eulerAngles.x >= (target - 1d))
        {
            activated = false;
        }
    }
}
