using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float health = 100.0f;
    public float maxHealth = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called when the player takes damage
    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
            EventManager.instance.OnPlayerDeath.Invoke();
    }

    public void ShutDown(float downTime)
    {
        //Implement a way to disable player completely
        print("Shutdown for " + downTime);
    }
}
