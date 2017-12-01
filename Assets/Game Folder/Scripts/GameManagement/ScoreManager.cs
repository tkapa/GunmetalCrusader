using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public float playerScore = 0;

	private static ScoreManager inst;
	public static ScoreManager instance{
		get{ 
			if (inst == null) {
				var newSM = new GameObject ("ScoreManager");
				inst = newSM.AddComponent<ScoreManager> ();
			}

			return inst;
		}
	}

	private void Awake(){
		if (inst != null) {
			DestroyImmediate (this);
			return;
		}

		inst = this;
	}

	public void ScoreIncrease(float _score){
		playerScore += _score;
	}
}
