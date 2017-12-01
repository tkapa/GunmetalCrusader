using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [Tooltip("Denotes whether or not we should be using VR Peripherals as our method of input or a Gamepad.")]
    public bool useGamePad = false;

    [Tooltip("Reference to the parent GameObject that holds the VRTK Rig.")]
    [SerializeField]
    private GameObject VRTKRig;

    [Tooltip("Reference to the parent object under which all the subobjects used in Gamepad mode are contained.")]
    [SerializeField]
    private GameObject GamepadControlObjectParent;

    [Tooltip("Reference to the Camera that is used when we are in Gamepad mode.")]
    [SerializeField]
    private Camera GamepadCam;

    [Tooltip("Reference to the GamepadPointer that is used for aimin when we are in Gamepad mode.")]
    [SerializeField]
    private GamepadPointer GamepadTargetLocator;

    [Tooltip("How sensitive the triggers are to usage.")]
    [SerializeField]
    private float TriggerDeadZone = 0.8f;

    [Tooltip("How fast does the camera rotate when using Gamepad controls.")]
    [SerializeField]
    private float CameraLookSpeed = 1.0f;

    [Tooltip("Is the camera inverted when using Gamepad controls?")]
    [SerializeField]
    private bool CameraInversion = false;

    private bool leftTriggerDown = false;
    private bool rightTriggerDown = false;
    private bool snapBtnDown = false;
    private bool reloadBtnDown = false;
    private bool jetBtnDown = false;

    private int leftWeaponIndex = -1;
    private int rightWeaponIndex = -1;

    // Initialize as Singleton
    public static InputManager inst;

    // Disables VR systems if using Gamepad input and activates our Camera.
    private void Start ()
    {
        GamepadControlObjectParent.SetActive(useGamePad);
        VRTKRig.SetActive(!useGamePad);

        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }

    // If we are using Gamepad Input, then read from the Gamepad and call events accordingly.
    private void Update()
    {
        if (useGamePad)
        {
            // Trigger Input //
            
            // Left Weapon
            if (Input.GetAxis("FireWeaponL") >= TriggerDeadZone && !leftTriggerDown)
            {
                EventManager.instance.OnWeaponFire.Invoke(leftWeaponIndex, true);
                leftTriggerDown = true;
            }
            else if (Input.GetAxis("FireWeaponL") < TriggerDeadZone && leftTriggerDown)
            {
                //EventManager.instance.OnWeaponFire.Invoke(leftWeaponIndex, false);
                leftTriggerDown = false;
            }

            // Right Weapon
            if (Input.GetAxis("FireWeaponR") >= TriggerDeadZone && !rightTriggerDown)
            {
                EventManager.instance.OnWeaponFire.Invoke(rightWeaponIndex, true);
                rightTriggerDown = true;
            }
            else if (Input.GetAxis("FireWeaponR") < TriggerDeadZone && rightTriggerDown)
            {
                EventManager.instance.OnWeaponFire.Invoke(rightWeaponIndex, false);
                rightTriggerDown = false;
            }
        }
    }
}
