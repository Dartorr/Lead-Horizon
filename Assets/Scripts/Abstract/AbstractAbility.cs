using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractAbility : MonoBehaviour
{
    protected PlayerControls inputActions;
    public float priority { get; protected set; }

    protected float duration = 0f;
    protected float cooldown = 1f;

    protected bool freezePlayerMovement = false;
    protected bool freezePlayerRotation = false;

    protected virtual void onAwake()
    {
        inputActions = GetComponentInParent<PlayerBeh>().inputActions;

        inputActions.AbilityMap.Ability.performed += Ability_performed;
        inputActions.AbilityMap.Ability.Enable();
    }

    protected void Ability_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StartAbility();
    }

    protected virtual void StartAbility()
    {
        DisableAbility();
        var t = gameObject.AddComponent<Timer>();
        t.TimerStart(duration);
        t.onTimeOver += AbilityOver;

        CheckRestrictions(false);
    }

    protected virtual void AbilityOver()
    {
        CheckRestrictions(true);

        var t = gameObject.AddComponent<Timer>();
        t.TimerStart(cooldown);
        t.onTimeOver += EnableAbility;
    }

    protected virtual void DisableAbility()
    {
        inputActions.AbilityMap.Ability.performed -= Ability_performed;
    }

    protected virtual void EnableAbility()
    {
        inputActions.AbilityMap.Ability.performed += Ability_performed;
    }

    protected virtual void CheckRestrictions(bool toEnable)
    {
        if (toEnable)
        {
            if (freezePlayerMovement)
            {
                inputActions.MovementMap.Movement.Enable();
            }
            if (freezePlayerRotation)
            {
                inputActions.MouseMap.MouseMoved.Enable();
            }
        }
        else
        {
            if (freezePlayerMovement)
            {
                inputActions.MovementMap.Movement.Disable();
            }
            if (freezePlayerRotation)
            {
                inputActions.MouseMap.MouseMoved.Disable();
            }
        }
    }

    private void OnDestroy()
    {
        inputActions.AbilityMap.Ability.performed -= Ability_performed;
    }
}
