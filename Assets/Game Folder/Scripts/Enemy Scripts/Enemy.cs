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

    [HideInInspector]
    public Enemy_States state;

    [HideInInspector]
    public int currentRound = 0;

    //Variables that set the stats for this agent
    [HideInInspector]
    public float health, damage, speed;

    //Manage enemy health count
    public AnimationCurve enemyHealthCurve;
    public float maximumEnemyHealth;

    //Manage enemy damage count
    public AnimationCurve enemyDamageCurve;
    public float maximumEnemyDamage;

    //Manage enemy speed count
    public AnimationCurve enemySpeedCurve;
    public float maximumEnemySpeed;

    //Distance the enemy must be to attack at
    public float attackingDistance = 3.0f, attackInterval = 1.0f;

    [HideInInspector]
    public float attackIntervalCounter;

    [HideInInspector]
    public float roundPercentage;

    //My reference to the player
    [HideInInspector]
    public Player target;

    //This navmesh agent
    [HideInInspector]
    public NavMeshAgent agent;
    private GameManager gameManager;

    [HideInInspector]
    public Vector3 moveToTransform;

    [HideInInspector]
    public float destinationUpdateTime = 0.5f, destinationUpdateTimer;

    [HideInInspector]
    public EnemyHealthComponent healthComponent;

    // Use this for initialization
    public virtual void Start () {
        state = Enemy_States.EES_Falling;

        if (!FindObjectOfType<GameManager>())
            Debug.LogError("There is no GameManager on the scene");
        else
            gameManager = FindObjectOfType<GameManager>();

        if (!GetComponent<NavMeshAgent>())
            Debug.LogError("Enemy does not have a NavMeshAgent!");
        else
            agent = GetComponent<NavMeshAgent>();

        agent.enabled = false;

        if (!FindObjectOfType<Player>())
            Debug.LogError("There is no Player script attached to the mech!");
        else
            target = FindObjectOfType<Player>();

        SetValues(gameManager.currentRound, gameManager.maximumNumberOfRounds);
	}

    public virtual void Update()
    {
        print(state);

        switch (state)
        {
            case Enemy_States.EES_Tracking:
                SetDestination();
                break;

            case Enemy_States.EES_Attacking:
                Attack();
                break;
        }

        CheckDistance();
    }

    //Changes how the enemy attacks
    public virtual void Attack()
    {
        if (attackIntervalCounter < 0)
        {
            print(damage);
            target.TakeDamage(damage);
            attackIntervalCounter = attackInterval;
        }
        else
            attackIntervalCounter -= Time.deltaTime;
    }

    //Sets the next destination for this enemy
    public void SetDestination()
    {
        if (destinationUpdateTimer < 0)
        {
            agent.SetDestination(moveToTransform);
            destinationUpdateTimer = destinationUpdateTime;
        }
        else
            destinationUpdateTimer -= Time.deltaTime;
    }

    //Allow the enemy to die
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            OnDeath();
    }

    //Checks the dstance from this unit to the player
    public void CheckDistance()
    {


        if (Vector3.Distance(transform.position, target.transform.position) < attackingDistance)
        {
            if (state != Enemy_States.EES_Attacking && state != Enemy_States.EES_Falling)
                state = Enemy_States.EES_Attacking;
                           
        }
        else
        {
            if (state != Enemy_States.EES_Tracking && state != Enemy_States.EES_Falling)
                state = Enemy_States.EES_Tracking;                           
        }

    }

    //Called when the enemy dies
    public virtual void OnDeath() {
        EventManager.instance.OnEnemyDeath.Invoke();
        Destroy(this.gameObject);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
		print ("Collision");
        if (state == Enemy_States.EES_Falling && collision.gameObject.tag == "Floor")
        {
			print ("pathing");
            state = Enemy_States.EES_Tracking;
            agent.enabled = true;
        }
    }

    //Used to set the heatlh, damage and speed values of this unit
    public void SetValues(int _round, int _maxRounds) {
        roundPercentage = (float)_round / _maxRounds;
                
		healthComponent.health = enemyHealthCurve.Evaluate(roundPercentage) * maximumEnemyHealth;
        damage = enemyHealthCurve.Evaluate(roundPercentage) * maximumEnemyDamage;
        
		agent.speed = enemyHealthCurve.Evaluate(roundPercentage) * maximumEnemySpeed;
        agent.stoppingDistance = attackingDistance;

		destinationUpdateTimer = destinationUpdateTime;
		attackIntervalCounter = attackInterval;
    }
}
