using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapperProjectileTTracker : MonoBehaviour {

    ScrapperProjectile parent;

	// Use this for initialization
	void Start () {
        parent = GetComponent<ScrapperProjectile>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!parent.damagingObjects.Contains(other.gameObject))
        {
            if (other.gameObject.GetComponent<Swarmer>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Scrapper>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Welder>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Glitch>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Shepherd>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Juggernaut>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Phoenix>())
                parent.damagingObjects.Add(other.gameObject);
            else if (other.gameObject.GetComponent<Player>())
                parent.damagingObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (parent.damagingObjects.Contains(other.gameObject))
            parent.damagingObjects.Remove(other.gameObject);
    }
}
