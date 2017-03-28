using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectEnemyHit : MonoBehaviour {

    public Slider playerHealthSlider;
    private CharacterController characterHandler;

    // Use this for initialization
    void Start()
    {
        characterHandler = GameObject.Find("littleswordfighter_Mod").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider otherColl)
    {
        if (otherColl.tag == "EnemyWeapon" && !PlayerShieldDetect.isBlocked)
        {
            characterHandler.setIsTakingDamage();
            Debug.Log("Enemy Hit Detected");
            playerHealthSlider.value = playerHealthSlider.value - 20;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "enemyweapon")
    //    {
    //        characterHandler.setIsTakingDamage();
    //        Debug.Log("enemy hit detected");
    //        playerHealthSlider.value = playerHealthSlider.value - 20;
    //    }
    //}
}
