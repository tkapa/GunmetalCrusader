using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMaster : MonoBehaviour {

    // The index of this weapon. Generally speaking for GC:SO, 0 = Left Arm, 1 = Right Arm, 2 = Jump jet target locator, 3 = Missile Pods if I get my way ;).
    [Tooltip("The index of this weapon. Generally speaking for GC:SO, 0 = Left Arm, 1 = Right Arm, 2 = Jump jet target locator, 3 = Missile Pods if I get my way ;)")]
    [SerializeField]
    protected int weaponPointIndex = 0;

    // The name of this weapon.
    [Tooltip("The name of this weapon.")]
    [SerializeField]
    protected string weaponName = "DUMMY_WEAPON_NAME";

    // Is the weapon currently active and in use?
    protected bool isEquipped = false;

    // Holds whether the weapon is currently firing.
    protected bool isFiring = false;

    // Holds whether the weapon is currently firing.
    protected bool isReloading = false;

    private float PickupDelay = 0.25f;
    private float PickupDelayTimer = 0.0f;

    protected VRControllerInterface vrCont;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
        // Bind Events
        EventManager.instance.OnWeapopnInit.AddListener((cref, i) =>
        {
            if (i == weaponPointIndex)
                vrCont = cref;
        });

        EventManager.instance.OnWeaponEquip.AddListener((i) =>
        {
            if (i == weaponPointIndex)
                OnEquip();
        });

        EventManager.instance.OnWeaponFire.AddListener((i, b) =>
        {
            if (i == weaponPointIndex)
                OnFireInput(b);
        });

        EventManager.instance.OnWeaponReload.AddListener((i) =>
        {
            if (i == weaponPointIndex)
                OnReload();
        });

        EventManager.instance.OnWeaponEquipAndReload.AddListener((i) =>
        {
            if (i == weaponPointIndex)
            {
                if (isEquipped)
                    OnReload();
                else
                    OnEquip();
            }
        });
    }

    /*
     * Called once per frame
     */
    protected virtual void Update()
    {
        UpdateWeaponAim();
        PickupDelayTimer -= Time.deltaTime;
    }

    // Points the weapon at the spot it's aiming at
    protected virtual void UpdateWeaponAim()
    {
        if (isEquipped)
            this.transform.LookAt(vrCont.GetHitLocation());
    }

    // Called when the weapon is equipped
    protected virtual void OnEquip()
    {
        isEquipped = !isEquipped;
        if (isEquipped)
            PickupDelayTimer = PickupDelay;
    }

    // Called when the weapon receives fire input
    protected virtual void OnFireInput(bool startFire)
    {
        if (isEquipped && PickupDelayTimer <= 0.0f)
            isFiring = startFire;
    }

    // Called when the weapon reload is called.
    protected virtual void OnReload()
    {
        isReloading = true;
    }

    // Sets the weapon point index
    public void SetWeaponPointIndex(int i)
    {
        weaponPointIndex = i;
    }
}
