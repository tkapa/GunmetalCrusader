using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : Enemy {

    public AnimationCurve shutDownTimeCurve;
    public float maximumShutdownTime = 5.0f;

	// Update is called once per frame
	public override void Update () {
        moveToTransform = target.transform.position;
	}

    //Attacking with the Glitch shutsdown the Mech
    public override void Attack()
    {
        /*
         * Other behaviour for ShutDown AKA Kill Swarmers
         */
        float downTime = shutDownTimeCurve.Evaluate(roundPercentage);
        target.ShutDown(downTime);
    }
}
