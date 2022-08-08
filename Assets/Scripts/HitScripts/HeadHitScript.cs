using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitScript : MonoBehaviour
{
    private PlayerController player;
    private EnemyController enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        enemy = GetComponent<EnemyController>();
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
                GetComponentInParent<Animator>().SetBool("head",true);
            }
        }    
    }
    
}
