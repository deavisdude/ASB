using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float target;
    public float targetHeight = 20f;

    public void Activate()
    {
        StartCoroutine(Rotate());
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        for(float f=0f; f<targetHeight; f+= 0.1f)
        {
            transform.Translate(0f, 0.1f, 0f);
            yield return null;
        }
    }

    IEnumerator Rotate()
    {
        for (float f = 0f; f <= target; f += 0.4f)
        {
            transform.Rotate(Vector3.right, 0.4f);
            yield return null;
        }
    }
}
