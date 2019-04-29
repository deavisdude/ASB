using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1PGame : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    // Start is called before the first frame update
    public void StartGame()
    {
        Camera.main.GetComponent<CameraRotator>().activated = true;
        Camera.main.GetComponent<CameraRotator>().target = 90f;
        foreach(GameObject go in objectsToToggle)
        {
            go.SetActive(!go.activeSelf);
        }

    }
}
