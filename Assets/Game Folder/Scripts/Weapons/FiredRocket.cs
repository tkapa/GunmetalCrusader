using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredRocket : FiredProjectile {

    // How far does this projectile move every second?
    [Tooltip("How fast is the rocket at top speed?")]
    [SerializeField]
    private float maxMoveSpeed = 10.0f;

    // How far does this projectile move every second?
    [Tooltip("How fast does the projectile accelerate")]
    [SerializeField]
    private float accelerationSpeed = 1.0f;

    /*
	 * Called once per frame. Override to accelerate the projectile over time.
	 */
    protected override void Update()
    {
        base.Update();

        moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, accelerationSpeed * Time.deltaTime);
    }
}
