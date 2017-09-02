using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

//Called when an enemy dies
public class EnemyDeath : UnityEvent
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
    public WeaponEquip OnWeaponEquip = new WeaponEquip();
    public WeaponFire OnWeaponFire = new WeaponFire();
    public WeaponReload OnWeaponReload = new WeaponReload();
    public EnemyDeath OnEnemyDeath = new EnemyDeath();

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
