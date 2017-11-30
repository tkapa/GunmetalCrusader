using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceObject : MonoBehaviour {

    // Enum Declaration
    public enum interfaceEvent { ie_None, ie_StartGame, ie_ExitGame, ie_OpenCredits,ie_CloseCredits };
    public enum interfactButton { ib_None, ie_TouchOnly, ib_CirclePad, ib_Trigger, ib_TriggerAndCirclePad, ib_Grip };

    // The event to be executed upon usage of this interface point.
    [Tooltip("The event to be executed upon usage of this interface point.")]
    [SerializeField]
    private interfaceEvent iEvent = interfaceEvent.ie_None;

    // The event to be executed upon usage of this interface point.
    [Tooltip("The button that causes this interface point to be used.")]
    [SerializeField]
    private interfactButton iButton = interfactButton.ib_None;

    // The index of this weapon. Generally speaking for GC:SO, 0 = Left Arm, 1 = Right Arm, 2 = Jump jet target locator, 3 = Missile Pods if I get my way ;).
    [Tooltip("The index of this weapon. Generally speaking for GC:SO, 0 = Left Arm, 1 = Right Arm, 2 = Jump jet target locator, 3 = Missile Pods if I get my way ;)")]
    public int weaponPointIndex = 0;

    // The amount of time between allowed uses of this object.
    [Tooltip("The amount of time between allowed uses of this object.")]
    [SerializeField]
    private float FreezeInterval = 1.0f;

    private float FreezeTimer = 0.0f;

    private bool pickedItemExists = false;

    

    // Init
    void Start()
    {
        this.transform.tag = "InterfacePoint";
    }

    // Update
    void Update()
    {
        FreezeTimer -= Time.deltaTime;
    }

    // Called to test if the event was executed.
    public void ExecuteEvent(VRTK.VRTK_ControllerEvents cEvents)
    {
        if (FreezeTimer <= 0.0f && this.enabled == true)
        {
            // Input checking
            switch (iButton)
            {
                case interfactButton.ie_TouchOnly:
                    {
                        break;
                    }
                case interfactButton.ib_CirclePad:
                    {
                        if (!cEvents.touchpadPressed)
                            return;
                        break;
                    }
                case interfactButton.ib_Trigger:
                    {
                        if (!cEvents.triggerClicked)
                            return;
                        break;
                    }
                case interfactButton.ib_Grip:
                    {
                        if (!cEvents.gripClicked)
                            return;
                        break;
                    }
                case interfactButton.ib_TriggerAndCirclePad:
                    {
                        if (GameObject.FindGameObjectWithTag("ControllerUsingObj_" + weaponPointIndex.ToString()))
                        {
                            if (!cEvents.triggerClicked)
                                return;
                        }
                        else
                        {
                            if (!cEvents.touchpadPressed)
                                return;
                        }
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            // Functionality
            switch (iEvent)
            {
                case interfaceEvent.ie_StartGame:
                    {


                        Debug.Log("GameStart");
                        GameObject.FindGameObjectWithTag("Player").GetComponent<GameStartHandler>().GameStarted();
                      //  EventManager.instance.OnUsePickup.Invoke(weaponPointIndex);
                        break;
                    }

                case interfaceEvent.ie_ExitGame:
                    {
                        Debug.Log("Quit");
                        GameObject.FindGameObjectWithTag("Player").GetComponent<GameStartHandler>().GameEnd();
                        //  EventManager.instance.OnUsePickup.Invoke(weaponPointIndex);
                        break;
                    }

                case interfaceEvent.ie_OpenCredits:
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<GameStartHandler>().OpenCredits();
                        break;
                    }

                case interfaceEvent.ie_CloseCredits:
                        
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<GameStartHandler>().CloseCredits();
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            // Play an Animation
            PlayEffects();

            // Reset the interval timer to prevent button spam
            FreezeTimer = FreezeInterval;
        }
    }

    // Play an Animation
    protected virtual void PlayEffects()
    { }

    public int getID()
    {
        return weaponPointIndex;
    }
}
