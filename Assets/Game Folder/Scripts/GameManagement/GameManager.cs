using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Manage the rounds
    public int maximumNumberOfRounds;

    [HideInInspector]
    public int currentRound;

    //Manage the number of enemies for the round
    public AnimationCurve enemyNumberCurve;
    public int maximumEnemyNumber;

    public EnemySpawningManager spawnManager;

	// Use this for initialization
	void Start () {
        /*if (GetComponent<EnemySpawningManager>())
            Debug.LogError("There is no EnemySpawningManager on the scene!");
        else
            spawnManager = GetComponent<EnemySpawningManager>();*/

        EventManager.instance.OnStartRound.AddListener(UpdateStartRoundVariables);
        EventManager.instance.OnEndRound.AddListener(()=> {
            if (currentRound == maximumNumberOfRounds)
                print("You won the game!!");
        });

        //Do something when the player dies
        EventManager.instance.OnPlayerDeath.AddListener(()=>
        {
            print("Player Dead");
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Used to tell the enemy spawner what variables to use for spawning
    void UpdateStartRoundVariables()
    {
        ++currentRound;
        print(currentRound);


        float roundPercentage = (float)currentRound / maximumNumberOfRounds;
        int enemyCount = (int)(enemyNumberCurve.Evaluate(roundPercentage) * maximumEnemyNumber);
        spawnManager.EnemyRoundValues(enemyCount);
    }
}
