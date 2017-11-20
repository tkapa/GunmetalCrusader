using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mAKEsoLIDsCRIPT : MonoBehaviour {
    public GameObject Solid;
	// Use this for initialization
	void Start () {
        Solid.GetComponent<Collider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeSolid()
    {
        Solid.GetComponent<Collider>().enabled = true;
    }
}
