using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartHandler : MonoBehaviour {


    


    public Animator MyAnim;

	// Use this for initialization
	void Start ()
    {
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
            GameStarted();
        }
    }

    

    public void GameStarted()
    {

        DiminishMenu();
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Animator>().SetTrigger("Start");



    }

    public void OpenCredits()
    {
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Animator>().SetTrigger("OpenCredits");
    }

    public void CloseCredits()
    {
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Animator>().SetTrigger("CloseCredits");
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

    

}
