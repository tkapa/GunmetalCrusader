using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Enemy {

    [HideInInspector]
    public List<GameObject> surroundingSwarmers = new List<GameObject>();

    [HideInInspector]
    float baseDamage, baseHealth;

    public override void Start()
    {
        base.Start();
        baseDamage = damage;
        baseHealth = health;
    }

    public override void Update()
    {
        //Set moveToTransform to the transform of the position I want to move to
        moveToTransform = target.transform.position;
        base.Update();
    }

    public void ShepherdBuff(bool isBuffing)
    {
        if (isBuffing)
        {
            print("Buffed by Shepherd");
            damage = baseDamage + 5;
            health = baseHealth + 5;
        }
        else
        {
            damage = baseDamage;
            health = baseHealth;
        }
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
}
