using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractWeapon : MonoBehaviour
{
    WeaponChars weaponChars;
    PlayerControls inputActions;
    PlayerBeh playerBeh;

    GameObject pivotPoint;

    int CurrentWeapon = 0;

    float RateOfFire;

    public GameObject Bullet;

    bool TriggerPulled = false;
    bool Cooldown = false;

    void ChangeWeapon(int NewWeapon)
    {
        if (pivotPoint.transform.childCount > 0)
            Destroy(pivotPoint.transform.GetChild(0).gameObject);
        CurrentWeapon = NewWeapon;
        weaponChars = GameBehaviour.gameBehaviour.WeaponChars[CurrentWeapon];

        Instantiate(GameBehaviour.gameBehaviour.WeaponsDictionary[weaponChars], pivotPoint.transform);

        RateOfFire = 60f / weaponChars.UI_ROF;
        playerBeh.RecoilChange(weaponChars.Recoil);
    }

    private void Start()
    {
        playerBeh = GetComponentInParent<PlayerBeh>();
        inputActions = playerBeh.inputActions;
        inputActions.WeaponMap.Shoot.performed += Shoot_performed;
        inputActions.WeaponMap.Shoot.canceled += Shoot_canceled;
        inputActions.WeaponMap.Reload.performed += Reload_performed;
        inputActions.WeaponMap.Enable();

        pivotPoint = transform.GetChild(0).gameObject;
        ChangeWeapon(CurrentWeapon);
    }

    private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (weaponChars._Reload())
            StartCoroutine(ReloadCour());
    }

    private void Shoot_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TriggerPulled = false;
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TriggerPulled = true;
        if (weaponChars.InMag < 1 || Cooldown) return;
        StartCoroutine(ShootSequence());
    }

    IEnumerator ReloadCour()
    {
        playerBeh.Reload(weaponChars.Reload);
        Cooldown = true;
        yield return new WaitForSeconds(weaponChars.Reload);
        Cooldown = false;
    }

    IEnumerator ShootSequence()
    {
        while (TriggerPulled)
        {
            if (!weaponChars.Shot())
            {
                TriggerPulled = false;
                break;
            }
            playerBeh.Shot();

            GameObject _bullet = Instantiate(Bullet);
            CameraBehaviour.instance._AddShake(weaponChars.Recoil);

            float deltaY = Random.Range(-weaponChars.Accuracy, weaponChars.Accuracy);

            _bullet.GetComponent<Bullet>().Characteristics = weaponChars;
            _bullet.transform.position = playerBeh.AimPlane.transform.position;
            _bullet.transform.rotation = playerBeh.AimPlane.transform.rotation;
            Vector3 vec = _bullet.transform.rotation.eulerAngles;
            vec.y += deltaY;
            _bullet.transform.eulerAngles = vec;
            _bullet.GetComponent<Rigidbody>().velocity = _bullet.transform.forward * weaponChars.BulletVelocity;

            Destroy(_bullet, 5f);

            Cooldown = true;
            yield return new WaitForSeconds(RateOfFire);
            Cooldown = false;
        }
    }


}
