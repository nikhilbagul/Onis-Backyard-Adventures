using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed = 10.0f;
    //public float rotationSpeed = 100.0f;

    private Animator playerAnimatorController;

	// Use this for initialization
	void Start ()
    {
        playerAnimatorController = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        float straffe = Input.GetAxis("Vertical") * speed;

        translation = translation * Time.deltaTime;         //movement on z axis - forward/backward movement
        straffe = straffe * Time.deltaTime;                 //movement on x axis - sideways movement
        transform.Translate(straffe, 0, translation);

        if (Input.GetButton("Fire1"))
        {
            playerAnimatorController.SetBool("isAttacking", true);
        }
        else
        {
            playerAnimatorController.SetBool("isAttacking", true);
        }

        if (translation != 0)
        {
            playerAnimatorController.SetBool("isWalking", true);
            playerAnimatorController.SetBool("isIdle", false);
        }
        else
        {
            playerAnimatorController.SetBool("isWalking", false);
            playerAnimatorController.SetBool("isIdle", true);
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
