using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Chars : MonoBehaviour
{
    private float _HP;
    public float HP
    {
        get { return _HP; }
        set
        {
            if (value < 0) value = 0;
            _HP = value;
        }
    }
    public float MaxHP;
    public static readonly float DefaultHP = 80;
    readonly List<float> HPModifiers = new List<float>();

    private float _Armor;
    public float Armor
    {
        get
        {
            return _Armor;
        }
        set
        {
            if (value < 0) value = 0;
            _Armor = value;
        }
    }
    public float MaxArmor;
    public float DefaultArmor;
    readonly List<float> ArmorModifiers = new List<float>();

    public float Speed;
    public float DefaultSpeed;
    readonly List<float> SpeedModifiers = new List<float>();

    public float AdditionalSpeed;
    public float dashSpeedModifier;
    // Start is called before the first frame update
    void Start()
    {
        Restore();
        dashSpeedModifier = Speed / DefaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RecalculateChars()
    {
        MaxHP = DefaultHP + HPModifiers.Sum();
        MaxArmor = DefaultArmor + ArmorModifiers.Sum();
        Speed = DefaultSpeed + SpeedModifiers.Sum();
    }

    void Restore()
    {
        RecalculateChars();
        HP = MaxHP;
        Armor = MaxArmor;
    }

    public void TakeDamage(float dmg)
    {
        if (HP > 0)
        {
            HP -= (dmg * (1f - Armor));
            Armor -= dmg;

            Debug.Log(HP);

            if (HP <= 0)
            {
                Dead();
            }
        }
    }

    public void TakeHeal(float hl)
    {
        HP = Mathf.Clamp(HP + hl, 0, MaxHP);
    }

    void Dead()
    {
        Destroy(gameObject);
        Debug.Log("Gay");
    }
}
