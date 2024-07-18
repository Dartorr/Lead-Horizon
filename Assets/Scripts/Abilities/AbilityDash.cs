using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : AbstractAbility
{
    Chars chars;
    public float debugDuration = 1f;
    public float debugCooldown = 1f;

    public float dashSpeed = 5f;

    protected override void onAwake()
    {
        inputActions = GetComponentInParent<PlayerBeh>().inputActions;

        inputActions.AbilityMap.Dash.performed += Ability_performed;
        inputActions.AbilityMap.Dash.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        chars = GetComponent<Chars>();

        duration = debugDuration;
        cooldown = debugCooldown;
        onAwake();
    }

    protected override void StartAbility()
    {
        base.StartAbility();
        chars.AdditionalSpeed = dashSpeed * chars.dashSpeedModifier;
        StartCoroutine(linearSlowdown());
        StartCoroutine(spawn());

    }

    protected override void AbilityOver()
    {
        base.AbilityOver();
        StopAllCoroutines();
        chars.AdditionalSpeed = 0;
    }

    protected override void DisableAbility()
    {
        inputActions.AbilityMap.Dash.performed -= Ability_performed;
    }

    protected override void EnableAbility()
    {
        inputActions.AbilityMap.Dash.performed += Ability_performed;
    }

    IEnumerator linearSlowdown()
    {
        float changeSpeed = 2;
        while (chars.AdditionalSpeed > 0.25)
        {
            yield return new WaitForFixedUpdate();
            chars.AdditionalSpeed = Mathf.Lerp(chars.AdditionalSpeed, 0, changeSpeed * Time.deltaTime);
        }
        chars.AdditionalSpeed = 0;
    }

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject gameObject = (GameObject.CreatePrimitive(PrimitiveType.Cube));
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.position = this.transform.position;
            gameObject.transform.parent = null;

            Destroy(gameObject, 1f);
        }
    }

    private void FixedUpdate()
    {
        //float changeSpeed = (dashSpeed - 0) / duration;
        //chars.dashSpeedModifier = Mathf.Lerp(chars.dashSpeedModifier, 0, changeSpeed * Time.deltaTime);
    }
}
