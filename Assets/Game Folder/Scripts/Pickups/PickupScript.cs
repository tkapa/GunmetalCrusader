using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    public enum pickupType { pt_None, pt_Weapon, pt_Sticker };

    // The event to be executed upon usage of this interface point.
    [Tooltip("The event to be executed upon usage of this interface point.")]
    [SerializeField]
    private pickupType pType = pickupType.pt_None;

    [SerializeField]
    private GameObject CabinDisplay;

    [SerializeField]
    private GameObject WeaponPrefab;

    void Start()
    {
        EventManager.instance.OnUsePickup.AddListener((i) => {

            if (Mecha_InventoryManager.Instance.touchingPickup())
            {
                OnUsePickup(i);
            }
        });
    }

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

    public void OnUsePickup(int usageIndex)
    {
        switch(pType)
        {
            case pickupType.pt_Weapon:
                Mecha_InventoryManager.Instance.AddWeapon(WeaponPrefab, usageIndex);
                break;
            default:
                break;
        }

        Destroy(this.gameObject);
    }
}
