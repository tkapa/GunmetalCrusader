using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour {

	public GameObject androidGiblets;

    [HideInInspector]
    public float health = 100.0f;

	[SerializeField]
	private float scoreValue = 10;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            OnDeath();
    }

    public void OnDeath()
    {
        if (FindObjectOfType<Wave0Script>() != null)
        {
            FindObjectOfType<Wave0Script>().SwarmersAlive--;
        }
        EventManager.instance.OnEnemyDeath.Invoke();
		ScoreManager.instance.ScoreIncrease (scoreValue);
		Instantiate(androidGiblets, this.transform.position, androidGiblets.transform.rotation);
        Destroy(this.gameObject);
    }
}
