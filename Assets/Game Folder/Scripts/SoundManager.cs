using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject audioPlayerPrefab;

    [Header("AI Audio Clips")]
    [Space(10)]
    public AudioClip beginGame;

    public AudioClip endGame;

    [Header("Mech Audio Clips")]
    [Space(10)]
    public AudioClip reloading;

    public AudioClip jumping;

    [Header("Enemy Audio Clips")]
    [Space(10)]
    public AudioClip enemyDropIn;

    public AudioClip enemyDeath;

    public void SpawnAudioAtPoint(AudioClip clip, Vector3 point)
    {
        GameObject a = Instantiate(audioPlayerPrefab, 
                        point, 
                        transform.rotation) as GameObject;

        a.GetComponent<AudioPlayer>().PlayClipOnce(clip);
    }
}
