using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_InventoryManager : MonoBehaviour {

    public static Mecha_InventoryManager Instance;

    [Tooltip("The points on the Mech that weapons will be spawned and parented to.")]
    [SerializeField]
    private Transform[] mechaWeaponSockets = new Transform[2];

    [SerializeField]
    private GameObject[] TEMPORARY_WpRefs = new GameObject[2];

    [SerializeField]
    public WeaponPrimary[] spawnedWeapons = new WeaponPrimary[2];

    private GameObject touchedPickup;

    int LastPickup = 0;

    void Awake() { 
    
        Instance = this;
    }

    // Use this for initialization
    void Start() {
    }

    void Update()
    {
        CheckCollisionWithPickup();
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

        // Kill old gun if it exists
        if(mechaWeaponSockets[socketIndex].transform.childCount > 0)
            Destroy(mechaWeaponSockets[socketIndex].transform.GetChild(0).gameObject);

        GameObject wp = (GameObject)Instantiate(weaponPrefab, mechaWeaponSockets[socketIndex]);

        wp.GetComponent<WeaponMaster>().SetWeaponPointIndex(socketIndex);
        spawnedWeapons[socketIndex] = wp.GetComponent<WeaponPrimary>();
    }

    private void CheckCollisionWithPickup()
    {
        touchedPickup = null;
        foreach (PickupScript pickup in FindObjectsOfType<PickupScript>())
        {
            if(Vector3.Distance(pickup.transform.position, this.transform.position) < 50.0f)
            {
                LastPickup = 1 - LastPickup;
                pickup.OnUsePickup(LastPickup);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == touchedPickup)
            touchedPickup = null;
    }

    public bool touchingPickup()
    {
        return !(touchedPickup == null);
    }

    public bool comparePickup (GameObject pickup)
    {
        if (!touchingPickup())
            return false;
        return pickup == touchedPickup;
    }

    public void InitializeInventory()
    {
        for (int i = 0; i < TEMPORARY_WpRefs.Length; i++)
            AddWeapon(TEMPORARY_WpRefs[i], i);
    }

    public void ClearInventory()
    {
        for (int i = 0; i < mechaWeaponSockets.Length; i++)
        {
            if(mechaWeaponSockets[i].transform.childCount > 0)
                Destroy(mechaWeaponSockets[i].transform.GetChild(0).gameObject);
        }
    }
}
