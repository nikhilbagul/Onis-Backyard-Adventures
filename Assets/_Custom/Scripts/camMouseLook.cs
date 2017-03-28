using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {

    public float smoothing = 2.0f;
    public float senstivity = 5.0f;

    GameObject playerCharacter;

    private Vector2 mouseLook;          //keeps strack of the mouse movement on the screen in 2D (x and y)
    private Vector2 smoothV;
    
    // Use this for initialization
	void Start () {

        playerCharacter = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y") );            //change in mouse movement every update
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(senstivity * smoothing, senstivity * smoothing));    //scale down the mouse delta value according to the senstivity and smoothing value

        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1.0f/ smoothing);           //to prevent jerky mouse movements
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1.0f/ smoothing);
        mouseLook = mouseLook + smoothV;            //to keep track of the total mouse movement

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, transform.right);                //to rotate the camera up & down around the x axis(inverted )
        playerCharacter.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, playerCharacter.transform.up);    //to rotate the player's transform around the y axis   

    }
}
