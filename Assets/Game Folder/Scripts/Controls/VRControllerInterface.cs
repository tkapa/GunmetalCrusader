using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class VRControllerInterface : GamepadPointer { 

    // Collision Data
    private SphereCollider col;
    private Rigidbody rb;

    // Controller Data
    private VRTK.VRTK_ControllerEvents cEvents = null;

    // Current Overlapped Interface Object
    private InterfaceObject io = null;

    // Linked Weapon Index
    public int linkedweap = -1;

    // Trigger pressed
    private bool triggerPressed = false;
    private bool circlePressed = false;

    // Initializes the controller
    protected override void Start()
    {
        base.Start();

        // Set up the Collider
        col = this.GetComponent<SphereCollider>();
        col.isTrigger = false;
        col.radius = 0.2f;

        // Set up the Rigid Body
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // Set up the Controller Events
        cEvents = this.gameObject.GetComponent<VRTK.VRTK_ControllerEvents>();

        // Hide the lines
        displayLines = false;
    }

    protected override void Update()
    {
        base.Update();

        Debug.Log(tag);

        // Tell the Interface Object that we're touching to check if it can do it's stuff.
        if (io)
        {
            io.ExecuteEvent(cEvents);
        }

        if (cEvents.touchpadPressed && !circlePressed)
        {
            if(io == null)
            {
                //Unlink Here
                tag = "UntaggedController";
                EventManager.instance.OnWeaponEquip.Invoke(linkedweap);
                linkedweap = -1;
                displayLines = false;
            }

            circlePressed = true;
        }
        else if (!cEvents.touchpadPressed && circlePressed)
        {
            circlePressed = false;
        }

        // Set weapon to start firing
        if (cEvents.triggerPressed && !triggerPressed)
        {
            EventManager.instance.OnWeaponFire.Invoke(linkedweap, true);
            triggerPressed = true;
        }
        // Set weapon to stop firing
        else if (!cEvents.triggerPressed && triggerPressed)
        {
            EventManager.instance.OnWeaponFire.Invoke(linkedweap, false);
            triggerPressed = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "InterfacePoint")
        {
            io = other.gameObject.GetComponent<InterfaceObject>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "InterfacePoint")
        {
            io = null;
        }
    }
}
