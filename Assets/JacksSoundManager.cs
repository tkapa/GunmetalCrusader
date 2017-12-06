using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacksSoundManager : MonoBehaviour {
    public static JacksSoundManager Instance;

    //the audio sources we need around the mech
    public AudioSource Jumpo;
    public AudioSource Turno;
    public AudioSource Deatho;

    //jump related clips
    public AudioClip JumpStart;
    public AudioClip JumpMidair;
    public AudioClip JumpLand;

    //the turning noises used when the mech decided to turn
    public AudioClip[] TurningWhirrs;

    //mechdeath
    public AudioClip MechDeath;

	
	void Start ()
    {
        Instance = this;	
	}
	
	
	void Update ()
    {
		
	}
    //jumping related functions
    public void MechJumped()
    {
        Jumpo.loop = false;
        Jumpo.Stop();
        Jumpo.PlayOneShot(JumpStart);
    }

    public void MechMidair()
    {
        Jumpo.loop = true;
        Jumpo.clip = JumpMidair;
        Jumpo.Play();
    }

    public void MechLanded()
    {
        Jumpo.loop = false;
        Jumpo.Stop();
        Jumpo.PlayOneShot(JumpLand);
    }


    //turning related functions
    public void MechTurning()
    {
        Turno.PlayOneShot(TurningWhirrs[Random.Range(0, TurningWhirrs.Length)]);
    }

    public void StoppedTurning()
    {
        Turno.Stop();
    }


    public void Died()
    {
        Turno.Stop();
        Jumpo.Stop();
        Deatho.PlayOneShot(MechDeath);
    }
}
