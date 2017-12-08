using System.Collections;
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

    //stuff for final wave

    //the spawn points for the final wave (swarmer spawn points)
    public GameObject[] SpawnPoints4;
    //the spawn point of shephards
 //   public GameObject[] ShephardSpawns;

    private bool DoneWave4;

   

   
    //check to see if player has killed all the swarmers we spawned (happens each wave)
    public float SwarmersAlive;
    public bool CheckingSwarmers;

    //gernade launcher spawn
    public GameObject genlocation;
    public GameObject GrenadeLauncherPrefab;

    //the audiosource lines play from
    public AudioSource TutLines;

    //the individual lines
    public AudioClip opening;
    public AudioClip PreAndroidLines;

    public AudioClip Androidsonright;

    public AudioClip ANdroidsOnBothSides;

    public AudioClip LetsGoToGrenadeLauncher;

    public AudioClip HowTojump;

    public AudioClip howToPickupWeapon;

    public AudioClip ShephardSpawned;

    public AudioClip FinalLine;


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
        TutLines.PlayOneShot(opening);
        Invoke("preFirstWave", 10);
    }


    //the voice lines before Wave 1
    void preFirstWave()
    {
        TutLines.PlayOneShot(PreAndroidLines);
        Invoke("EnemySpawnbatch1", 18);
    }
    

	void Update () {
        //are the first wave of swarmers still alive
		if (CheckingSwarmers)
        {

            //finished wave 1
            if (SwarmersAlive <= 0 && !DoneWave1 && !DoneWave2 && !DoneWave3 && !DoneWave4)
            {
                print("Call After Wave 1");
                AfterWave1();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
            //finished wave2
           else if (SwarmersAlive <= 0 && DoneWave1 && !DoneWave2 && !DoneWave3 && !DoneWave4)
            {
                AfterWave2();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
            //finished wave 3
           else if (SwarmersAlive <= 0 && DoneWave1 && DoneWave2  && !DoneWave3  && !DoneWave4)
            {
                AfterWave3();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }

          else  if (SwarmersAlive <= 0 && DoneWave1 == true && DoneWave2 == true && DoneWave3 == true && DoneWave4 == false)
            {
                KilledWave4();
                SwarmersAlive = 0;
                CheckingSwarmers = false;
            }
        }
	}

    //functions below update are designed to happen "In Order"


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
        print("Wave 1 Clear.");
        TutLines.PlayOneShot(Androidsonright);
        Invoke("EnemySpawnbatch2", 5);
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
        Invoke("EnemySpawnbatch3", 5);
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
        TutLines.PlayOneShot(ANdroidsOnBothSides);
        CheckingSwarmers = true;
    }

    void AfterWave3()
    {
        TutLines.PlayOneShot(HowTojump);

        Invoke("JumpSequence", 9);

        Player.p.HasDoneJumpTutorial = true;
        //JumpSequence(); // For now just cut losses and finish the shit
        /*
        if(genlocation)
            Instantiate(GrenadeLauncherPrefab, genlocation.transform.position, Quaternion.identity);
        else
            Instantiate(GrenadeLauncherPrefab, GameObject.FindGameObjectWithTag("WeaponSpawnPoint").transform.position, Quaternion.identity);
        */
        //Invoke("JumpSequence", 5);
    }

    void JumpSequence()
    {
        Debug.Log("Starting Real Game");
        TutLines.PlayOneShot(FinalLine);

        StartRealGame();
    }

    //EXTERNAL CALL REQUIRED, PLAYERGRABBEDJUMPINDICATOR BUT HASNT JUMPED YET

    //EXTERNAL CALL REQUIRED, DID PLAYER JUMP AND LANG WITHIN (X) UNITS OF GRENADE LAUNCHER (CLOSE ENOUGH TO OPEN WEAPON PICKUP OBJETS)
    void PostJump()
    {
        TutLines.PlayOneShot(howToPickupWeapon);        
    }

    //EXTERNAL CALL REQUIRED, DID PLAYER PICKUP GRENADE LAUNCHER!
    void PickedUpGrenadeLauncher()
    {
        //probs wait a bit then call the voice line before wave 4, then start wave 4
        Invoke("EnemySpawnbatch4", 7);
    }

    //spawn the wave with a shephard and some more enemies
    void EnemySpawnbatch4()
    {
        foreach (GameObject U in SpawnPoints4)
        {
            Instantiate(batchSwarmer, U.transform.position, Quaternion.identity);
            SwarmersAlive++;
        }
        //foreach (GameObject Y in ShephardSpawns)
        //{
        //    Instantiate(batchShephard, Y.transform.position, Quaternion.identity);
        //    SwarmersAlive++;
        //}
        //TutLines.PlayOneShot(ShephardSpawned);
        CheckingSwarmers = true;
        
    }


    void KilledWave4()
    {
        TutLines.PlayOneShot(FinalLine);
        DoneWave4 = true;
    }
    void AndroidAttackingTheBigbanana()
    {

    }

    void StartRealGame()
    {

        //turn on stuff like the wave manager and normal audio manager
        foreach (GameObject P in ShitToKill)
        {
            P.SetActive(true);
        }

        EventManager.instance.OnStartGame.Invoke(); // Start the real game

        //destroy all our spawn points for wave 0 to save space
        foreach (GameObject U in SpawnPoints4)
        {
            Destroy(U.gameObject);
        }
        //foreach (GameObject Y in ShephardSpawns)
        //{
        //    Destroy(Y.gameObject);
        //}

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

        Destroy(this);
    }

}
