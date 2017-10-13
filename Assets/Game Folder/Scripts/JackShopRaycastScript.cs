using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackShopRaycastScript : MonoBehaviour {

    //reference to the shop manager for ease of life
    public GameObject ShopManager;
    public bool ShopOpen;


    //fairly self explanatory, doing it the ez way. 
    public GameObject WeaponRightBuy;
    public GameObject WeaponLeftBuy;

    public GameObject AmmoRightBuy;
    public GameObject AmmoLeftBuy;

    public GameObject ExitSign;

    public GameObject SMGButton;
    public GameObject LMGButton;
    public GameObject GrenButton;
    public GameObject RailButton;
    public GameObject FlakButton;

    
    // Controller Data
    private VRTK.VRTK_ControllerEvents cEvents = null;

    // Use this for initialization
    void Start () {
        // Set up the Controller Events
        cEvents = this.gameObject.GetComponent<VRTK.VRTK_ControllerEvents>();
        ShopOpen = false;
    }
	
	
    void FixedUpdate()
       {
        //the shop manager controls the "SHopOpen" bool, which is itself controlled by the eventmanager
        if (ShopOpen)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                //if we are hitting a button when they press click then activate that function in the shop manager
                print(hit.transform.name);
                if (cEvents.triggerClicked)
                {
                    ButtonPressed(hit.transform.gameObject);
                }

            }
        }
           
    }

    void ButtonPressed(GameObject Icon)
    {
        if (Icon == ExitSign)
        {
            ShopManager.GetComponent<JackShopTestScript>().CalledShopClosed();
            ShopOpen = false;
        }

        if (Icon == WeaponLeftBuy)
        {
            ShopManager.GetComponent<JackShopTestScript>().CalledBuyNewLeft();
        }

        if (Icon == WeaponRightBuy)
        {
            ShopManager.GetComponent<JackShopTestScript>().CalledBuyNewRight();
        }

        if (Icon == AmmoLeftBuy)
        {
            ShopManager.GetComponent<JackShopTestScript>().CalledBuyAmmoLeft();
        }

        if (Icon == AmmoRightBuy)
        {
            ShopManager.GetComponent<JackShopTestScript>().CalledBuyAmmoRight();
        }

        if (Icon == SMGButton)
        {
            ShopManager.GetComponent<JackShopTestScript>().SelectedSMG();
        }

        if (Icon == LMGButton)
        {
            ShopManager.GetComponent<JackShopTestScript>().SelectedLMG();
        }

        if (Icon == GrenButton)
        {
            ShopManager.GetComponent<JackShopTestScript>().SelectedGren();
        }

        if (Icon == RailButton)
        {
            ShopManager.GetComponent<JackShopTestScript>().SelectedRail();
        }

        if (Icon == FlakButton)
        {
            ShopManager.GetComponent<JackShopTestScript>().SelectedFlak();
        }
    }
    
}
