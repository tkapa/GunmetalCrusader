using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerRange : MonoBehaviour {

    Swarmer parent;

    public int rangeRadius = 3;

	// Use this for initialization
	void Start () {
        GetComponent<SphereCollider>().radius = rangeRadius;
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
