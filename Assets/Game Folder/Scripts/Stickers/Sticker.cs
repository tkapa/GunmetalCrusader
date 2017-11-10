using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour {

    public float RotateSpeed;

    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed);
	}

    public void StickerCOllected()
    {
        Destroy(this.gameObject);
    }

}
