using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnim;
    private Rigidbody enemyRb;
    public GameObject head;
    public GameObject body;
    public bool isDead;
    public int health;
    public bool canGetHit;
    public bool isMoving;
    [SerializeField] float damageBounce;
    [SerializeField] List<GameObject> enemyFoot;
    [SerializeField] List<GameObject> enemyHand;
    public Transform player;
    public LayerMask whatIsPlayer, whatIsGround;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private float randomNum;
    //Attacking
    public float attackCountDown;
    public bool canAttack;
    //States
    public float sightRange, attackRange, closeAttackRange;
    public bool playerInSightRange , playerInAttackRange, playerInCloseAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
        canGetHit = true;
        canAttack = true;
    }
    private void Awake() 
    {
        player = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        
        Death();
        if(!isDead)
        {
            canGetHit = true;
            playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);
            playerInCloseAttackRange = Physics.CheckSphere(transform.position, closeAttackRange, whatIsPlayer);
            
            if(!playerInSightRange && !playerInAttackRange) Patroling();
            if(playerInSightRange && !playerInAttackRange) ChasePlayer();
            if(playerInAttackRange && playerInSightRange && !playerInCloseAttackRange) Attacking();
            if(playerInCloseAttackRange && playerInSightRange) CloseAttack();
            if(isMoving)
            enemyAnim.SetBool("walk",true);
            else
            enemyAnim.SetBool("walk",false);
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
            for (int i = 0; i < 2; i++)
            {
                enemyFoot[i].GetComponent<BoxCollider>().enabled = false;
                enemyHand[i].GetComponent<BoxCollider>().enabled = false;
            }
            enemyAnim.SetBool("death",true);
            GetComponent<BoxCollider>().enabled = false;
            enemyRb.useGravity = false;
        }
        else
        isDead = false;
    }
    private void Patroling()
    {
        if(walkPointSet) SearchWalkPoint();
        if(walkPointSet)
        {
            isMoving = true;
        }
        Vector3 distenceWalkPoint = transform.position - walkPoint;
        
        if(distenceWalkPoint.magnitude < 1f)
        walkPointSet = false;
        
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        walkPointSet = true;
    }
    private void Attacking()
    {
        transform.LookAt(player);
        isMoving = false;
        if(canAttack ==  true)
        {
            enemyAnim.SetBool("attack",true);
            //enemyAnim.SetBool("shortAttack",true);
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCountDown);
        }
    }
    private void ChasePlayer()
    {
        isMoving = true;
        transform.LookAt(player);
    }
    
    private void ResetAttack()
    {
        canAttack = true;
        //enemyAnim.SetBool("shortAttack",false);
    }
    private void CloseAttack()
    {
        isMoving = false;
        if(canAttack ==  true)
        {
            enemyAnim.SetBool("shortAttack",true);
            //enemyAnim.SetBool("shortAttack",true);
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCountDown);
        }
    }
    private void KickEvent()
    {
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Roundhouse Kick"))
        {
            enemyFoot[1].GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void KickEnd()
    {
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Roundhouse Kick"))
        {
            enemyFoot[1].GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void PunchEvent()
    {
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("RPunching"))
        {
            enemyHand[1].GetComponent<BoxCollider>().enabled = true;
        }
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("LPunching"))
        {
            enemyHand[0].GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void PunchEnd()
    {
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("RPunching"))
        {
            enemyHand[1].GetComponent<BoxCollider>().enabled = false;
        }
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("LPunching"))
        {
            enemyHand[0].GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void TakeDamage()
    {
        transform.Translate(new Vector3(0,0,damageBounce)*Time.deltaTime);
    }
}
