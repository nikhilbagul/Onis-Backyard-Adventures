using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldDetect : MonoBehaviour {

    public static bool isBlocked;

	// Use this for initialization
	void Start ()
    {
        isBlocked = false;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "EnemyWeapon")
        {
            isBlocked = true;        
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "EnemyWeapon")
        {
            isBlocked = false;
        }
    }
}
