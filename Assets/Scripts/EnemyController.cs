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

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        GetHit();
    }
    private void GetHit()
    {
         if(enemyAnim.GetBool("head") == true)
        {
            head.SetActive(false);
            body.SetActive(false);
        }
        else if(enemyAnim.GetBool("body") == true)
        {
            
            head.SetActive(false);
            body.SetActive(false);
        }
        else
        {
            head.SetActive(true);
            body.SetActive(true);
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
