using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    //Enumeration of the states that this agent can be in
    public enum Enemy_States
    {
        EES_Falling,
        EES_Tracking,
        EES_Attacking
    }

    Enemy_States state = Enemy_States.EES_Falling;

    //Variables that set the stats for this agent
    [HideInInspector]
    public float health, damage, speed;

    //Distance the enemy must be to attack at
    public float attackingDistance = 3.0f, attackInterval = 1.0f;
    float attackIntervalCounter;

    //My reference to the player
    Player target;

    //This navmesh agent
    private NavMeshAgent agent;

    private float destinationUpdateTime = 0.5f, destinationUpdateTimer;

	// Use this for initialization
	void Start () {
        if (!GetComponent<NavMeshAgent>())
            Debug.LogError("Enemy does not have a NavMeshAgent!");
        else
            agent = GetComponent<NavMeshAgent>();

        agent.enabled = false;

        if (!FindObjectOfType<Player>())
            Debug.LogError("There is no Player script attached to the mech!");
        else
            target = FindObjectOfType<Player>();

        destinationUpdateTimer = destinationUpdateTime;
        attackIntervalCounter = attackInterval;
	}
	
	// Update is called once per frame
	void Update () {

        //Switch the enemy behaviour based on state
        switch (state)
        {
            case Enemy_States.EES_Tracking:
                Track();
                break;

            case Enemy_States.EES_Attacking:
                Attack();
                break;
        }
	}

    //Used to set the enemy path for the next half second
    void UpdateDestination()
    {
        if (destinationUpdateTimer < 0)
        {
            destinationUpdateTimer = destinationUpdateTime;
            agent.SetDestination(target.transform.position);
        }
        else
            destinationUpdateTimer -= Time.deltaTime;
    }

    //Set the state to either tracking or attacking
    void SetTracking()
    {
        state = Enemy_States.EES_Tracking;
        agent.isStopped = false;
    }

    void SetAttacking()
    {
        state = Enemy_States.EES_Attacking;
        agent.isStopped = true;
    }

    //Attack the player
    void Attack()
    {
        if (attackIntervalCounter < 0)
        {
            print(damage);
            attackIntervalCounter = attackInterval;
            target.TakeDamage(damage);
        }
        else
            attackIntervalCounter -= Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) > attackingDistance)
            SetTracking();
    }

    //Track the player
    void Track()
    {
        UpdateDestination();
        if (Vector3.Distance(transform.position, target.transform.position) < attackingDistance)
            SetAttacking();
    }

    //Allow the enemy to die
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            OnDeath();
    }

    //Called when the enemy dies
    public void OnDeath() {
        EventManager.instance.OnEnemyDeath.Invoke();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == Enemy_States.EES_Falling && collision.gameObject.tag == "ground")
        {
            state = Enemy_States.EES_Tracking;
            agent.enabled = true;
        }
    }
}
