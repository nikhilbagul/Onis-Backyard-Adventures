using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectPlayerHit : MonoBehaviour {

    public Slider enemyHealthSlider;

    private SkeletonAI enemyHandler;

    // Use this for initialization
    void Start ()
    {
        enemyHandler = GameObject.Find("SkeletonEnemy").GetComponent<SkeletonAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider otherColl)
    {
        if (otherColl.tag == "PlayerWeapon")
        {
            //StartCoroutine("AnimationTriggerDelay");
            enemyHandler.setIsTakingDamage();
            Debug.Log("Player Hit Detected");
            enemyHealthSlider.value = enemyHealthSlider.value - 20;
        }
    }

    public IEnumerator AnimationTriggerDelay()
    {
        yield return new WaitForSeconds(0.25f);
        enemyHandler.setIsTakingDamage();
        enemyHealthSlider.value = enemyHealthSlider.value - 20;
    }
}
