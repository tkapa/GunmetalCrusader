using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerDiedAudiohack : MonoBehaviour {

    public AudioSource ThisAudio;
    public AudioClip DeathPew;

    // Use this for initialization
    void Start () {
        if (ThisAudio != null)
        {
            ThisAudio.PlayOneShot(DeathPew);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
