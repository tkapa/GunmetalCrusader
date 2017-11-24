using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public float roundIntervalTime = 30.0f;
    float roundIntervalTimer;

    bool isCounting = false;

    [SerializeField]
    private GameObject[] WeaponPickups = new GameObject[2];

    HeadsUpInfo hud;

    [SerializeField]
    private GameManager gm;

    private GameObject spawnedPickup;

    // Use this for initialization
    void Start()
    {
        EventManager.instance.OnStartGame.AddListener(() => {
            roundIntervalTimer = roundIntervalTime;
            isCounting = true;
        });

        EventManager.instance.OnEndRound.AddListener(() => {
            roundIntervalTimer = roundIntervalTime;
            isCounting = true;

            SpawnPickup();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
            RoundCountdown();
    }

    //Manages when a round should begin
    void RoundCountdown()
    {
        if (roundIntervalTimer <= 0)
        {
            print("Starting new round!");

            if (hud)
                hud.timeToNextRound = 0;

            isCounting = false;
            EventManager.instance.OnStartRound.Invoke();
        }
        else
        {
            roundIntervalTimer -= Time.deltaTime;

            if (hud)
                hud.timeToNextRound = roundIntervalTimer;
        }
    }

    void SpawnPickup()
    {
        GameObject sp = GameObject.FindGameObjectWithTag("WeaponSpawnPoint");

        Destroy(spawnedPickup);

        if(gm.currentRound < WeaponPickups.Length)
            spawnedPickup = Instantiate(WeaponPickups[Random.Range(0, gm.currentRound)], sp.transform) as GameObject;
        else
            spawnedPickup = Instantiate(WeaponPickups[Random.Range(0, WeaponPickups.Length - 1)], sp.transform) as GameObject;
    }
}
