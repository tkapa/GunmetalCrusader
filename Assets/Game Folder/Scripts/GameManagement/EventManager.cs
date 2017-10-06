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

// Called on Mecha Jump Start.
public class MechaJumpStart : UnityEvent
{
}

// Called on Mecha Jump End.
public class MechaJumpEnd : UnityEvent
{
}

//Called when an enemy dies
public class EnemyDeath : UnityEvent
{
}

//Called when the game is started
public class StartGame : UnityEvent
{
}

//Called when the game ends
public class EndGame : UnityEvent
{
}

//Called when the round begins
public class StartRound : UnityEvent
{
}

//Called when the round ends
public class EndRound : UnityEvent
{
}

//Called when the player dies
public class PlayerDeath : UnityEvent
{
}

// Called when Opening and Closing the Shop
public class ToggleShop : UnityEvent<bool>
{
}

// Called when buying a weapon. Index is determined via the shop
public class NavigateShop : UnityEvent<bool>
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
    public MechaJumpStart OnMechaJumpStart = new MechaJumpStart();
    public MechaJumpEnd OnMechaJumpEnd = new MechaJumpEnd();
    public EnemyDeath OnEnemyDeath = new EnemyDeath();
    public StartGame OnStartGame = new StartGame();
    public EndGame OnEndGame = new EndGame();
    public StartRound OnStartRound = new StartRound();
    public EndRound OnEndRound = new EndRound();
    public PlayerDeath OnPlayerDeath = new PlayerDeath();
    public ToggleShop OnToggleShop = new ToggleShop();
    public NavigateShop OnNavigateShop = new NavigateShop();

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
