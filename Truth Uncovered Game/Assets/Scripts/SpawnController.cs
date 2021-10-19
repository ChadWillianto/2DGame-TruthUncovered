using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject locations;
    private float zVal = -6;

    // Start is called before the first frame update
    void Start() {
        
        int waypointIndex = Random.Range(0,3);
        Debug.Log("Waypoint Index" + waypointIndex);
        Vector3 targetPos = new Vector3(locations.transform.GetChild(waypointIndex).transform.position.x, 
        locations.transform.GetChild(waypointIndex).transform.position.y, 
        zVal);

        this.transform.position = targetPos;
        Debug.Log("Object moved to waypoint: " + waypointIndex);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player")){
            //If player collides with the trigger then destroy game object

            Debug.Log("Object Destroyed");
            Destroy(this.gameObject);
            }
}
}
