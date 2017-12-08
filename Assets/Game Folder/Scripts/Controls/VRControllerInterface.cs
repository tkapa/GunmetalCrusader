using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class VRControllerInterface : GamepadPointer { 

    // Collision Data
    private Collider col;
    private Rigidbody rb;

    // Controller Data
    private VRTK.VRTK_ControllerEvents cEvents = null;

    private VRTK.VRTK_ControllerReference cReference = null;

    public GameObject CirclePrompt;

    // Current Overlapped Interface Object
    [SerializeField]
    //private List<InterfaceObject> io = new List<InterfaceObject>();
    private Dictionary<InterfaceObject, float> io2 = new Dictionary<InterfaceObject, float>();

    //public GameObject fuckmylifeOBJ;
    //public float fuckmylifeINT = 0.25f;

    // Linked Weapon Index
    public int linkedweap = -1;

    // OtherController
    public VRControllerInterface otherController;

    // ResetSideTimer
    public float holdMaxTimer = 1.2f;
    private float holdTimer = 0.0f;

    // Trigger pressed
    private bool triggerPressed = false;
    private bool touchpadPressed = false;
    

    // Initializes the controller
    protected override void Start()
    {
        base.Start();

        // Set up the Collider
        col = this.GetComponent<Collider>();
        col.isTrigger = false;

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

        // Resets the Controller Layout //
        if (linkedweap == -1 || (holdTimer >= holdMaxTimer && cEvents.gripClicked)){
            initController();
        }else if(cEvents.gripClicked){
            holdTimer += Time.deltaTime;
        }
        else{
            holdTimer = 0.0f;
        }

        if (Player.p.HasDoneJumpTutorial && !Player.p.HasJumpedYet)
            CirclePrompt.SetActive(true);
        else
            CirclePrompt.SetActive(false);


        /////////////////////////////////////////////////////////////
        // THIS SHIT IS FUCKED //
        //if (Vector3.Distance(this.transform.position, fuckmylifeOBJ.transform.position) < fuckmylifeINT)
        //{
        //    io2[fuckmylifeOBJ.GetComponent<InterfaceObject>()] = Time.time + 0.1f;
        //}

        // Tell the Interface Object that we're touching to check if it can do it's stuff.
        List<InterfaceObject> keyCopy = io2.Keys.ToList();
        for (int index = 0; index < keyCopy.Count; ++index)
        {
            if (io2[keyCopy[index]] > Time.time)
            {
                keyCopy.RemoveAt(index);
                --index;
            }
        }
        foreach (InterfaceObject obj in keyCopy)
        {
            io2.Remove(obj);
        }

        if (io2.Count > 0)
        {
            foreach (InterfaceObject i in io2.Keys) { i.ExecuteEvent(cEvents); }
        }

        /////////////////////////////////////////////////////////////
        /*
        if (cEvents.touchpadPressed && !circlePressed)
        {
            //Unlink Here
            tag = "UntaggedController";
            EventManager.instance.OnWeaponEquip.Invoke(linkedweap);
            linkedweap = -1;
            displayLines = false;

            circlePressed = true;
        }
        else if (!cEvents.touchpadPressed && circlePressed)
        {
            circlePressed = false;
        }
        */

        // Circle Pad //
        if (cEvents.touchpadPressed && !touchpadPressed)
        {
            Debug.Log("about to send the weaponswitch");
            EventManager.instance.OnWeaponSwitch.Invoke(linkedweap   );
            
            touchpadPressed =true;

        }

        else if (!cEvents.touchpadPressed)
        {
            touchpadPressed = false;
        }

        // Weapon Firing //
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

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "InterfacePoint")
        {
            io2[other.gameObject.GetComponent<InterfaceObject>()] = Time.time + 0.1f;
        }
    }

    protected override void initController()
    { //if weapon not enabled, cancel the function
        if (FindObjectsOfType<WeaponMaster>().Length <=0)
        {
            return;
        }

        if(this.transform.localPosition.x < otherController.gameObject.transform.localPosition.x)
        {
            linkedweap = 1;
            otherController.linkedweap = 0;
        }
        else
        {
            linkedweap = 0;
            otherController.linkedweap = 1;
        }
        Debug.Log("we are about to call the eventmanager instance");
        EventManager.instance.OnWeaponInit.Invoke(0);
        EventManager.instance.OnWeaponInit.Invoke(1);
        Debug.Log("called eventmanager instance");
    }
}
