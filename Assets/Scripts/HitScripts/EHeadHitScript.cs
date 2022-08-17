using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHeadHitScript : MonoBehaviour
{
    private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
            if(other.tag == "PlayerFoot" && enemyController.canGetHit || other.tag == "PlayerHand" && enemyController.canGetHit)
            {
                if(!enemyController.isDead)
                {
                    enemyController.health = enemyController.health - 10;
                }
                //GetComponentInParent<Animator>().SetBool("head",true);
                GetComponentInParent<Animator>().Play("Hit To Body",-1,0f);
                GetComponentInParent<Animator>().SetBool("attack",false);
                GetComponentInParent<Animator>().SetBool("shortAttack",false);
                Debug.Log("HeadShot");
            }
          
    }
}
