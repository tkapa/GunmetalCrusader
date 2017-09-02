using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningObject : MonoBehaviour {

    Player player;
    EnemySpawningManager spawningManager;

    float updateObjectsTime = 0.5f, updateObjectsCounter;

    public float maximumDistanceFromPlayer = 20;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        spawningManager = FindObjectOfType<EnemySpawningManager>();
        spawningManager.spawningObjects.Add(this.gameObject);
        updateObjectsCounter = updateObjectsTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Updates which objects can and cannot spawn enemies at any time
		if(updateObjectsCounter <= 0)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < maximumDistanceFromPlayer && !spawningManager.spawningObjects.Contains(this.gameObject))
            {
                spawningManager.spawningObjects.Add(this.gameObject);
            }
            else if (spawningManager.spawningObjects.Contains(this.gameObject))
            {
                spawningManager.spawningObjects.Remove(this.gameObject);
            }
        }
	}
}
