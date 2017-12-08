using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    // Misc
    private Player pRef;
    private Mecha_MovementHandler moveHandle;
    private Mecha_InventoryManager iRef;

    // HP
    private float displayHP = 0;

    [SerializeField]
    private RectTransform HPMask;
    [SerializeField]
    private float HPFullSize = 100.0f;

    // Boost
    private float displayBoost = 0;

    [SerializeField]
    private RectTransform BoostMask;
    [SerializeField]
    private float BoostFullsize = 100.0f;

    // Heat
    private float[] displayHeat = new float[2];
    private bool[] hasOverheated = new bool[2];

    [SerializeField]
    private RectTransform[] heatMask = new RectTransform[2];
    [SerializeField]
    private GameObject[] overheatIcon = new GameObject[2];
    [SerializeField]
    private float heatFullsize = 100.0f;

    // Score
        //TODO : Do the score

    // Use this for initialization
    void Start () {
        
        // Misc
        pRef = FindObjectOfType<Player>();
        moveHandle = FindObjectOfType<Mecha_MovementHandler>();
        iRef = FindObjectOfType<Mecha_InventoryManager>();

        // HP
        displayHP = pRef.health;
    }
	
	// Update is called once per frame
	void Update () {
        // HP
        //if(displayHP != pRef.health)
            displayHP = pRef.health; // TODO : Maybe lerp this??

        HPMask.sizeDelta = new Vector2((displayHP / pRef.maxHealth) * HPFullSize, 200);

        // Boost
        //if (displayBoost != moveHandle.jumpChargeTimer)
            displayBoost = moveHandle.jumpChargeTimer; // TODO : Maybe lerp this??

        BoostMask.sizeDelta = new Vector2(1000, ((displayBoost / moveHandle.jumpChargeTime)) * BoostFullsize);

        // Heat(s)
        for(int i = 0; i < 2; i++)
        {
            if (iRef.spawnedWeapons[i] != null)
            {
                //if (displayHeat[i] != iRef.spawnedWeapons[i].currentHeatValue)
                displayHeat[i] = iRef.spawnedWeapons[i].currentHeatValue; // TODO : Maybe lerp this??

                //if (hasOverheated[i] != iRef.spawnedWeapons[i].hasOverheated)
                hasOverheated[i] = iRef.spawnedWeapons[i].hasOverheated;

                heatMask[i].sizeDelta = new Vector2(1000, ((displayHeat[i] / 100.0f)) * heatFullsize);
                overheatIcon[i].SetActive(hasOverheated[i]);
            }
        }
    }
}
