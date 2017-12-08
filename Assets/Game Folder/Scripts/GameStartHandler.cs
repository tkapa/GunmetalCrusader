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

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    GameStarted();
        //}
    }

    

    public void GameStarted()
    {
        Debug.Log("game started");
        DiminishMenu();
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Animator>().SetTrigger("Start");



    }

    public void GameEnd()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        MyAnim.SetTrigger("OpenCredits");
    }

    public void CloseCredits()
    {
        MyAnim.SetTrigger("CloseCredits");
    }

    void ExpandMenu()
    {
        MyAnim.SetTrigger("OpenMenu");
        Mecha_InventoryManager.Instance.ClearInventory();
    }

    void DiminishMenu()
    {
        MyAnim.SetTrigger("CloseMenu");
        Mecha_InventoryManager.Instance.InitializeInventory();
    }

    

}
