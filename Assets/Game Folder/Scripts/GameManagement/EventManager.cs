using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Called when initializing the controller sides. Param equates to using controller ref and weapon index.
public class WeaponInit : UnityEvent<GamepadPointer, int>
{
}

// Called on Weapon Fire. Param equates to weapon index.
public class WeaponFire : UnityEvent<int,bool>
{
}

// Called on Weapon Reload. Param equates to weapon index.
public class WeaponSwitch : UnityEvent<int>
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

//Called when the round begins
public class StartRoundTwo : UnityEvent
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

// Called when buying a weapon. Index is determined via the shop
public class UsePickup : UnityEvent<int>
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
    public WeaponInit OnWeaponInit = new WeaponInit();
    public WeaponFire OnWeaponFire = new WeaponFire();
    public WeaponSwitch OnWeaponSwitch = new WeaponSwitch();
    public MechaJumpStart OnMechaJumpStart = new MechaJumpStart();
    public MechaJumpEnd OnMechaJumpEnd = new MechaJumpEnd();
    public EnemyDeath OnEnemyDeath = new EnemyDeath();
    public StartGame OnStartGame = new StartGame();
    public EndGame OnEndGame = new EndGame();
    public StartRound OnStartRound = new StartRound();
    public StartRoundTwo OnStartRoundLate = new StartRoundTwo();
    public EndRound OnEndRound = new EndRound();
    public PlayerDeath OnPlayerDeath = new PlayerDeath();
    public ToggleShop OnToggleShop = new ToggleShop();
    public NavigateShop OnNavigateShop = new NavigateShop();
    public UsePickup OnUsePickup = new UsePickup();

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
