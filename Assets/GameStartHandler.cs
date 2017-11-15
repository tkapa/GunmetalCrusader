using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartHandler : MonoBehaviour {


    public bool Tutorial = true;
    private bool HasPlayed;


    public Animator MyAnim;

	// Use this for initialization
	void Start () {
        ExpandMenu();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.D))
        {
            DiminishMenu();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ExpandMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartTut();
        }
    }

    public void TutValueChanged()
    {
        if (Tutorial) Tutorial = false;
        else Tutorial = true;
    }

    public void GameStarted()
    {
        if (Tutorial)
        {
            StartTut();
        }

        else StartNoTut();
        HasPlayed = true;
    }

    void ExpandMenu()
    {
        MyAnim.SetTrigger("OpenMenu");
        Debug.Log("MenuExpanded");
    }

    void DiminishMenu()
    {
        MyAnim.SetTrigger("CloseMenu");
        Debug.Log("MenuClosed");
    }

    void StartNoTut()
    {
        DiminishMenu();
    }

    void StartTut()
    {
        DiminishMenu();
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Animator>().SetTrigger("Start");
    }

}
