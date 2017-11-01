using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractorController : MonoBehaviour {

    [SerializeField]
    private InterfaceObject io;
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().enabled = Mecha_InventoryManager.Instance.touchingPickup();
        io.enabled = Mecha_InventoryManager.Instance.touchingPickup();
    }
}
