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

            // Bumper Input //

            // TODO: THat


            /*
            // Face Button Inputs //
            reloadBtnDown = Input.GetKey(KeyCode.Joystick1Button2);
            snapBtnDown = Input.GetKey(KeyCode.Joystick1Button8);
            int invert = -1; if (CameraInversion) { invert = 1; } // Grab Inversion Multiplier

            // Y Button // 
            if (Input.GetKey(KeyCode.Joystick1Button3))
            {
                //if (FindObjectOfType<WeaponShopActivator>().GetPlayerColliding() && Input.GetKeyDown(KeyCode.Joystick1Button3))
                //{
                    //jetBtnDown = false;
                    //if(!GameObject.FindGameObjectWithTag("WeaponShop").GetComponent<WeaponShopController>().CheckIfShopOpen())
                        //EventManager.instance.OnToggleShop.Invoke(true);
                    //else
                        //EventManager.instance.OnToggleShop.Invoke(false);
                //}
                //else
                    jetBtnDown = true;
            }else
                jetBtnDown = false;

            // Camea Look //
            Vector3 tempRotation = GamepadCam.transform.localEulerAngles; // Grab old rotation

            tempRotation.y = tempRotation.y + Input.GetAxis("HorizontalLook") * (CameraLookSpeed * Time.deltaTime); // Apply Yaw

            tempRotation.x = tempRotation.x + Input.GetAxis("VerticalLook") * (CameraLookSpeed * -invert * Time.deltaTime); // Apply Pitch

            GamepadCam.transform.localEulerAngles = tempRotation; // Reapply to the Camera

            // Targetting //
            if (!snapBtnDown) // Ignore this and set it to the camera's rotation if the snap button is down
            {
                tempRotation = GamepadTargetLocator.transform.localEulerAngles; // Grab old rotation

                tempRotation.y = tempRotation.y + Input.GetAxis("Horizontal") * (CameraLookSpeed / 2 * Time.deltaTime); // Apply Yaw

                tempRotation.x = tempRotation.x + Input.GetAxis("Vertical") * (CameraLookSpeed / 2 * invert * Time.deltaTime); // Apply Pitch
            }
            GamepadTargetLocator.transform.localEulerAngles = tempRotation; // Reapply to the Camera

            // Left Weapon //
            // Equip/Reload
            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                if (reloadBtnDown) // Do Reload
                    EventManager.instance.OnWeaponReload.Invoke(leftWeaponIndex);
                else
                {
                    if (jetBtnDown) // Set index to the jump jets
                        leftWeaponIndex = 2;
                    else // Set index to the left-hand weapon
                        leftWeaponIndex = 0;

                    EventManager.instance.OnWeaponEquip.Invoke(leftWeaponIndex);
                }
            }

            //Fire/Unfire
            if (Input.GetAxis("FireWeaponL") >= TriggerDeadZone && !leftTriggerDown)
            {
                EventManager.instance.OnWeaponFire.Invoke(leftWeaponIndex, true);
                leftTriggerDown = true;
            }
            else if (Input.GetAxis("FireWeaponL") < TriggerDeadZone && leftTriggerDown)
            {
                EventManager.instance.OnWeaponFire.Invoke(leftWeaponIndex, false);
                leftTriggerDown = false;
            }

            // Left Weapon //
            // Equip/Reload
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                if (reloadBtnDown) // Do Reload
                    EventManager.instance.OnWeaponReload.Invoke(rightWeaponIndex);
                else
                {
                    if (jetBtnDown) // Set index to the jump jets
                        rightWeaponIndex = 2;
                    else // Set index to the left-hand weapon
                        rightWeaponIndex = 1;

                    EventManager.instance.OnWeaponEquip.Invoke(rightWeaponIndex);
                }
            }

            //Fire/Unfire
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
            */
        }
    }
}
