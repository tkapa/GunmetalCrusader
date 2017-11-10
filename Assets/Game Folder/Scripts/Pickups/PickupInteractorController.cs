using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractorController : MonoBehaviour {

    [SerializeField]
    private InterfaceObject io;
	
	// Update is called once per frame
	void Update () {
        if (Mecha_InventoryManager.Instance)
        {
            GetComponent<Renderer>().enabled = Mecha_InventoryManager.Instance.touchingPickup();
            GetComponent<Collider>().enabled = Mecha_InventoryManager.Instance.touchingPickup();
           // io.enabled = Mecha_InventoryManager.Instance.touchingPickup();
        }else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
           // io.enabled = false;
        }
    }
}
