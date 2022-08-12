using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitScript : MonoBehaviour
{
    [SerializeField] GameObject body;
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
            if(other.tag == "PlayerFoot"|| other.tag == "PlayerHand")
            {
                if(!enemyController.isDead)
                {
                    enemyController.health = enemyController.health - 25;
                }
                GetComponentInParent<Animator>().SetBool("head",true);
                Debug.Log("HeadShot");
            }
        }    
    }
    
    
}
