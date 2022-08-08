using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitScript : MonoBehaviour
{
    [SerializeField] GameObject head;
    // Start is called before the first frame update
    void Start()
    {

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
                head.SetActive(false);
                GetComponentInParent<Animator>().SetBool("body",true);
                Debug.Log("BodyShot");
            }
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if(gameObject.name == "Player")
        {

        }
        else
        {
            if(other.tag == "PlayerFoot")
            {
                head.SetActive(true);
            }
        }    
    }
}
