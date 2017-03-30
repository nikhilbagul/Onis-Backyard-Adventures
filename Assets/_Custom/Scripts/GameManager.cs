using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Canvas gameHudCanvas;

    //private Blur_MOD blurRef;

    // Use this for initialization
    void Start()
    {
        gameHudCanvas.gameObject.SetActive(false);
        Camera.main.GetComponent<Blur_MOD>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
