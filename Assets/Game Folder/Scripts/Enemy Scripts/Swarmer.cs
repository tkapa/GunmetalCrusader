using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : Enemy {

    public float separationStrength = 1.0f;

    Vector3 moveToPos = new Vector3(0, 0, 0);

    EnemySpawningManager spawningManager;
    SwarmerDetector detector;

    public override void Start()
    {
        base.Start();

        spawningManager = FindObjectOfType<EnemySpawningManager>();

        if (!GetComponentInChildren<SwarmerDetector>())
            Debug.LogError("There is no SwarmerDetector component on" + this.name);
        else
            detector = GetComponentInChildren<SwarmerDetector>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateDestination()
    {
        if (destinationUpdateTimer < 0)
        {
            destinationUpdateTimer = destinationUpdateTime;
            SwarmerBehaviour();
        }
        else
            destinationUpdateTimer -= Time.deltaTime;
    }

    //Swarmer Behaviour
    void SwarmerBehaviour()
    {
    }
}
