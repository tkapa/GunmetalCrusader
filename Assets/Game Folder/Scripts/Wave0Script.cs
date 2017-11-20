﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave0Script : MonoBehaviour {
    public static Wave0Script Instance;

    //the enemy prefabs
    public GameObject batchSwarmer;
    public GameObject batchShephard;


    //the first batch of spawn points
    public GameObject[] SpawnPoints1;
    private bool DoneWave1;


    //the second batch of spawn points
    public GameObject[] SpawnPoints2;
    private bool DoneWave2;


    //stuff for wave 3
    private bool DoneWave3;

    //the spawn points for the final wave (swarmer spawn points)
    public GameObject[] SpawnPoints4;
    //the spawn point of shephards
    public GameObject[] ShephardSpawns;

   
    //check to see if player has killed all the swarmers we spawned (happens each wave)
    public float SwarmersAlive;
    public bool CheckingSwarmers;


    //the audiosource lines play from
    public AudioSource TutLines;


    //the shit we need to disable at the start, and enable at the end of wave 0
    public GameObject[] ShitToKill;


    void Start ()
    {
        Instance = this;
        StartingStuff();

    }
	//this function is to do with doing things like disabling the wavemanager and so on. 
    void StartingStuff()
    {
        foreach(GameObject P in ShitToKill)
        {
            P.SetActive(false);
        }
    }

	void Update () {
        //are the first wave of swarmers still alive
		if (CheckingSwarmers)
        {
            //okay if there are none left alive, which wave did we just finish, and what do we do now


            //finished wave 1
            if (SwarmersAlive <= 0 && DoneWave1 == false && DoneWave2 == false && DoneWave3 == false)
            {
                AfterWave1();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
            //finished wave2
            if (SwarmersAlive <= 0 && DoneWave1 == true && DoneWave2 == false && DoneWave3 == false)
            {
                AfterWave2();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
            //finished wave 3
            if (SwarmersAlive <= 0 && DoneWave1 == true && DoneWave2 == true && DoneWave3 == false)
            {
                AfterWave3();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
        }
	}

    //functions below update are designed to happen "In Order"



    //the voice lines before Wave 1
    void Opening()
    {
        
    }
    

    //spawn the first wave of enemies
    void EnemySpawnbatch1()
    {
        foreach(GameObject I in SpawnPoints1)
        {
            Instantiate(batchSwarmer, I.transform.position, Quaternion.identity);
            SwarmersAlive++;

        }
        CheckingSwarmers = true;
    }


    //voice lines between first and second batch
    void AfterWave1()
    {

        DoneWave1 = true;
    }


    //spawn the second batch
    void EnemySpawnbatch2()
    {
        foreach (GameObject O in SpawnPoints2)
        {
            Instantiate(batchSwarmer, O.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }
        CheckingSwarmers = true;
    }

    //voice lines between second and third batch
    void AfterWave2()
    {
        DoneWave2 = true;
    }


    //spawn both sides at once!
    void EnemySpawnbatch3()
    {
        foreach (GameObject O in SpawnPoints2 )
        {
            Instantiate(batchSwarmer, O.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }

        foreach (GameObject I in SpawnPoints1)
        {
            Instantiate(batchSwarmer, I.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }
        CheckingSwarmers = true;
    }

    void AfterWave3()
    {

        //probs play a voice line then do the jump stuff
        DoneWave3 = true;
    }

    void JumpSequence()
    {

    }
    //EXTERNAL CALL REQUIRED, DID PLAYER JUMP AND LANG WITHIN (X) UNITS OF GRENADE LAUNCHER
    void PostJump()
    {

    }
    //EXTERNAL CALL REQUIRED, DID PLAYER PICKUP GRENADE LAUNCHER!
    void PickedUpGrenadeLauncher()
    {
        //probs wait a bit then call the voice line before wave 4, then start wave 4
    }

    //spawn the wave with a shephard and some more enemies
    void EnemySpawnbatch4()
    {
        foreach (GameObject U in SpawnPoints4)
        {
            Instantiate(batchSwarmer, U.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }
        foreach (GameObject Y in ShephardSpawns)
        {
            Instantiate(batchShephard, Y.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }
        CheckingSwarmers = true;
        
    }


    void StartRealGame()
    {

        //turn on stuff like the wave manager and normal audio manager
        foreach (GameObject P in ShitToKill)
        {
            P.SetActive(true);
        }


        //destroy all our spawn points for wave 0 to save space
        foreach (GameObject U in SpawnPoints4)
        {
            Destroy(U.gameObject);
        }
        foreach (GameObject Y in ShephardSpawns)
        {
            Destroy(Y.gameObject);
        }

        foreach (GameObject O in SpawnPoints2)
        {
            Destroy(O.gameObject);
        }

        foreach (GameObject I in SpawnPoints1)
        {
            Destroy(I.gameObject);
        }

        //hopefully this will make this component null, so enemies wont try to interact with it when they die
        this.enabled = false;
    }

}
