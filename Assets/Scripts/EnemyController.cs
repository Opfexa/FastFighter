using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnim;
    private Rigidbody enemyRb;
    public GameObject head;
    public GameObject body;
    public bool isDead;
    public int health;
    public bool canGetHit;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
        canGetHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        if(!isDead)
        {
            canGetHit = true;
        }
        else if(isDead)
        {
            canGetHit = false;
        }
    }
    private void Death()
    {
        if(health == 0 || health < 0)
        {
            isDead = true;
            enemyAnim.SetBool("death",true);
            GetComponent<BoxCollider>().enabled = false;
            enemyRb.useGravity = false;
        }
        else
        isDead = false;
    }
    
}
