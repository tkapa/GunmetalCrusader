using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mekarm_Controller : MonoBehaviour {

    GameObject inventory;
	
	// Update is called once per frame
	void Update () {
		if(!inventory)
        {
            inventory = Mecha_InventoryManager.Instance.gameObject;
        }else
        {
            this.transform.LookAt(inventory.transform);
        }
	}
}
