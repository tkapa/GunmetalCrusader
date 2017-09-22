using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shepherd : Enemy {

    [Tooltip("The range at which ")]
    public float effectiveRange = 10.0f;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        moveToTransform = target.transform.position;
        base.Update();
	}

    void BuffSwarmers()
    {
        if (FindObjectOfType<Swarmer>())
        {
            foreach(Swarmer s in FindObjectsOfType<Swarmer>())
            {
                float distance = Vector3.Distance(transform.position, s.transform.position);

                if (distance < effectiveRange)
                    s.isBuffed = true;
                else
                    s.isBuffed = false;
            }
        }
    }
}
