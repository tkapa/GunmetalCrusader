using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapperProjectile : MonoBehaviour {

    public float movementSpeed = 10.0f;
    float damageValue = 0.0f;
    public Scrapper parent;
	// Use this for initialization
	void Start () {
        damageValue = parent.damage;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void DamageObjects()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Boom, dealt: " + damageValue);
        Destroy(this.gameObject, 0.1f);
    }
}
