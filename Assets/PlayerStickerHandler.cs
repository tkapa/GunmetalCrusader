using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickerHandler : MonoBehaviour {

    private GameObject[] StickersAvailable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckFOrStickers();
	}

    

   void  CheckFOrStickers()
    {
        StickersAvailable = GameObject.FindGameObjectsWithTag("Sticker");

        foreach(GameObject I in StickersAvailable)
        {
            if (Vector3.Distance(this.transform.position, I.transform.position) <= 3)
                {

            }
        }


    }



}
