using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerRange : MonoBehaviour {

    Swarmer parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Swarmer>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Swarmer" && !parent.surroundingSwarmers.Contains(other.gameObject))
            parent.surroundingSwarmers.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Swarmer" && parent.surroundingSwarmers.Contains(other.gameObject))
            parent.surroundingSwarmers.Remove(other.gameObject);
    }
}
