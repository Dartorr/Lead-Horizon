//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//public class AbilitiesAbstract : MonoBehaviour
//{

//    public float duration = 0f;
//    public float cooldown = 0f;
//    public bool performing = false;

//    public bool _FreezeMovement = false;
//    public bool _FreezeMouse = false;

//    delegate void AbilityDelegate();
//    AbilityDelegate abilityStart;
//    AbilityDelegate abilityOver;


//    protected void beforeFirstUpdate()
//    {
//        if (_FreezeMouse)
//        {
//            abilityStart += FreezeMouse;
//            abilityOver += UnfreezeMouse;
//        }
//        if (_FreezeMovement)
//        {
//            abilityStart += FreezeMov;
//            abilityOver += UnfreezeMov;
//        }
//    }

//    public virtual void AbilityStart()
//    {
//        Timer timer = nPCVar.gameObject.AddComponent<Timer>();
//        timer.SetTimer(duration);
//        timer.onTimeOver += AbilityOver;
//        abilityStart.Invoke();

//        nPCVar.InvokeAbilityEvent(abilityAnimation);
//        DisableConflicts();
//        Disable();
//    }

//    protected void DisableConflicts()
//    {
//        if (abilitiesConflicts.Count == 0) return;
//        var allAbilitiesInObject = nPCVar.GetComponents<AbilitiesAbstract>();
//        foreach (var item in allAbilitiesInObject)
//        {
//            if (abilitiesConflicts.IndexOf(item.abilityAnimation) != -1) item.Disable();
//        }
//    }

//    protected void EnableConflicts()
//    {
//        if (abilitiesConflicts.Count == 0) return;
//        var allAbilitiesInObject = nPCVar.GetComponents<AbilitiesAbstract>();
//        foreach (var item in allAbilitiesInObject)
//        {
//            if (abilitiesConflicts.IndexOf(item.abilityAnimation) != -1) item.Enable();
//        }
//    }

//    public virtual void AbilityOver()
//    {
//        abilityOver.Invoke();
//        EnableConflicts();
//        Cooldown();
//        nPCVar.InvokeAbilityEvent(AbilityType.None);
//    }

//    protected virtual void Cooldown()
//    {
//        Timer timer = nPCVar.gameObject.AddComponent<Timer>();
//        timer.SetTimer(cooldown);
//        timer.onTimeOver += Enable;
//    }

//    public virtual void Disable() { }
//    public virtual void Enable() { }

//    public virtual void FreezeMov()
//    {
//        if (!nPCVar.isNPC)
//        {
//            nPCVar.inputActions.MovDirCashe.Enable();
//            nPCVar.inputActions.Movement.Disable();

//            nPCVar.DoNotUpdateVelocityEachFrame();
//        }
//        else
//        {
//            //«¿“€◊ ¿
//        }
//    }

//    public virtual void FreezeMouse()
//    {
//        if (!nPCVar.isNPC)
//        {
//            nPCVar.inputActions.Mouse.Disable();  
//        }
//        else
//        {
//            //«¿“€◊ ¿
//        }
//    }

//    public virtual void UnfreezeMov()
//    {
//        if (!nPCVar.isNPC)
//        {
//            nPCVar.inputActions.Movement.Enable();
//            nPCVar.inputActions.MovDirCashe.Disable();
//            nPCVar.InvokeRefreshToggleHold();

//            nPCVar.UpdateVelocityEachFrame();
//        }
//        else
//        {
//            //«¿“€◊ ¿
//        }
//    }
//    public virtual void UnfreezeMouse()
//    {
//        if (!nPCVar.isNPC)
//        {
//            nPCVar.inputActions.Mouse.Enable();
//        }
//        else
//        {
//            //«¿“€◊ ¿
//        }
//    }

//    public virtual void Link() { }
//    public virtual void UnLink() { }


//    protected virtual void OnDestroyAbility()
//    {
//        UnLink();
//        Disable();
//    }
//}
