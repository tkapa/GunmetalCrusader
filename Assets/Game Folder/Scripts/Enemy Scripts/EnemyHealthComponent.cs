﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour {

    [HideInInspector]
    public float health = 100.0f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            OnDeath();
    }

    public void OnDeath()
    {
        print("Enemy Killed");
        if (FindObjectOfType<Wave0Script>() != null)
        {
            FindObjectOfType<Wave0Script>().SwarmersAlive--;
        }
<<<<<<< HEAD
        this.GetComponent<Enemy>().OnDeath();
=======

        Destroy(this.gameObject);
>>>>>>> Develop
        EventManager.instance.OnEnemyDeath.Invoke();
        Destroy(this.gameObject);
    }
}
