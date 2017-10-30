using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJumpLocator : WeaponMaster {

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    [SerializeField]
    private GameObject jumpTargetObject;

    // The game object that was spawned.
    private GameObject spawnedJumpTarget;

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    [SerializeField]
    private float MaxCharge = 100.0f;

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    [SerializeField]
    private float ChargeSpeed = 40.0f;

    // The amount the locator has charged.
    private float ChargeAmount = 0.0f;

    // Is the locator locked in.
    private bool LockedIn = false;

    // The Color of the Locator when it's inactive.
    [Tooltip("The Color of the Locator when it's active.")]
    [SerializeField]
    protected Color ActiveReticuleColor;

    // The Color of the Locator when it's confirmed.
    [Tooltip("The Color of the Locator when it's confirmed.")]
    [SerializeField]
    protected Color ConfirmedReticuleColor;

    // The Color of the Locator when it's blocked.
    [Tooltip("The Color of the Locator when it's blocked.")]
    [SerializeField]
    protected Color BlockedReticuleColor;

    protected override void Start()
    {
        base.Start();
        // Bind Events
        EventManager.instance.OnMechaJumpEnd.AddListener(() =>
        {
            ResetTargetting();
        });
    }

    /*
     * Called once per frame
     */
    protected override void Update()
    {
        base.Update();
        UpdateJumpTarget();
        ChargeJumpTarget();
        SetReticuleColor();
    }

    // Updates and spawns the jump target locator
    private void UpdateJumpTarget()
    {
        if (isEquipped && !LockedIn)
        {
            if (spawnedJumpTarget == null)
                spawnedJumpTarget = (GameObject)Instantiate(jumpTargetObject, new Vector3(0,0,0), Quaternion.identity);

            GameObject myInterface;
            if (InputManager.inst.useGamePad)
            {
                // TODO: Move this into the IK script for the Mech's Arms
                myInterface = GameObject.FindGameObjectWithTag("UsingGamepadControllerObj");

                if (myInterface != null)
                    spawnedJumpTarget.transform.position = (myInterface.GetComponent<GamepadPointer>().GetHitLocation());
            }
            else
            {
                // TODO: Move this into the IK script for the Mech's Arms
                myInterface = GameObject.FindGameObjectWithTag("ControllerUsingObj_" + weaponPointIndex.ToString());

                if (myInterface != null)
                    spawnedJumpTarget.transform.position = (myInterface.GetComponent<VRControllerInterface>().GetHitLocation());
            }

        } else if (spawnedJumpTarget != null && !LockedIn)
            ResetTargetting();
    }

    //
    private void ResetTargetting()
    {
        ChargeAmount = 0.0f;
        Destroy(spawnedJumpTarget);
        LockedIn = false;
    }

    // FiredFunctionality
    private void ChargeJumpTarget()
    {
        bool isCol = false;

        GameObject myInterface;
        if (InputManager.inst.useGamePad)
        {
            // TODO: Move this into the IK script for the Mech's Arms
            myInterface = GameObject.FindGameObjectWithTag("UsingGamepadControllerObj");

            if (myInterface != null)
                isCol = (myInterface.GetComponent<GamepadPointer>().testHitObjectAgainst(null));
        }
        else
        {
            // TODO: Move this into the IK script for the Mech's Arms
            myInterface = GameObject.FindGameObjectWithTag("ControllerUsingObj_" + weaponPointIndex.ToString());

            if (myInterface != null)
                isCol = (myInterface.GetComponent<VRControllerInterface>().testHitObjectAgainst(null));
        }

        if (myInterface != null && !isCol) {
            
            if (isEquipped)
            {
                if (isFiring)
                {
                    ChargeAmount = Mathf.Clamp(ChargeAmount + (ChargeSpeed * Time.deltaTime), 0.0f, MaxCharge);
                    if (ChargeAmount >= MaxCharge && !LockedIn)
                    {
                        LockedIn = true;
                        EventManager.instance.OnMechaJumpStart.Invoke();
                    }
                }
                else if (!LockedIn)
                    ChargeAmount = Mathf.Clamp(ChargeAmount - (ChargeSpeed * 2.5f * Time.deltaTime), 0.0f, MaxCharge);

                if (GameObject.FindGameObjectWithTag("JumpReticuleChargeBar").GetComponent<PercentScale>() != null)
                    GameObject.FindGameObjectWithTag("JumpReticuleChargeBar").GetComponent<PercentScale>().ScalePercent = new Vector3((ChargeAmount / MaxCharge) * 100.0f, 100.0f, 100.0f);
            }
        }
    }

    // Reticule Colour
    private void SetReticuleColor()
    {
        if (isEquipped && spawnedJumpTarget != null)
        {
            Color tmpCol = Color.gray;

            if (LockedIn)
                tmpCol = ConfirmedReticuleColor;
            else
                tmpCol = ActiveReticuleColor;

            ColourSet c = FindObjectOfType<ColourSet>();
            if(c)
                c.ChangeColour(tmpCol);
        }
    }
}
