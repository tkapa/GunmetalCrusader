using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGibComponent : MonoBehaviour {

    bool done = false;

    float range = 500.0f;

    float timer;

    public GameObject explosion;

	// Use this for initialization
	void Start () {
        timer = Random.Range(1.0f,3.0f);
    }

    private void Update()
    {
        if (!done)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-range, range), Random.Range(0, range * 1.5f), Random.Range(-range, range)));
            done = true;
        }

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Instantiate(explosion, this.transform.position, explosion.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
