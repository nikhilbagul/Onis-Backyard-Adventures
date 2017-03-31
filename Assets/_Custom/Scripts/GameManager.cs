using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Canvas gameHudCanvas;
    public Button newGameButton, resumeButton, quitButton, restartButton;

    public static bool isPaused;
    private GameObject player;
    
    // initialization
    void Start()
    {
        Camera.main.GetComponent<Blur_MOD>().enabled = true;

        gameHudCanvas.gameObject.SetActive(false);
        isPaused = false;
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)                                     //disable user Input
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
               
    }


    public void OnNewGame()
    {
        newGameButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        Camera.main.GetComponent<Blur_MOD>().enabled = false;
        
        if (player != null)     //enable user Input
        {
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
            gameHudCanvas.gameObject.SetActive(true);
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuit()
    {
        Application.Quit();
    }


    public void OnUnpause()
    {
        Camera.main.GetComponent<Blur_MOD>().enabled = false;
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        //quitButton.gameObject.SetActive(false);

        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
        gameHudCanvas.gameObject.SetActive(true);
                
        StartCoroutine("PauseInputDelay");
    }

    public void OnPause()
    {
        Camera.main.GetComponent<Blur_MOD>().enabled = true;
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        //quitButton.gameObject.SetActive(true);

        gameHudCanvas.gameObject.SetActive(false);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<MouseLook>().enabled = false;
        
        StartCoroutine("PauseInputDelay");
    }
    
    public IEnumerator PauseInputDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isPaused = !isPaused;
    }
}
