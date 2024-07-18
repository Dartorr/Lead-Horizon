using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeh : MonoBehaviour
{
    public PlayerControls inputActions;
    public delegate void PlayerDelegate();
    public delegate void WeaponFloatDelegate(float t);
    public event PlayerDelegate onUpdate;
    public event PlayerDelegate onShot;
    public event WeaponFloatDelegate onReload;
    public event WeaponFloatDelegate onRecoilChange;
    public GameObject AimPlane;

    public List<int> Weapons;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        Weapons = new List<int>(new int[] { 0, 1 });

        AimPlane = GetComponentInChildren<AimPlane>().gameObject;
        DontDestroyOnLoad(this);
        onUpdate += nothing;
    }

    protected void nothing()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reload(float t)
    {
        onReload(t);
    }

    public void RecoilChange(float rec)
    {
        onRecoilChange(rec);
    }

    public void Shot()
    {
        onShot();
    }

    protected virtual void _onUpdate()
    {
        onUpdate();
    }
}
