using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour {

    //Used to maange enemy values and round count
    int roundEnemyCount;
    int spawnedEnemyCount;
    int deadEnemyCount;
    
    //Enemy value spawning
    float enemyHealth, enemyDamage, enemySpeed;

    public float spawningInterval = 1.5f;
    float spawningIntervalTimer;

    //The current enemy prefab
    public GameObject enemyPrefab;

    [HideInInspector]
    public List<GameObject> spawningObjects = new List<GameObject>();

    bool isSpawning = false;

	// Use this for initialization
	void Start () {
        if (enemyPrefab == null)
            Debug.LogError("The enemy prefab is not set!");

        EventManager.instance.OnStartRound.AddListener(()=> {
            isSpawning = true;
            spawnedEnemyCount = 0;

            spawningIntervalTimer = spawningInterval;
        });
        EventManager.instance.OnEnemyDeath.AddListener(() => {
            ++deadEnemyCount;

            if (deadEnemyCount == roundEnemyCount)
                EventManager.instance.OnEndRound.Invoke();
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (isSpawning)
            Spawn();
	}

    //Used to manage the spawning of enemies
    void Spawn()
    {
        if (spawningIntervalTimer < 0)
        {
            GameObject e = Instantiate(enemyPrefab, spawningObjects[(int)Random.Range(0, spawningObjects.Count)].transform) as GameObject;
            Enemy ec = e.GetComponent<Enemy>();
            ec.health = enemyHealth;
            ec.damage = enemyDamage;
            ec.speed = enemySpeed;

            ++spawnedEnemyCount;
            spawningIntervalTimer = spawningInterval;
        }
        else
            spawningIntervalTimer -= Time.deltaTime;

        if (spawnedEnemyCount == roundEnemyCount)
            isSpawning = false;
    }

    //Called by the game manager when the round starts to set values
    public void EnemyRoundValues(int _enemyRoundCount, float _health, float _damage, float _speed)
    {
        roundEnemyCount = _enemyRoundCount;
        enemyHealth = _health;
        enemyDamage = _damage;
        enemySpeed = _speed;
    }
}
