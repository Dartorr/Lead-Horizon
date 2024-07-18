using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponChars Characteristics;

    Chars target;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Chars>(out target))
            return;
        target.TakeDamage(Characteristics.Damage);
    }

}
