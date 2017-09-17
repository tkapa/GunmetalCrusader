using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Enemy {

    [HideInInspector]
    public List<GameObject> surroundingSwarmers = new List<GameObject>();

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.LookAt(agent.velocity);

        switch (state)
        {
            case Enemy_States.EES_Tracking:
                print("tracking");
                SetDestination();
                break;

            case Enemy_States.EES_Attacking:
                print("attacking");
                Attack();
                break;
        }        
    }

    //Sets the next destination for this enemy
    void SetDestination()
    {
        if (destinationUpdateTimer < 0)
        {
            agent.SetDestination((FindFriends() +FindPlayer())/2);
            destinationUpdateTimer = destinationUpdateTime;
        }
        else
            destinationUpdateTimer -= Time.deltaTime;

        CheckDistance();
    }

    //Looks For Friendly Swarmers and sets a point to move towards
    Vector3 FindFriends()
    {
        Vector3 retVec = transform.position;

        if(surroundingSwarmers.Count > 0)
        {
            foreach (GameObject s in surroundingSwarmers)
            {
                retVec += s.transform.position;
            }

            retVec /= surroundingSwarmers.Count;
        }

        return retVec * 0.2f;
    }

    //Finds the player
    Vector3 FindPlayer()
    {
        return target.transform.position;
    }   
    
    void Attack()
    {
        if (attackIntervalCounter < 0)
        {
            print(damage);
            target.TakeDamage(damage);
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
