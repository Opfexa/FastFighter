using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitScript : MonoBehaviour
{
    [SerializeField] GameObject head;
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
        if(gameObject.name == "Player")
        {

        }
        else
        {
            if(other.tag == "PlayerFoot")
            {
                if(!enemyController.isDead)
                {
                    enemyController.health = enemyController.health - 10;
                }
                GetComponentInParent<Animator>().SetBool("body",true);
                Debug.Log("BodyShot");
            }
        }    
    }
    
}
