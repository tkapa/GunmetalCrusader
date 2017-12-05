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

    // Holds whether the weapon is currently firing.
    protected bool isFiring = false;

    private float PickupDelay = 0.25f;
    private float PickupDelayTimer = 0.0f;

    [SerializeField] // TEMP
    protected GamepadPointer vrCont;

    // Is the weapon in target locator mode or is it in firing mode
    private bool isInJumpMode = false;

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    [SerializeField]
    private GameObject jumpTargetObject;

    // The game object that was spawned.
    private GameObject spawnedJumpTarget;

    private Mecha_MovementHandler playerjumper;

    // How long the jump takes to charge in seconds.
    [Tooltip("How long the jump takes to charge in seconds.")]
    [SerializeField]
    private float jumpChargeTime = 3.0f;

    private float jumpChargeTimer = 0.0f;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
        Debug.Log("called weaponmaster start");
        // Bind Events
        EventManager.instance.OnWeaponSwitch.AddListener((i) =>
        {
            if (i == weaponPointIndex)
            {
                isInJumpMode = !isInJumpMode;
                OnFireInput(false);
            }
        });

        EventManager.instance.OnWeaponInit.AddListener((cref, i) =>
        {
            if (i == weaponPointIndex)
                vrCont = cref;
            Debug.Log(vrCont.name);
           // Debug.Log("lMAOOOOOOOOOOOOOOO");
        });

        EventManager.instance.OnWeaponFire.AddListener((i, b) =>
        {
            if (i == weaponPointIndex)
                OnFireInput(b);
        });

        // Initializing variables
        playerjumper = FindObjectOfType<Mecha_MovementHandler>();
    }

    /*
     * Called once per frame
     */
    protected virtual void Update()
    {
        UpdateWeaponAim();
        PickupDelayTimer -= Time.deltaTime;

        if (isInJumpMode)
            jumpChargeTimer = Mathf.Clamp(jumpChargeTimer + Time.deltaTime, 0, jumpChargeTime);
        else
            jumpChargeTimer = Mathf.Clamp(jumpChargeTimer - Time.deltaTime, 0, jumpChargeTime);
    }

    // Points the weapon at the spot it's aiming at
    protected virtual void UpdateWeaponAim()
    {
        this.transform.LookAt(vrCont.GetHitLocation());
        Debug.Log("vr cont in update weapon" + vrCont.name);
    }

    // Called when the weapon receives fire input
    protected virtual void OnFireInput(bool startFire)
    {
        if(startFire && isInJumpMode)
        {
            // Do jump checking and stuff here
            if(jumpChargeTimer >= jumpChargeTime)
            {
                // Todo: Move the isCol stuff somewhere else to change the colour of lasers
                bool isCol = vrCont.testHitObjectTag("Floor");

                if (!isCol)
                    EventManager.instance.OnMechaJumpStart.Invoke();
                else
                {
                    // Notify player they can't jump to that surface
                }
            }
        }
        else
            isFiring = startFire;
    }

    // Sets the weapon point index
    public void SetWeaponPointIndex(int i)
    {
        weaponPointIndex = i;
    }

    // Updates and spawns the jump target locator
    private void UpdateJumpTarget()
    {
        if (!isInJumpMode){
            Destroy(spawnedJumpTarget);
            return;
        }
        else if (playerjumper.isJumping())
        {
            return;
        }
        else { 
            if (spawnedJumpTarget == null)
                spawnedJumpTarget = (GameObject)Instantiate(jumpTargetObject, new Vector3(0, 0, 0), Quaternion.identity);

            spawnedJumpTarget.transform.position = vrCont.GetHitLocation();
        }
    }
}
