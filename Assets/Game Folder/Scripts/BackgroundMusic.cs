using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip[] musicClips;
    AudioSource source;
    AudioLowPassFilter lowPassFilter;
    Player playerObject;

    public float maximumCutOffValue = 20000.0f;

    bool isTakingPlayerHealth = false;

	// Use this for initialization
	void Start () {
        StartInitials();

        EventManager.instance.OnStartRound.AddListener(()=> {
            isTakingPlayerHealth = true;
        });
        EventManager.instance.OnEndRound.AddListener(()=> {
            isTakingPlayerHealth = false;
            lowPassFilter.cutoffFrequency = 250.0f;
        });

    }
	
	// Update is called once per frame
	void Update () {
        if (!source.isPlaying)
            PlayNewAudio();

        if (isTakingPlayerHealth)
            lowPassFilter.cutoffFrequency = DynamicPass();
	}

    //Plays a new audio clip continuously
    void PlayNewAudio()
    {
        source.PlayOneShot(musicClips[(int)Random.Range(0, musicClips.Length)]);
    }

    //Dynamically changes low pass filter
    float DynamicPass()
    {
        float passVal = playerObject.health / playerObject.maxHealth;

        passVal *= maximumCutOffValue;

        return Mathf.Clamp(passVal, 250, maximumCutOffValue);
    }

    // Initialisations made when starting
    void StartInitials()
    {
        if (!GetComponent<AudioSource>())
            Debug.Log(this.name + "does not contain an Audio Source!");
        else
            source = GetComponent<AudioSource>();

        if (!GetComponent<AudioLowPassFilter>())
            Debug.Log(this.name + "does not contain an Audio Low Pass Filter!");
        else
            lowPassFilter = GetComponent<AudioLowPassFilter>();

        if (!FindObjectOfType<Player>())
            Debug.Log("There is no Player object on the scene!");
        else
            playerObject = FindObjectOfType<Player>();
    }
}
