using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MenuManager : MonoBehaviour {

    public Material Skyyyy;

    public float SkyRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SkyRot);
    }

    public void LoadLevel()
    {

        RenderSettings.skybox = Skyyyy;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GameSceneChanger>().LoadScene(3);

     
    }
}
