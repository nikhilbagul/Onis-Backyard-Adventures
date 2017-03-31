using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {


    private GameManager gameManagerRef;

	// Use this for initialization
	void Start ()
    {
        gameManagerRef = this.gameObject.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !Camera.main.GetComponent<Blur_MOD>().enabled && ! GameManager.isPaused)           //check for Espace press and game pause
        {
            gameManagerRef.OnPause();
        }

        if (Input.GetKey(KeyCode.Escape) && Camera.main.GetComponent<Blur_MOD>().enabled && GameManager.isPaused)
        {
            gameManagerRef.OnUnpause();
        }
    }

    public void OnNewGameClick()
    {
        gameManagerRef.OnNewGame();
    }

    public void OnRestartClick()
    {
        gameManagerRef.OnRestart();
    }

    public void OnQuitClick()
    {
        gameManagerRef.OnQuit();
    }

    public void OnResumeClick()
    {
        gameManagerRef.OnUnpause();    
    }

    }
