using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    public enum pickupType { pt_None, pt_Weapon, pt_Sticker };

    [SerializeField]
    private GameObject WeaponPrefab;

    public void OnUsePickup(int usageIndex)
    {
        Mecha_InventoryManager.Instance.AddWeapon(WeaponPrefab, usageIndex);

        Destroy(this.gameObject);
    }
}
