using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentTools : MonoBehaviour {

    //Keys used for each commend
    public string killAllEnemiesInput = "k";
    public string resetGameInput = "r";

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(killAllEnemiesInput))        
            KillAllActiveEnemies();

        if (Input.GetKeyDown(resetGameInput))
            ResetGame();
    }

    //Kills All active enemies on the scene
    void KillAllActiveEnemies()
    {
        if (FindObjectOfType<Enemy>())
        {
            foreach(Enemy e in FindObjectsOfType<Enemy>())
            {
                e.OnDeath();
            }
        }
    }

    //Resets the game back to the main mmenu
    void ResetGame()
    {
        Application.LoadLevel(0);
    }
}
