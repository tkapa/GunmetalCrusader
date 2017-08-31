using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentTools : MonoBehaviour {

    public string killAllEnemiesInput = "k";

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(killAllEnemiesInput))
        {
            KillAllActiveEnemies();
        }
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
}
