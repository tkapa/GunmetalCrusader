using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour {

    GameManager gameManager;

    //Used to maange enemy values and round count
    int roundEnemyCount,
        spawnedEnemyCount,
        deadEnemyCount,
        aliveEnemyCount;

    public int maximumAliveEnemyCount;

    public float spawningInterval = 1.5f;
    float spawningIntervalTimer;

    float roundPercentage;

    [Tooltip("The percentage chance that an android has of spawning each round")]
    public AnimationCurve shepherdSpawnRate, 
        glitchSpawnRate,
        scrapSpawnRate;

    //The current enemy prefab
    public GameObject swarmerPrefab, 
        shepherdPrefab, 
        glitchPrefab,
        scrapperPrefab;

    public GameObject leftSpawnParent, rightSpawnParent, otherSpawnParent;

    [HideInInspector]
    public List<GameObject> spawningObjects = new List<GameObject>();

    HeadsUpInfo hud;

    bool isSpawning = false;

	// Use this for initialization
	void Start () {
        if (swarmerPrefab == null)
            Debug.LogError("The swarmer prefab is not set!");

        if (rightSpawnParent)
            rightSpawnParent.SetActive(false);
        if (otherSpawnParent)
            otherSpawnParent.SetActive(false);

        gameManager = FindObjectOfType<GameManager>();

        EventManager.instance.OnStartRoundLate.AddListener(()=> {
            
            isSpawning = true;
            spawnedEnemyCount = 0;
            deadEnemyCount = 0;
            spawningIntervalTimer = spawningInterval;
            roundPercentage = (float)gameManager.currentRound / gameManager.maximumNumberOfRounds;
        });
        EventManager.instance.OnEnemyDeath.AddListener(() => {
            ++deadEnemyCount;
            --aliveEnemyCount;

            Debug.Log(deadEnemyCount + "/" + roundEnemyCount);

            if (hud)
            {
                hud.eliminations = deadEnemyCount;
                hud.enemyCount = aliveEnemyCount;
            }

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
            SpawnEnemy();
        }
        else
            spawningIntervalTimer -= Time.deltaTime;

        if (spawnedEnemyCount == roundEnemyCount)
            isSpawning = false;
    }

    //Called by the game manager when the round starts to set values
    public void EnemyRoundValues(int _enemyRoundCount)
    {
        roundEnemyCount = _enemyRoundCount;
    }

    public void SpawnEnemy()
    {
        print("Attempting Spawn");
        spawningIntervalTimer = spawningInterval;

        if (aliveEnemyCount >= maximumAliveEnemyCount)
            return;

        ++spawnedEnemyCount;
        ++aliveEnemyCount;

        float chance = Random.Range(0, 100)/100.0f;
      
        float shepSpawnChance = shepherdSpawnRate.Evaluate(roundPercentage);
        float scrapSpawnChance = scrapSpawnRate.Evaluate(roundPercentage);

        if (chance < shepSpawnChance)
            SpawnShepherd(spawningObjects[Random.Range(0, spawningObjects.Count)].transform);
        else
            SpawnSwarmer(spawningObjects[Random.Range(0, spawningObjects.Count)].transform);
    }

    public void SpawnSwarmer(Transform point)
    {
        if (aliveEnemyCount >= maximumAliveEnemyCount)
            return;

        ++spawnedEnemyCount;
        ++aliveEnemyCount;

        Instantiate(swarmerPrefab, point);
    }

    public void SpawnShepherd(Transform point)
    {
        if (aliveEnemyCount >= maximumAliveEnemyCount)
            return;

        ++spawnedEnemyCount;
        ++aliveEnemyCount;

        Instantiate(shepherdPrefab, point);
    }

    public void SpawnGlitch(Transform point)
    {
        if (aliveEnemyCount >= maximumAliveEnemyCount)
            return;

        ++spawnedEnemyCount;
        ++aliveEnemyCount;

        Instantiate(glitchPrefab, point);
    }

    public void SpawnScrapper(Transform point)
    {
        if (aliveEnemyCount >= maximumAliveEnemyCount)
            return;

        ++spawnedEnemyCount;
        ++aliveEnemyCount;

        Instantiate(glitchPrefab, point);
    }
}
