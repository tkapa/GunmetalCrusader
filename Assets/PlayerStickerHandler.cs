using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickerHandler : MonoBehaviour {

    public float TimeBetweenChecks;

    private float Checker;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Checker -= Time.deltaTime;
		
	}
}
