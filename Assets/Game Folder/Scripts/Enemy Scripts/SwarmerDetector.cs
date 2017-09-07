using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerDetector : MonoBehaviour {

    public List<GameObject> surroundingSwarmers = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Swarmer>())
        {
            surroundingSwarmers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Swarmer>() && surroundingSwarmers.Contains(other.gameObject))
        {
            surroundingSwarmers.Remove(other.gameObject);
        }
    }
}
