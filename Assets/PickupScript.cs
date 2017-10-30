using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    [SerializeField]
    private GameObject CabinDisplay;

    [SerializeField]
    private GameObject WeaponPrefab;

    // Update is called once per frame
    void Update () {
        if (Mecha_InventoryManager.Instance.comparePickup(this.gameObject))
        {
            CabinDisplay.SetActive(true);
        }
        else
        {
            CabinDisplay.SetActive(false);
        }
	}

    public void OnUsePickup()
    {
        
    }
}
