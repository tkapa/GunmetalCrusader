using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopActivator : MonoBehaviour {

    // Reference to our shop GUI
    private GameObject ShopObject;

    // Is the playewr currently colliding with this trigger.
    private bool PlayerColliding = false;

    void Start()
    {
        ShopObject = GameObject.FindGameObjectWithTag("WeaponShop");
    }

    void Update()
    {
        ShopObject.SetActive(PlayerColliding);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PlayerColliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            PlayerColliding = false;
    }

    public bool GetPlayerColliding()
    {
        return PlayerColliding;
    }
}