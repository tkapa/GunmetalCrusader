using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrapper : Enemy {

    public Transform firePoint;
    public GameObject projectile;

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case Enemy_States.EES_Tracking:
                FindEnemy();
                break;

            case Enemy_States.EES_Attacking:
                Attack();
                break;
        }
	}

    //Sets destination to the player
    void FindEnemy()
    {
        if (destinationUpdateTimer < 0)
        {
            agent.SetDestination(target.transform.position);
            destinationUpdateTimer = destinationUpdateTime;
        }
        else
            destinationUpdateTimer -= Time.deltaTime;

        CheckDistance();
    }

    void Attack()
    {
        if (attackIntervalCounter < 0)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
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
