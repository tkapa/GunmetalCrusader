using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrimary : WeaponMaster
{

    // Holds the amount of time (in seconds) until the weapon can be fired again after each shot.
    [Tooltip("Holds the amount of time (in seconds) until the weapon can be fired again after each shot.")]
    [SerializeField]
    private float fireInterval = 1.0f;

    // Holds the amount of time (in seconds) until the weapon can be fired again.
    private float fireTimer = 0.0f;

    // Should the weapon automatically fire as long as the trigger is held?
    [Tooltip("Should the weapon automatically fire as long as the trigger is held?")]
    [SerializeField]
    private bool autoRefire = false;

    // The amount of time (in seconds) that a reload takes.
    [Tooltip("The amount of time (in seconds) that a reload takes.")]
    [SerializeField]
    private float maxReloadTime = 2.0f;

    // The amount of time (in seconds) left in the current reload.
    private float reloadTimer = 0.0f;

    // The amount of x/y spread that can be imposed per shot.
    [Tooltip("The max amount of x/y spread that can be imposed per shot and the end of the volley")]
    [SerializeField]
    private Vector2 maxSpreadAmount = new Vector2(16.0f, 16.0f);

    // The percentage of max of x/y spread that can be imposed on the first shot.
    [Tooltip("The percentage of max of x/y spread that can be imposed on the first shot.")]
    [SerializeField]
    private float minSpreadPercentage = 0.1f;

    // The number of shots it takes before the weapon is as inaccurate as it can be.
    [Tooltip("The number of shots it takes before the weapon is as inaccurate as it can be.")]
    [SerializeField]
    private int maxVolleyLength = 10;

    // The number of shots fired during the current volley.
    private int volleyLength = 0;

    // The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.
    [Tooltip("The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.")]
    [SerializeField]
    private float recoilTime = 1.0f;

    // The amount of time (in seconds) remaining until the volley resets.
    private float recoilTimer = 0.0f;

    // The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.
    [Tooltip("Used exclusively for shotguns/flak guns. Determines how many times to run the projectile spawn action.")]
    [SerializeField]
    private int projectilesPerShot = 1;

    // Array of Locations and Rotations that bullets will be spawned from.
    [Tooltip("Array of Locations and Rotations that bullets will be spawned from.")]
    [SerializeField]
    private GameObject[] muzzles;

    // The current muzzle we should fire a shot from.
    private int currentMuzzle = 0;

    // The prefab that gets instantiated when the weapon is fired.
    [Tooltip("The prefab that gets instantiated when the weapon is fired.")]
    [SerializeField]
    private GameObject firedObject;

    // Holds a reference to the Legacy Animation system that drives the weapon.
    [Tooltip("Holds a reference to the Legacy Animation system that drives the weapon.")]
    [SerializeField]
    private Animation weaponAnimation;

    // The firing animation clip.
    [Tooltip("The firing animation clip.")]
    [SerializeField]
    private AnimationClip fireAnim;

    // The firing animation clip.
    [Tooltip("The speed at which the firing animation plays")]
    [SerializeField]
    private float fireAnimSpeed = 0.0f;

    // The reloading animation clip.
    [Tooltip("The reloading animation clip.")]
    [SerializeField]
    private AnimationClip reloadAnim;

    // The firing animation clip.
    [Tooltip("The speed at which the reloading animation plays")]
    [SerializeField]
    private float reloadAnimSpeed = 0.0f;

    // The game object representing the muzzle flash. Should be a particle system on a kill timer.
    [Tooltip("The game object representing the muzzle flash. Should be a particle system on a kill timer.")]
    [SerializeField]
    private GameObject muzzleFlashObject;

    // The game object representing the muzzle flash. Should be a particle system on a kill timer.
    [Tooltip("The game object representing the muzzle flash. Should be a particle system on a kill timer.")]
    [SerializeField]
    private GameObject laserSightObj;

    // The game object representing the muzzle flash. Should be a particle system on a kill timer.
    [Tooltip("The game object representing the muzzle flash. Should be a particle system on a kill timer.")]
    [SerializeField]
    private GameObject lasPoint;

    // How much heat is added every shot.
    [Tooltip("How much heat is added every shot.")]
    [SerializeField]
    private float HeatAccrueRate = 5.0f;

    // How much heat is dispersed pert second.
    [Tooltip("How much heat is dispersed pert second.")]
    [SerializeField]
    private float HeatDisperseRate = 75.0f;
    
    private float currentHeatValue = 0.0f;

    // The game object representing the muzzle flash. Should be a particle system on a kill timer.
    [Tooltip("How long between stoping firing and cooldown.")]
    [SerializeField]
    private float HeatRetensionTime = 1.5f;

    private float HeatRetensionTimer = 0.0f;

    private bool hasOverheated = false;

    private GameObject spawnedLaserSightObj;

    // Game object that spawns the vfx for when a weapon has overheated.
    [Tooltip("Game object that spawns the vfx for when a weapon has overheated.")]
    [SerializeField]
    private GameObject OverheatVFX;

    /*
     * Called on instance create
     */
    protected override void Start()
    {
        Debug.Log("called primary start");
        // Call Superclass function
        base.Start();

        spawnedLaserSightObj = Instantiate(laserSightObj, lasPoint.transform);

        // Bind Animations
        weaponAnimation.AddClip(fireAnim, "Fire");
        weaponAnimation["Fire"].speed = fireAnimSpeed;
        weaponAnimation.AddClip(reloadAnim, "Reload");
        weaponAnimation["Reload"].speed = reloadAnimSpeed;
    }

    /*
	 * Called once per frame.
	 */
    protected override void Update()
    {
        // Call Superclass function
        base.Update();

        // Call CheckFire to...check the fire.
        CheckFire();

        // Decrement the fireTimer
        fireTimer -= Time.deltaTime;

        // Decrement the reloadTimer
        reloadTimer -= Time.deltaTime;

        // Decrement the recoilTimer
        resetVolley();

        HeatRetensionTimer += Time.deltaTime;
        if(HeatRetensionTimer > HeatRetensionTime)
        {
            currentHeatValue -= HeatDisperseRate * Time.deltaTime;
            if (currentHeatValue < 0) { currentHeatValue = 0; hasOverheated = false; }
        }
    }

    protected override void UpdateWeaponAim()
    {
        base.UpdateWeaponAim();

        if (hasOverheated)
        {
            spawnedLaserSightObj.SetActive(false);
            return;
        }

        spawnedLaserSightObj.SetActive(true);
        spawnedLaserSightObj.GetComponent<LaserSightMngr>().target = vrCont.GetHitLocation();
    }

    /*
     * Called once a frame to see if we are attempting to fire, and doing so if we can.
     */
    void CheckFire()
    {
        if (isFiring && hasOverheated == false)
        {
            if(fireTimer <= 0.0f)
                DoFire();
        }
        else
        {
            isFiring = false;
        }
    }

    /*
     * Calculates the amount of spread
     */
    Vector2 CalcSpreadAmount()
    {

        float multiplier = minSpreadPercentage;
        if (volleyLength > 0)
        {
            multiplier = Mathf.Lerp(minSpreadPercentage, 1.0f, (float)volleyLength/ (float)maxVolleyLength);
        }
        multiplier = Mathf.Clamp(multiplier, 0, 1);

        spawnedLaserSightObj.GetComponent<LaserSightMngr>().angle = multiplier * ((maxSpreadAmount.x + maxSpreadAmount.y) / 2);

        return new Vector2(Random.Range(-maxSpreadAmount.x, maxSpreadAmount.x) * multiplier, Random.Range(-maxSpreadAmount.y, maxSpreadAmount.y) * multiplier);
    }

    /*
	 * Called from CheckFire when we can and are firing the weapon.
	 */
    void DoFire()
    {
        // Spawn our projectiles
        for (int i = 0; i < projectilesPerShot; i++) // For Shotguns, fire more than one
        {
            Instantiate(firedObject, muzzles[currentMuzzle].transform.position, Quaternion.Euler(muzzles[currentMuzzle].transform.eulerAngles + new Vector3(CalcSpreadAmount().x, CalcSpreadAmount().y, 0)));//muzzles[currentMuzzle].transform.rotation);
                                                                                                                                                                                                             // Play a particle effect.
            Instantiate(muzzleFlashObject, muzzles[currentMuzzle].transform);//muzzles[currentMuzzle].transform.rotation);                     
        }
        
        // Post shot setup: Increment muzzle, set timers, decrease ammo etc.
        currentMuzzle = nextMuzzle();
        fireTimer = fireInterval;
        isFiring = autoRefire;
        incrementVolley();

        currentHeatValue += HeatAccrueRate;
        HeatRetensionTimer = 0.0f;
        if (currentHeatValue > 100.0f)
        {
            currentHeatValue = 100.0f;
            hasOverheated = true;
            OnFireInput(false);

            weaponAnimation.Play("Reload");

            Instantiate(OverheatVFX, muzzles[currentMuzzle].transform);
        }
        else {
            // Play anim
            weaponAnimation.Play("Fire");
        }
    }

    /*
     * Called after firing to increment the muzzle index.
     */
    int nextMuzzle()
    {
        int newMuzz = currentMuzzle + 1;
        // Overflow the muzzle index
        if (newMuzz > muzzles.Length - 1)
            newMuzz = 0;

        return newMuzz;
    }

    /*
     * Resets the muzzle index to zero.
     */
    void resetMuzzle()
    {
        currentMuzzle = 0;
    }

    /*
     * Adds to our volley length and resets the recoil timer
     */
    void incrementVolley()
    {
        volleyLength = Mathf.Clamp(volleyLength + 1, 0, maxVolleyLength);
        recoilTimer = recoilTime;
    }

    /*
     * Reset our volley after the cool down
     */
    void resetVolley()
    {
        recoilTimer -= Time.deltaTime;
        if (recoilTimer <= 0.0f)
        {
            volleyLength = 0;
            spawnedLaserSightObj.GetComponent<LaserSightMngr>().angle = minSpreadPercentage * ((maxSpreadAmount.x + maxSpreadAmount.y) / 2);
        }
    }
}
