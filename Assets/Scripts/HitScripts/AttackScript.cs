using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Enemy")
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
