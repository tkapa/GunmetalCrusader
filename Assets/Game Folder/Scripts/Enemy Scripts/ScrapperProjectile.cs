using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapperProjectile : MonoBehaviour {

    public float movementSpeed = 10.0f;
    public float damageValue = 0.0f;

    public List<GameObject> damagingObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
    }

    void DamageObjects()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Boom");
        Destroy(this.gameObject, 0.2f);
    }
}
