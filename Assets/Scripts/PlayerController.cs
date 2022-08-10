using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private float horizontal;
    private float vertical;
    private bool fighting;
    public bool lookRight;
    public int combo;
    public bool canAnimation;
    [SerializeField] List<GameObject> foot;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
        fighting = false;
        lookRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Movement();
        FightMode();
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            combo = 0;
            playerAnimator.SetBool("combo",false);
            canAnimation = true;
        }
        else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
            canAnimation = true;
        }
        else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            canAnimation = false;
        }
    }
    private void Movement()
    {
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
        if(Input.GetKeyDown(KeyCode.Space) && canAnimation)
        {
            playerAnimator.SetBool("Jump",true);
        }
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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            fighting = !fighting;
            if(fighting)
            playerAnimator.SetBool("Fighting",true);
            else
            playerAnimator.SetBool("Fighting",false);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            combo++;
            playerAnimator.SetTrigger("kick");
        }
        if(Input.GetKeyDown(KeyCode.J) && canAnimation)
        {
            playerAnimator.SetTrigger("slowKick");
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerAnimator.SetBool("touchEnemy",true);
        }   
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            playerAnimator.SetBool("touchEnemy",false);
        }   
    }
    private void KickEvent()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick1"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Mma Kick"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick2"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick3"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flip Kick"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flying Kick"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = true;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void KickEnd()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick1"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Mma Kick"))
        {
            foot[1].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick2"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MmaKick3"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flip Kick"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Flying Kick"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = false;
        }
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
        {
            foot[0].GetComponent<BoxCollider>().enabled = false;
        }
    }
}
