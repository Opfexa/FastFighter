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
    }
}
