using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneChanger : MonoBehaviour {

    public static GameSceneChanger inst;

    private int storedScene = -1;

    // How long a transition takes (One way, so double for the entire transition)
    [Tooltip("The scene to load immediately")]
    [SerializeField]
    private int initialScene = 1;

    // How long a transition takes (One way, so double for the entire transition)
    [Tooltip("How long a transition takes")]
    [SerializeField]
    private float maxTransitionTimer = 3.0f;

    // The firing animation clip.
    [Tooltip("The renderer of the transitionsphere")]
    [SerializeField]
    private MeshRenderer tSphere;

    private float transitionTimer = 0.0f;

    private void Start()
    {
        inst = this;
        InstantSceneChange(initialScene); 
    }

    private void Update()
    {
        transitionTimer = Mathf.Clamp(transitionTimer - Time.deltaTime, -maxTransitionTimer, maxTransitionTimer);

        if (transitionTimer <= 0.0f)
            CommitSceneChange();

        UpdateTransitionSphere();
    }

    private void UpdateTransitionSphere()
    {
        float alpha = Mathf.Lerp(0.0f, 1.25f, 1 -(Mathf.Abs(transitionTimer) / maxTransitionTimer));

        tSphere.material.SetColor("_Color", new Color(0,0,0, alpha));
    }

    public void LoadScene(int SceneNum)
    {
        if (storedScene == -1) // Check to ensure we aren't mid scene transition
        { 
            storedScene = SceneNum;
            transitionTimer = maxTransitionTimer;
        }
    }

    public void InstantSceneChange(int SceneNum)
    {
        if (storedScene == -1)  // Check to ensure we aren't mid scene transition
        {
            storedScene = SceneNum;
            transitionTimer = 0.0f;
        }
    }

    private void CommitSceneChange()
    {
        if (storedScene != -1)
        {
            // Destroy the Old Scene
            Destroy(GameObject.FindGameObjectWithTag("MapRootObj"));
            // Load the new one
            SceneManager.LoadScene(storedScene, LoadSceneMode.Additive);
            // And Reset the Transition
            storedScene = -1;
            // Now move the player, if there's a spawnpoint and a player
            GameObject sp = GameObject.FindGameObjectWithTag("PlayerSpawn");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Null check to ensure shit didn't fuck up lol
            if (sp != null && player != null)
                player.transform.position = sp.transform.position;
            else
                Debug.Log("ERROR! Cannot find Player or Spawn Point during Scene Transition.");
        }
    }
}
