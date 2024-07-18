using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour instance;
    public CinemachineBrain cinemachineBrain;
    Camera mainCamera;
    public delegate void CameraShake(float shake);
    public event CameraShake SetShake;
    public event CameraShake AddShake;

    private void Awake()
    {
        instance = this;
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    public Camera getActiveCamera()
    {
        mainCamera = cinemachineBrain.gameObject.GetComponent<Camera>();
        return mainCamera;
    }

    public void _AddShake(float shake)
    {
        AddShake(shake);
    }

    public void _SetShake(float shake)
    {
        SetShake(shake);
    }
}
