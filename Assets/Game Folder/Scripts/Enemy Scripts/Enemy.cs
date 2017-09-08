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

    public Enemy_States state = Enemy_States.EES_Falling;

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
    float attackIntervalCounter;

    //My reference to the player
    [HideInInspector]
    public Player target;

    //This navmesh agent
    [HideInInspector]
    public NavMeshAgent agent;
    private GameManager gameManager;

    [HideInInspector]
    public float destinationUpdateTime = 0.5f, destinationUpdateTimer;

    // Use this for initialization
    public virtual void Start () {
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

        destinationUpdateTimer = destinationUpdateTime;
        attackIntervalCounter = attackInterval;
	}

    //Allow the enemy to die
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            OnDeath();
    }

    //Called when the enemy dies
    public virtual void OnDeath() {
        EventManager.instance.OnEnemyDeath.Invoke();
        Destroy(this.gameObject);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (state == Enemy_States.EES_Falling && collision.gameObject.tag == "ground")
        {
            state = Enemy_States.EES_Tracking;
            agent.enabled = true;
        }
    }

    //Used to set the heatlh, damage and speed values of this unit
    public virtual void SetValues(int _round, int _maxRounds) {
        float percentage = _round / _maxRounds;
        health = enemyHealthCurve.Evaluate(percentage) * maximumEnemyHealth;
        damage = enemyHealthCurve.Evaluate(percentage) * maximumEnemyDamage;
        speed = enemyHealthCurve.Evaluate(percentage) * maximumEnemySpeed;
    }
}
