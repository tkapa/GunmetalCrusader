using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    AudioSource source;

    bool played = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (played && source.isPlaying)
            Destroy(this.gameObject, 0.1f);
	}

    public void PlayClipOnce(AudioClip clip)
    {
        played = true;

        source.PlayOneShot(clip);
    }
}
