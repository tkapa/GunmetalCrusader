using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOverTime : MonoBehaviour {

    // The firing animation clip.
    [Tooltip("The amount that should be rotated per second.")]
    [SerializeField]
    private Vector3 rotAmount = new Vector3(0,0,0);

    // The firing animation clip.
    [Tooltip("The firing animation clip.")]
    [SerializeField]
    private bool Randomize = false;

    // The firing animation clip.
    [Tooltip("Percentage of the total to be used as a minimum when randomising.")]
    [SerializeField]
    private float MinRandom = 0.5f;

    // Use this for initialization
    void Start () {
		if(Randomize)
        {
            Vector3 newrot;
            newrot.x = Random.Range(rotAmount.x * MinRandom, rotAmount.x);
            newrot.y = Random.Range(rotAmount.y * MinRandom, rotAmount.y);
            newrot.z = Random.Range(rotAmount.z * MinRandom, rotAmount.z);

            rotAmount = newrot;
        }
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles += rotAmount * Time.deltaTime;

    }
}
