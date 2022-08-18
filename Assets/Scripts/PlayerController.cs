using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    //Movement
    private float horizontal;
    private float vertical;
    [SerializeField] bool lookRight;
    private bool isOnground;
    public bool canAnimation;
    //Death
    public bool isDead { get; set; }
    public int health;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject hips;
    //Attack
    public int combo { get; set; }
    public bool block { get; set; }
    public bool kick { get; set; }
    public bool punch { get; set; }
    public int pCombo { get; set; }
    [SerializeField] List<GameObject> foot;
    [SerializeField] List<GameObject> hands;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
        lookRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Get input horizontal or vertical
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Movement();
        FightMode();
        Death();
        //Player idle, run, jump, kick reset animation trigger or bool
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            combo = 0;
            playerAnimator.SetBool("combo",false);
            playerAnimator.SetBool("pCombo",false);
            canAnimation = true;
            isOnground = true;
            playerAnimator.applyRootMotion = true;
            punch = false;
            kick = false;
        }
        else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
            canAnimation = true;
            playerAnimator.ResetTrigger("punch");
        }
        else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            canAnimation = false;
            playerAnimator.ResetTrigger("punch");
            playerAnimator.ResetTrigger("kick");

        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flying Kick") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flip Kick"))
        {
            canAnimation = false;
            playerAnimator.ResetTrigger("punch");
            playerAnimator.ResetTrigger("kick");
        }
    }
    //Players Movement
    private void Movement()
    {
        //Run and turn face
        if(horizontal > 0)
        {
            if(lookRight == false)
            {
                transform.localEulerAngles = new Vector3(0,0,0);
            }
            lookRight= true;
            playerAnimator.SetBool("Running",true);
        }
        else if(horizontal < 0)
        {
            if(lookRight)
            {
                transform.localEulerAngles = new Vector3 (0,-180,0);
            }
            lookRight = false;
            playerAnimator.SetBool("Running",true);
        }
        if(horizontal == 0)
        {
            playerAnimator.SetBool("Running",false);
        }
        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && canAnimation && isOnground)
        {
            playerAnimator.SetBool("Jump",true);
            isOnground =false;
            playerAnimator.SetBool("onGround",false);
            
        }
        //Getting input for kick
        if(vertical > 0)
        {
            playerAnimator.SetInteger("vertical",1);
        }
        else if(vertical < 0)
        {
            playerAnimator.SetInteger("vertical",-1);
        }
        else if(vertical == 0)
        {
            playerAnimator.SetInteger("vertical",0);
        }
        
    }
    private void FightMode()
    {
        //Block
        if(Input.GetKey(KeyCode.LeftShift) == true)
        {
            block = true;
            playerAnimator.SetBool("Fighting",true);
        }
        else
        {
            playerAnimator.SetBool("Fighting",false);
        }
        //Kick
        if(Input.GetKeyDown(KeyCode.K))
        {
            combo++;
            kick = true;
            playerAnimator.SetTrigger("kick");
        }
        //Kick v2
        if(Input.GetKeyDown(KeyCode.J) && canAnimation)
        {
            playerAnimator.SetTrigger("slowKick");
        }
        //Punch
        if(Input.GetKeyDown(KeyCode.L))
        {
            pCombo ++;
            punch = true;
            playerAnimator.SetTrigger("punch");
        }
        
    }
    //Detection enemy and Ground
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerAnimator.SetBool("touchEnemy",true);
        }
        if(other.gameObject.tag == "Ground")
        {
            isOnground = true;
            playerAnimator.SetBool("onGround",true);
            playerAnimator.applyRootMotion = true;
        }
    }
    //Detection enemy touch
    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerAnimator.SetBool("touchEnemy",true);
        }
        else
        playerAnimator.SetBool("touchEnemy",false);
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerAnimator.SetBool("touchEnemy",false);
        }
    }
    //Kick collider open
    private void KickEvent()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("RKick"))
        foot[1].GetComponent<BoxCollider>().enabled = true;
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LKick"))
        foot[0].GetComponent<BoxCollider>().enabled = true;
    }
    //Kick collider close
    private void KickEnd()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("RKick"))
        foot[1].GetComponent<BoxCollider>().enabled = false;
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LKick"))
        foot[0].GetComponent<BoxCollider>().enabled = false;
    }
    //Jump
    private void Jump()
    {
        playerAnimator.applyRootMotion = false;
        playerRigid.AddForce(new Vector3(0,1,horizontal) * jumpForce);
    }
    //Punch collider open
    private void PunchEvent()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("RPunch"))
        hands[1].GetComponent<BoxCollider>().enabled = true;
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LPunch"))
        hands[0].GetComponent<BoxCollider>().enabled = true;

    }
    //Punch collider close
    private void PunchEnd()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("RPunch"))
        hands[1].GetComponent<BoxCollider>().enabled = false;
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LPunch"))
        hands[0].GetComponent<BoxCollider>().enabled = false;
    }
    //Check death
    private void Death()
    {
        if(health == 0 || health < 0)
        {
            isDead = true;
        }
        else
        isDead = false;
    }
}
