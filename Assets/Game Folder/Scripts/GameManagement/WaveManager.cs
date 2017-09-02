using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float roundIntervalTime = 30.0f;
    float roundIntervalTimer;

    bool isCounting = true;

	// Use this for initialization
	void Start () {
        roundIntervalTimer = roundIntervalTime;

        EventManager.instance.OnEndRound.AddListener(() => {
            roundIntervalTimer = roundIntervalTime;
            isCounting = true;
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (isCounting)
            RoundCountdown();
	}

    //Manages when a round should begin
    void RoundCountdown()
    {
        if(roundIntervalTimer <= 0)
        {
            isCounting = false;
            EventManager.instance.OnStartRound.Invoke();
        }
        else
        {
            roundIntervalTimer -= Time.deltaTime;
        }
    }
}
