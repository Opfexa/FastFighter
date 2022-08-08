using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitScript : MonoBehaviour
{
    [SerializeField] GameObject body;
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
                body.SetActive(false);
                GetComponentInParent<Animator>().SetBool("head",true);
                Debug.Log("HeadShot");
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
                body.SetActive(true);
            }
        }
    }
    
}
