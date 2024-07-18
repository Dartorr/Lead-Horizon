//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class AbilityDash : AbilitiesAbstract
//{
//    public float SpeedMod = 8;
//    Vector3 vel = new Vector3();


//    private void Start()
//    {
//        duration = 0.15f;
//        cooldown = 1;
//        _FreezeMovement = true;
//        abilityAnimation = AbilityType.Dash;
//        abilitiesConflicts = new List<AbilityType> { };

//        nPCVar = gameObject.GetComponent<AbstractNPCVar>();

//        if (!nPCVar.isNPC)
//        {
//            Link();
//            Enable();
//        }
//        beforeFirstUpdate();
//    }

//    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
//    {
//        if (nPCVar._moveDir == new Vector2())
//        {
//            return;
//        }

//        Vector3 moveDirBuffer = nPCVar._moveDir;
//        AbilityStart();


//        vel = (nPCVar.transform.forward * moveDirBuffer.y + nPCVar.transform.right * moveDirBuffer.x)
//            * (nPCVar.playerChars.speed) * SpeedMod;

//        nPCVar.rb.velocity=vel;

//    }

//    public override void AbilityStart()
//    {
//        base.AbilityStart();
//        performing = true;
//    }

//    private void OnDestroy()
//    {
//        base.OnDestroyAbility();
//    }

//    public void AbilityStart(AbstractNPCVar _AbstractNPCVar)
//    {
//        base.AbilityStart();
//    }

//    public override void AbilityOver()
//    {
//        base.AbilityOver();
//        performing = false;
//    }

//    public override void Disable()
//    {
//        base.Disable();
//        nPCVar.inputActions.Abilities.Dash.Disable();
//    }
//    public override void Enable()
//    {
//        base.Enable();
//        nPCVar.inputActions.Abilities.Dash.Enable();
//    }

//    public override void Link()
//    {
//        base.Link();
//        nPCVar.inputActions.Abilities.Dash.performed += Dash_performed;
//    }

//    public override void UnLink()
//    {
//        base.UnLink();
//        nPCVar.inputActions.Abilities.Dash.performed -= Dash_performed;
//    }

//}
