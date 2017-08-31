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
    private bool isFiring = false;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
        // Bind Events
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
    }

    /*
     * Called once per frame
     */
    protected virtual void Update()
    {
        // TODO: Move this into the IK script for the Mech's Arms
        GameObject myInterface = GameObject.FindGameObjectWithTag("ControllerUsingObj_" + weaponPointIndex.ToString());

        if (myInterface != null)
            this.transform.LookAt(myInterface.GetComponent<VRControllerInterface>().GetHitLocation());
    }

    protected virtual void OnEquip()
    {
        isEquipped = !isEquipped;
        if (isEquipped)
            Debug.Log(weaponName + " at " + weaponPointIndex.ToString() + " equipped.");
        else
            Debug.Log(weaponName + " at " + weaponPointIndex.ToString() + " is no longer equipped.");
    }

    protected virtual void OnFireInput(bool startFire)
    {
        if (isEquipped)
        {
            isFiring = startFire;
            if (isFiring)
                Debug.Log(weaponName + " at " + weaponPointIndex.ToString() + " has begun firing sequence.");
            else
                Debug.Log(weaponName + " at " + weaponPointIndex.ToString() + " has halted firing sequence.");
        }
        else
            Debug.Log("WARNING: " + weaponName + " at " + weaponPointIndex.ToString() + " attempted fire without being equipped.");
    }

    protected virtual void OnReload()
    {
        Debug.Log(weaponName + " at " + weaponPointIndex.ToString() + " attempted reload.");
    }

    public void SetWeaponPointIndex(int i)
    {
        weaponPointIndex = i;
    }
}
