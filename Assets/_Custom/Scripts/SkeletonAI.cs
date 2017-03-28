using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonAI : MonoBehaviour {

    public Transform player, enemyHead;
    public Slider enemyHealthSlider, playerHealthSlider;
    public float timeToRotate;
    public float attackRange = 3;
    public float FOVRadius = 15;
    public float FOVAngle = 90;
   
    private Animator enemyAnimatorController;
    private Vector3 direction;
    private float angleFOV;
    private bool pursuing, isTakingDamage, isDead, hasWon;

    // Use this for initialization
    void Start ()
    {
        timeToRotate = 0.1f;
        enemyAnimatorController = GetComponent<Animator>();
        pursuing = false;
        isTakingDamage = false;
        isDead = false;
        hasWon = false;        
    }
	
	// Update is called once per frame
	void Update ()
    {
        direction = player.position - this.transform.position;              //calculate the direction vector from enemy to player
        direction.y = 0.0f;                                                 //prevent the enemy from bending            
        angleFOV = Vector3.Angle(enemyHead.transform.up, direction);
        
        if (Vector3.Distance(this.transform.position, player.position) < FOVRadius && (angleFOV < FOVAngle/2 || pursuing) && !isDead && !hasWon)            //when the player is within enemy's range and FOV
        {
            pursuing = true;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), timeToRotate);      //rotate the enemy to face the player

            enemyAnimatorController.SetBool("isIdle", false);
            if (direction.magnitude > attackRange)
            {
                this.transform.Translate(0, 0, 0.05f);
                enemyAnimatorController.SetBool("isWalking", true);
                enemyAnimatorController.SetBool("isAttacking", false);
            }

            else        //defines the enemy attack radius/AOE
            {
                enemyAnimatorController.SetBool("isWalking", false);
                enemyAnimatorController.SetBool("isAttacking", true);

                if (playerHealthSlider.value <= 0 )                             //check for player's death and play victory animation
                {
                    enemyAnimatorController.SetBool("hasWon", true);
                    enemyAnimatorController.SetBool("isAttacking", false);
                    enemyAnimatorController.SetBool("isIdle", false);
                    enemyAnimatorController.SetBool("isWalking", false);
                    enemyAnimatorController.SetBool("isTakingDamage", false);
                    hasWon = true;
                }
            }
        }

        else        //swtich the animations off when player is out of enemy range
        {
            enemyAnimatorController.SetBool("isIdle", true);
            enemyAnimatorController.SetBool("isWalking", false);
            enemyAnimatorController.SetBool("isAttacking", false);
            enemyAnimatorController.SetBool("isTakingDamage", false);
            pursuing = false;
        }

        if (isTakingDamage)
        {
            enemyAnimatorController.SetBool("isTakingDamage", true);
            enemyAnimatorController.SetBool("isIdle", false);
            enemyAnimatorController.SetBool("isWalking", false);
            enemyAnimatorController.SetBool("isAttacking", false);

            if (enemyAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                //Debug.Log("animation complete !");
                isTakingDamage = false;
                enemyAnimatorController.SetBool("isTakingDamage", false);
                enemyAnimatorController.SetBool("isIdle", true);
                enemyAnimatorController.SetBool("isWalking", false);
                enemyAnimatorController.SetBool("isAttacking", false);
            }
        }

        if (enemyHealthSlider.value == 0)
        {
            enemyAnimatorController.SetBool("isDead", true);
            enemyAnimatorController.SetBool("isIdle", false);
            enemyAnimatorController.SetBool("isWalking", false);
            enemyAnimatorController.SetBool("isAttacking", false);
            enemyAnimatorController.SetBool("isTakingDamage", false);
            isDead = true;
        }          	
	}

    public void setIsTakingDamage()
    {
        isTakingDamage = true;
    }
}
