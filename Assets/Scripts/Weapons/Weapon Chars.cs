using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

[Serializable]
public class WeaponChars
{
    public string Name="name";

    public float Recoil;
    public float Accuracy;//максимальное отклонение в градусах
    public float Reload;
    public float APMod;//мод урона по броне
    public float BulletVelocity=15f;

    public int Damage =1;
    public int MagSize=10;
    public int InMag=10;
    public int Ammo = 20;
    public int UI_ROF=120; //роф в ЮИ


    public bool Shot()
    {
        if (InMag > 0)
        {
            InMag--;
            return true;
        }
        else return false;
    }

    public bool _Reload()
    {
        if (Ammo == 0) return false;
        if (Ammo > (MagSize-InMag))
        {
            Ammo-=(MagSize-InMag);
            InMag = MagSize;
        }
        else
        {
            InMag += Ammo;
            Ammo = 0;
        }
        return true;
    }
}


