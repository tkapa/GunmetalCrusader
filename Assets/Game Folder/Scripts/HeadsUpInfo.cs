using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpInfo : MonoBehaviour {

    public Text eliminationsText;
    public Text enemyCountText;

    public Text timeToNextRoundText;

    public Text leftWeaponAmmunitionText, rightWeaponAmmunitionText;

    public int eliminations;
    public int enemyCount;

    public float timeToNextRound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        eliminationsText.text = eliminations.ToString();
        enemyCountText.text = enemyCount.ToString();
        timeToNextRoundText.text = ((int)timeToNextRound).ToString();
	}
}
