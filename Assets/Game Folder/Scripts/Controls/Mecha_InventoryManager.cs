using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_InventoryManager : MonoBehaviour {

    [Tooltip("The points on the Mech that weapons will be spawned and parented to.")]
    [SerializeField]
    private Transform[] mechaWeaponSockets = new Transform[3];

    [SerializeField]
    private GameObject[] TEMPORARY_WpRefs = new GameObject[3];

    // Use this for initialization
    void Start () {
        // TODO: These should be added and taken away as the player enters and leaves a "match" by the Game Manager, not this script.
        for (int i = 0; i < TEMPORARY_WpRefs.Length; i++)
            AddWeapon(TEMPORARY_WpRefs[i], i);
    }
	
	public void AddWeapon(GameObject weaponPrefab, int socketIndex)
    {
        if(weaponPrefab.GetComponent<WeaponMaster>() == null)
        {
            Debug.Log("ERROR! Attempting to equip an object which is not a Weapon! Ensure that the object has a Weapon script attached.");
            return;
        }

        if(socketIndex > mechaWeaponSockets.Length || socketIndex < 0)
        {
            Debug.Log("ERROR! Attempting to equip a Weapon into a Socket which does not exist!");
            return;
        }

        GameObject wp = (GameObject)Instantiate(weaponPrefab, mechaWeaponSockets[socketIndex]);

        wp.GetComponent<WeaponMaster>().SetWeaponPointIndex(socketIndex);
    }
}
