using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitScript : MonoBehaviour
{
    [SerializeField] GameObject head;
    private EnemyController enemyController;
    
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(gameObject.GetComponentInParent<PlayerController>().gameObject.name == "Player")
        {
            
            if(other.tag == "EnemyFoot" || other.tag == "EnemyHand")
            {
                if(!playerController.isDead)
                {
                    playerController.health = playerController.health - 25;
                }
                GetComponentInParent<Animator>().SetBool("body",true);
                GetComponentInParent<Animator>().SetBool("Fighting",true);
            }
        }
           
    }
    
}
