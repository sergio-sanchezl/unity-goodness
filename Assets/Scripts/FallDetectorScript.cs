using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectorScript : MonoBehaviour {

	public GameObject respawn;

	private Vector3 respawnLocation;

	// Use this for initialization
	void Start () {
		respawnLocation = respawn.transform.position;	
	}

	void OnTriggerEnter(Collider col) {
        if(col.transform.tag == "Projectile")
        {
            Destroy(col.gameObject);
        } else
        {
            col.gameObject.transform.position = respawnLocation;
        }
		
	}
}
