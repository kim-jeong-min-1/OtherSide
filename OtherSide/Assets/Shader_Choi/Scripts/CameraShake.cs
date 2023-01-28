using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField][Range(0.01f, 0.1f)] float ShakeRange = 0.05f;
    [SerializeField][Range(0.1f, 1f)] float Duration = 0.5f;

    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", Duration);
    }

    private void StartShake()
    {
        float cameraPosX = Random.value * ShakeRange * 2 - ShakeRange;
        float cameraPosY = Random.value * ShakeRange * 2 - ShakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }

}
