using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MenuManager : MonoBehaviour {

   

    public float SkyRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SkyRot);
    }
}
