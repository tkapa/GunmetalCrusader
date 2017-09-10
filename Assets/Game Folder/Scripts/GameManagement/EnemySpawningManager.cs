using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour {

    //Used to maange enemy values and round count
    int roundEnemyCount;
    int spawnedEnemyCount;
    int deadEnemyCount;

    public float spawningInterval = 1.5f;
    float spawningIntervalTimer;

    //The current enemy prefab
    public GameObject swarmerPrefab;

    [HideInInspector]
    public List<GameObject> spawningObjects = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> swarmers = new List<GameObject>();

    bool isSpawning = false;

	// Use this for initialization
	void Start () {
        if (swarmerPrefab == null)
            Debug.LogError("The swarmer prefab is not set!");

        EventManager.instance.OnStartRound.AddListener(()=> {
            isSpawning = true;
            spawnedEnemyCount = 0;
            deadEnemyCount = 0;
            spawningIntervalTimer = spawningInterval;
        });
        EventManager.instance.OnEnemyDeath.AddListener(() => {
            ++deadEnemyCount;

            print("Dead enemies: " + deadEnemyCount + "Kills Needed: " + roundEnemyCount);

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
            SpawnSwarmer();
            ++spawnedEnemyCount;
            spawningIntervalTimer = spawningInterval;
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

    public void SpawnSwarmer()
    {
        GameObject s = Instantiate(swarmerPrefab, spawningObjects[(int)Random.Range(0, spawningObjects.Count)].transform);
        swarmers.Add(s);
    }
}
