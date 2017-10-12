using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackShopTestScript : MonoBehaviour {

    public Animator MyAnim;
    //the triggers for the animator
    public string OpenShopTrigger;

    public string CloseShopTrigger;

    public string LeftBuyTrigger;

    public string RightBuyTrigger;

    public string LeftAmmoTrigger;

    public string RightAmmoTrigger;
    //the shop big screen 
    public GameObject MasterWeaponScreen;
    public GameObject CurrentWeaponOnScreen;


    //the sprites and prefabs we need to change the big screen too for weapon changes
    public Sprite SMG;
    public GameObject _SMG;

    public Sprite LMG;
    public GameObject _LMG;

    public Sprite Gren;
    public GameObject _Gren;

    public Sprite Flak;
    public GameObject _Flak;

    public Sprite Rail;
    public GameObject _Rail;


    //the raycast pointer
    public JackShopRaycastScript RayCastOrigin;


    // Use this for initialization
    void Start () {
        // Bind Events
        EventManager.instance.OnToggleShop.AddListener((i) =>
        {
            if (i)
                CalledShopOpen();
            else
                CalledShopClosed();
        });

        // Bind Events
        EventManager.instance.OnNavigateShop.AddListener((i) =>
        {
            if (i)
                CalledShopOpen();
            else
                CalledShopClosed();
        });
    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CalledBuyNewLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CalledBuyNewRight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CalledBuyAmmoLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CalledBuyAmmoRight();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            CalledShopOpen();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CalledShopClosed();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedSMG();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedLMG();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedGren();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectedRail();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectedFlak();
        }
    }

    public void SelectedSMG()
    {
        MasterWeaponScreen.GetComponent<SpriteRenderer>().sprite = SMG;
        CurrentWeaponOnScreen = _SMG;
    }

    public void SelectedLMG()
    {
        MasterWeaponScreen.GetComponent<SpriteRenderer>().sprite = LMG;
        CurrentWeaponOnScreen = _LMG;
    }

    public void SelectedGren()
    {
        MasterWeaponScreen.GetComponent<SpriteRenderer>().sprite = Gren;
        CurrentWeaponOnScreen = _Gren;
    }

    public void SelectedRail()
    {
        MasterWeaponScreen.GetComponent<SpriteRenderer>().sprite = Rail;
        CurrentWeaponOnScreen = _Rail;
    }

    public void SelectedFlak()
    {
        MasterWeaponScreen.GetComponent<SpriteRenderer>().sprite = Flak;
        CurrentWeaponOnScreen = _Flak;
    }

    public void CalledShopOpen()
    {
        MyAnim.SetTrigger(OpenShopTrigger);
        Debug.Log("Should have opened shop");
        RayCastOrigin.ShopOpen = true;
    }

    public void CalledShopClosed()
    {
        MyAnim.SetTrigger(CloseShopTrigger);
    }

    public void CalledBuyNewLeft()
    {
        MyAnim.SetTrigger(LeftBuyTrigger);
    }

    public void CalledBuyNewRight()
    {
        MyAnim.SetTrigger(RightBuyTrigger);
    }

    public void CalledBuyAmmoLeft()
    {
        MyAnim.SetTrigger(LeftAmmoTrigger);
    }

    public void CalledBuyAmmoRight()
    {
        MyAnim.SetTrigger(RightAmmoTrigger);
    }
}
