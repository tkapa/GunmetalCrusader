using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrapper : Enemy {

    public Transform firePoint;
    public GameObject projectile;

	// Update is called once per frame
	public override void Update () {
        moveToTransform = target.transform.position;

        base.Update();
	}

    public override void Attack()
    {
        if (attackIntervalCounter < 0)
        {
            GameObject p = Instantiate(projectile, firePoint.position, transform.rotation);
            p.GetComponent<ScrapperProjectile>().parent = this;
            attackIntervalCounter = attackInterval;
        }
        else
            attackIntervalCounter -= Time.deltaTime;

        CheckDistance();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
