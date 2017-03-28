using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    public GameObject shield;
    public BoxCollider sword;
    public Image damageOverlay;
    public Slider playerHealthSlider;
    public float speed = 10.0f;
    //public float rotationSpeed = 100.0f;

    private Animator playerAnimatorController;
    private bool isTakingDamage, isDead;
	// Use this for initialization
	void Start ()
    {
        playerAnimatorController = GetComponent<Animator>();
        shield.gameObject.SetActive(false);
        sword.enabled = false;
        isTakingDamage = false;
        isDead = false;
        damageOverlay.gameObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;	
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (!isDead)
        {
            float straffe = Input.GetAxis("Horizontal") * speed;
            float translation = Input.GetAxis("Vertical") * speed;

            translation = translation * Time.deltaTime;         //movement on z axis - forward/backward movement
            straffe = straffe * Time.deltaTime;                 //movement on x axis - sideways movement
            transform.Translate(straffe, 0, translation);


            if (Input.GetButton("Attack"))
            {
                playerAnimatorController.SetBool("isAttacking", true);
                playerAnimatorController.SetBool("isIdle", false);
                sword.enabled = true;
                //call delay coroutine
                //StartCoroutine("SwordSpawnDelay");            
            }
            else
            {
                playerAnimatorController.SetBool("isAttacking", false);
                sword.enabled = false;
                //playerAnimatorController.SetBool("isIdle", true);
            }

            if (Input.GetButton("Block"))
            {
                shield.gameObject.SetActive(true);
                playerAnimatorController.SetBool("isBlocking", true);
                playerAnimatorController.SetBool("isIdle", false);
            }

            else
            {
                shield.gameObject.SetActive(false);
                playerAnimatorController.SetBool("isBlocking", false);
                //playerAnimatorController.SetBool("isIdle", true);
            }


            if (translation != 0)
            {
                if (translation > 0)
                {
                    if (!playerAnimatorController.GetBool("isWalking"))
                    {
                        playerAnimatorController.SetBool("isWalking", true);
                        playerAnimatorController.SetBool("isIdle", false);
                    }
                }

                else
                {
                    if (!playerAnimatorController.GetBool("isWalking"))
                    {
                        //transform.Rotate(transform.up, 180.0f);
                        playerAnimatorController.SetBool("isWalking", true);
                        playerAnimatorController.SetBool("isIdle", false);
                    }
                }

            }
            else
            {
                playerAnimatorController.SetBool("isWalking", false);

                if (!playerAnimatorController.GetBool("isBlocking"))
                    playerAnimatorController.SetBool("isIdle", true);
            }

            if (straffe != 0)
            {
                if (straffe > 0)
                {
                    playerAnimatorController.SetBool("isStraffeRight", true);
                    playerAnimatorController.SetBool("isIdle", false);
                }

                if (straffe < 0)
                {
                    playerAnimatorController.SetBool("isStraffeLeft", true);
                    playerAnimatorController.SetBool("isIdle", false);
                }
            }
            else
            {
                playerAnimatorController.SetBool("isStraffeLeft", false);
                playerAnimatorController.SetBool("isStraffeRight", false);
                //playerAnimatorController.SetBool("isWalking", false);
                //playerAnimatorController.SetBool("isIdle", true);
            }

            if (isTakingDamage)
            {
                playerAnimatorController.SetBool("isTakingDamage", true);
                playerAnimatorController.SetBool("isIdle", false);
                playerAnimatorController.SetBool("isWalking", false);
                playerAnimatorController.SetBool("isAttacking", false);
                playerAnimatorController.SetBool("isStraffeRight", false);
                playerAnimatorController.SetBool("isStraffeLeft", false);
                damageOverlay.gameObject.SetActive(true);

                if (playerAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage"))
                {
                    //Debug.Log("animation complete !");
                    isTakingDamage = false;
                    playerAnimatorController.SetBool("isTakingDamage", false);
                    playerAnimatorController.SetBool("isIdle", true);
                    damageOverlay.gameObject.SetActive(false);
                }

                if (playerHealthSlider.value <= 0)                              //checking if the player is Dead
                {
                    playerAnimatorController.SetBool("isTakingDamage", false);
                    playerAnimatorController.SetBool("isDead", true);
                    isDead = true;
                }
            }


            if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        

    }

    public void setIsTakingDamage()
    {
        isTakingDamage = true;
    }

    public IEnumerator SwordSpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        //sword.gameObject.SetActive(true);
    }
}
