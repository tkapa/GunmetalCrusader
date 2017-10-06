using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Enemy {

    [HideInInspector]
    public List<GameObject> surroundingSwarmers = new List<GameObject>();

    [HideInInspector]
    float baseDamage, baseHealth;

    int swarmerGroupSize;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        //Set moveToTransform to the transform of the position I want to move to
        moveToTransform = FindFriends();
        base.Update();
    }

    public void ShepherdBuff(bool isBuffing)
    {
        baseDamage = damage;
        baseHealth = healthComponent.health;

        if (isBuffing)
        {
            print("Buffed by Shepherd");
            damage = baseDamage + 5;
            healthComponent.health = baseHealth + 5;
        }
        else
        {
            damage = baseDamage;
            healthComponent.health = baseHealth;
        }
    }

    //Looks For Friendly Swarmers and sets a point to move towards
    Vector3 FindFriends()
    {
        Vector3 retVec = transform.position;

        if (FindObjectOfType<Shepherd>())
        {
            retVec = FindObjectOfType<Shepherd>().transform.position;
        }
        else if (surroundingSwarmers.Count > 0)
        {
            int i = 0;

            foreach (GameObject s in surroundingSwarmers)
            {
                if (i < swarmerGroupSize)
                    retVec += s.transform.position;
            }

            retVec += target.transform.position;

            retVec /= swarmerGroupSize + 1;
        }
        else
            retVec = target.transform.position;

        return retVec;
    }
}
