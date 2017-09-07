using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredObject : WeaponEffect {

    // How much base damage an enemy takes from this shot.
    [Tooltip("How much base damage an enemy takes from this shot.")]
    [SerializeField]
    private float baseDamage = 8.0f;

    // How much base damage an enemy takes from this shot.
    [Tooltip("Which team does this shot belong to? 0 = Player, 1 = Enemies")]
    [SerializeField]
    private int teamIndex = 0;

    // Does this weapon cause Splash Damage?
    [Tooltip("Does this weapon cause Splash Damage?")]
    [SerializeField]
    private bool splashDamage = false;

    // How far away from the epicentre splash effects will take place.
    [Tooltip("How far away from the epicentre splash effects will take place.")]
    [SerializeField]
    private float innerSplashRadius = 4.0f, outerSplashRadius = 6.0f;

    // How much baseDamage is multiplied by if the enemy is only hit by the splash damage from this weapon.
    [Tooltip("How much baseDamage is multiplied by if the enemy is only hit by the splash damage from this weapon.")]
    [SerializeField]
    private float innerSplashModifier = 0.5f, outerSplashModifier = 0.1f;

    /*
     * Comment block for unimplemented functionality. Effectively a TODO: list
     *
    // How much baseDamage is multiplied by if this strikes a critical area on an enemy.
    [Tooltip("How much baseDamage is multiplied by if this strikes a critical area on an enemy.")]
    [SerializeField]
    private float critModifier = 2.0f;

    // How much baseDamage is multiplied by if the enemy is only hit by the splash damage from this weapon.
    [Tooltip("Does this shot continue through enemies until it hits the terrain?")]
    [SerializeField]
    private bool penetratesEnemies = false;

    // How much baseDamage is multiplied by if the enemy is only hit by the splash damage from this weapon.
    [Tooltip("Does this shot continue through enemies until it hits the terrain?")]
    [SerializeField]
    private bool penetratesTerrain = false;

    // How hard enemies will be knocked back.
    [Tooltip("How hard enemies will be knocked back.")]
    [SerializeField]
    private float impulseStrength = 128.0f;
    */

    // How long this shot survives for before garbage collection.
    [Tooltip("The game object spawned where the bullet impacts.")]
    [SerializeField]
    protected GameObject impactEffect;

    /*
     * Called once per frame.
     */
    protected virtual void Update() { }

    /*
     * Called when we have hit an enemy
     */
    protected virtual void hitEnemy(GameObject e, float damageMod)
    {
        e.GetComponent<Enemy>().TakeDamage(baseDamage * damageMod);
    }

    /*
     * Called when we have hit an enemy
     */
    protected virtual void splashEnemy(GameObject ignoreme, Vector3 loc)
    {
        if (splashDamage)
        {
            foreach (Enemy e in FindObjectsOfType<Enemy>())
            {
                if (e.gameObject == ignoreme)
                    continue;

                float dist = Vector3.Distance(loc, e.gameObject.transform.position);

                if (dist < innerSplashRadius)
                    hitEnemy(e.gameObject, innerSplashModifier);
                else if (dist < outerSplashRadius)
                    hitEnemy(e.gameObject, outerSplashModifier);
                else
                    continue;
            }
        }
    }
}