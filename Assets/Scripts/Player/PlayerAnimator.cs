using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float changeSpeed = 1;
    float ReloadAnimDur = 2.8f;

    PlayerMovement playerMovement;
    PlayerBeh playerBeh;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerBeh = GetComponentInParent<PlayerBeh>();
        playerBeh.onReload += ReloadPerformed;
        playerBeh.onRecoilChange += PlayerBeh_onRecoilChange;
        playerBeh.onShot += PlayerBeh_onShot;
        playerMovement.MovementPerformed += PlayerMovement_MovementPerformed;
        playerMovement.RotationPerformed += PlayerMovement_rotationPerformed;
    }

    private void PlayerBeh_onShot()
    {
        animator.SetTrigger("Shoot");
    }

    private void PlayerBeh_onRecoilChange(float t)
    {
        animator.SetFloat("Recoil", t);
    }

    private void PlayerMovement_rotationPerformed(float value)
    {
        animator.SetFloat("Angle", value);
    }

    void ReloadPerformed(float reloadTime)
    {
        animator.SetFloat("ReloadMult", ReloadAnimDur / reloadTime);
        animator.SetTrigger("Reload");
    }

    private void PlayerMovement_MovementPerformed(float vel)
    {
        animator.SetFloat("Speed", vel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
