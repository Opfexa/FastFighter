using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offsett;
    [SerializeField] GameObject player;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offsett;
    }
}
