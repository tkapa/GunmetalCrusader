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
    protected VRControllerInterface vrCont;

    // Is the weapon in target locator mode or is it in firing mode
    protected bool isInJumpMode = false;

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    [SerializeField]
    private GameObject jumpTargetObject;

    // The game object that was spawned.
    private GameObject spawnedJumpTarget;

    private Mecha_MovementHandler playerjumper;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
       // Debug.Log("called weaponmaster start");
        // Bind Events
        EventManager.instance.OnWeaponSwitch.AddListener((i) =>
        {
            if (i == weaponPointIndex && !Player.p.Locked)
            {
                isInJumpMode = !isInJumpMode;
                OnFireInput(false);
            }
        });

        EventManager.instance.OnWeaponInit.AddListener(( i) =>
        {
            if (i == weaponPointIndex)
            {
                foreach(VRControllerInterface vr in FindObjectsOfType<VRControllerInterface>())
                {
                    if (weaponPointIndex == vr.linkedweap)
                    {
                        vrCont = vr;
                        break;
                    }
                }

            }
        });

        EventManager.instance.OnWeaponFire.AddListener((i, b) =>
        {
            if (i == weaponPointIndex){
                OnFireInput(b);
            }
        });

        // Initializing variables
        playerjumper = FindObjectOfType<Mecha_MovementHandler>();
    }

    /*
     * Called once per frame
     */
    protected virtual void Update()
    {
        if (!vrCont)
        {
            foreach (VRControllerInterface vr in FindObjectsOfType<VRControllerInterface>())
            {
                if (weaponPointIndex == vr.linkedweap)
                {
                    vrCont = vr;
                    break;
                }
            }
        }

        UpdateJumpTarget();
        UpdateWeaponAim();
        PickupDelayTimer -= Time.deltaTime;

        if (isInJumpMode)
            playerjumper.SetJumpCharge();
    }

    // Points the weapon at the spot it's aiming at
    protected virtual void UpdateWeaponAim()
    {
        this.transform.LookAt(vrCont.GetHitLocation());
    }

    // Called when the weapon receives fire input
    protected virtual void OnFireInput(bool startFire)
    {
        if (startFire && isInJumpMode && !Player.p.Locked)
        {
            // Do jump checking and stuff here
            if (playerjumper.isCharged())
            {
                // Todo: Move the isCol stuff somewhere else to change the colour of lasers
                bool isCol = vrCont.testHitObjectTag("Floor");

                if (!isCol)
                {
                    EventManager.instance.OnMechaJumpStart.Invoke();

                    isInJumpMode = false;
                    Player.p.HasJumpedYet = true;
                }

                else
                {
                    // Notify player they can't jump to that surface
                }
            }
        }
        else
        {
            isFiring = startFire;
        }
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
            if (!playerjumper.isJumping())
            {
                Destroy(spawnedJumpTarget);
            }
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
