using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectsPlayerScript : MonoBehaviour {

    public float pushPower = 2.0f;
    public float weight = 6.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        Vector3 force;
        if (body == null || body.isKinematic)
            return;
        if(hit.moveDirection.y < -0.3)
        {
            force = new Vector3(0,-0.5f,0) * 9.8f * weight;
        } else
        {
            force = hit.controller.velocity * pushPower;
        }

        body.AddForceAtPosition(force, hit.point);

    }
}
