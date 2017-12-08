using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float regenRate = 25.0f;
    public float regenTime = 5.0f;
    public float regenTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        regenTimer -= Time.deltaTime;
        if(regenTimer <= 0.0f)
        {
            health += regenRate * Time.deltaTime;
            if (health > maxHealth)
                health = maxHealth;
        }
	}

    //Called when the player takes damage
    public void TakeDamage(float damage)
    {
        regenTimer = regenTime;
        health -= damage;

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ShutDown(float downTime)
    {
        //Implement a way to disable player completely
        print("Shutdown for " + downTime);
    }
}
