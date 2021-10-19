using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairController : MonoBehaviour
{
    public GameObject Waypoint;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        var targetPos = Waypoint.transform.position;
        if(other.CompareTag("Player")){
            //adavnce to next "floor"
            player.transform.position = targetPos;
    }
    }
}
