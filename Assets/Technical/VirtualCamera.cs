using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    float Shake = 0;
    float DecreaseSpeed=10f;

    Animator animator;
    void Start()
    {
        animator=GetComponent<Animator>();

        CameraBehaviour.instance.SetShake += CameraBehaviour_SetShake;
        CameraBehaviour.instance.AddShake += Instance_AddShake;
    }

    private void Instance_AddShake(float shake)
    {
        Shake += shake;
    }

    private void CameraBehaviour_SetShake(float shake)
    {
        Shake = shake;
    }

    private void Update()
    {
        Shake=Mathf.Lerp(Shake, 0, DecreaseSpeed*Time.deltaTime);
        animator.SetFloat("Shake", Shake);
    }


}
