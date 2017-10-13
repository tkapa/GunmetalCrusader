using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopController : MonoBehaviour {

    // The button that opens the shop.
    [Tooltip("The button that opens the shop.")]
    [SerializeField]
    private GameObject OpenShopBtn;

    // The button that opens the shop.
    [Tooltip("GameObject holding all of the shop elements.")]
    [SerializeField]
    private GameObject ShopComp;

    // Is the shop currently open
    private bool isShopOpen = false;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
        // Bind Events
        EventManager.instance.OnToggleShop.AddListener((i) =>
        {
            ToggleShop(i);
        });
    }

    void ToggleShop(bool isOpen)
    {
        isShopOpen = isOpen;
        OpenShopBtn.SetActive(!isOpen);
        ShopComp.SetActive(isOpen);
    }

    void OnDisable()
    {
        ToggleShop(false);
    }

    public bool CheckIfShopOpen()
    {
        return isShopOpen;
    }
}
