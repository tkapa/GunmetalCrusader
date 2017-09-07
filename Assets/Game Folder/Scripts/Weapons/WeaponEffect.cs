using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour {

    // How long this shot survives for before garbage collection.
    [Tooltip("How long this shot survives for before garbage collection.")]
    [SerializeField]
    protected float existTime = 3.0f;

    /*
     * Called on instance create
     */
    protected virtual void Start()
    {
        Destroy(this.gameObject, existTime);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
