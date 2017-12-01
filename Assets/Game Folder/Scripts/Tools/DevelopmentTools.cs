using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevelopmentTools : MonoBehaviour {

    public string killEnemiesKey = "k", resetLevelKey = "r";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(killEnemiesKey))
            KillEnemies();
        if (Input.GetKeyDown(resetLevelKey))
            ResetLevel();
    }

    void KillEnemies()
    {
		if (FindObjectOfType<EnemyHealthComponent>()) {
			foreach (EnemyHealthComponent h in FindObjectsOfType<EnemyHealthComponent>()) {
				h.OnDeath ();
			}
		}
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
