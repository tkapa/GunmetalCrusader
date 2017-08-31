using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Temporary example event
public class DummyEvent : UnityEvent<bool>
{
}

// Called on Weapon Equip. Param equates to weapon index.
public class WeaponEquip : UnityEvent<int>
{
}

// Called on Weapon Fire. Param equates to weapon index.
public class WeaponFire : UnityEvent<int,bool>
{
}

// Called on Weapon Reload. Param equates to weapon index.
public class WeaponReload : UnityEvent<int>
{
}

public class EventManager : MonoBehaviour
{
    // Initialize as Singleton
    private static EventManager inst;
    public static EventManager instance
    {
        get
        {
            if (inst == null)
            {
                var newEventManager = new GameObject("EventManager");
                inst = newEventManager.AddComponent<EventManager>();
            }

            return inst;
        } 
    }

    // Bind Events
    public DummyEvent OnDummyEvent = new DummyEvent();
    public WeaponEquip OnWeaponEquip = new WeaponEquip();
    public WeaponFire OnWeaponFire = new WeaponFire();
    public WeaponReload OnWeaponReload = new WeaponReload();

    // Self deletion on duplicate creation
    private void Awake()
    {
        if (inst != null)
        {
            DestroyImmediate(this);
            return;
        }

        inst = this;
    }
}
