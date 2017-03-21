using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour {

    public Transform player, enemyHead;
    public float timeToRotate;

    private Animator enemyAnimatorController;
    private Vector3 direction;
    private float angleFOV;
    private bool pursuing;

    // Use this for initialization
    void Start ()
    {
        timeToRotate = 0.1f;
        enemyAnimatorController = GetComponent<Animator>();
        pursuing = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        direction = player.position - this.transform.position;              //calculate the direction vector from enemy to player
        direction.y = 0.0f;                                                 //prevent the enemy from bending            
        angleFOV = Vector3.Angle(enemyHead.transform.up, direction);
        
        if (Vector3.Distance(this.transform.position, player.position) < 10 && (angleFOV < 30 || pursuing))            //when the player is within enemy's range and FOV
        {
            pursuing = true;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), timeToRotate);      //rotate the enemy to face the player

            enemyAnimatorController.SetBool("isIdle", false);
            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.05f);
                enemyAnimatorController.SetBool("isWalking", true);
                enemyAnimatorController.SetBool("isAttacking", false);
            }

            else        //defines the enemy attack radius/AOE
            {
                enemyAnimatorController.SetBool("isWalking", false);
                enemyAnimatorController.SetBool("isAttacking", true);
            }
        }

        else        //swtich the animations off when player is out of enemy range
        {
            enemyAnimatorController.SetBool("isIdle", true);
            enemyAnimatorController.SetBool("isWalking", false);
            enemyAnimatorController.SetBool("isAttacking", false);
            pursuing = false;
        }        	
	}
}
