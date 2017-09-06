using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltScript : MonoBehaviour {

    public float speed;

    private float textureOffset = 0f;

    private Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        textureOffset += Time.deltaTime * speed;
        rend.material.mainTextureOffset = new Vector2(textureOffset, 0);
	}

    void OnTriggerStay(Collider other)
    {
        // if the object that enters has a character controller or 
        // has a rigidbody (But not a kinematic one), then move it.
        if((other.gameObject.GetComponent<CharacterController>() != null) || 
            ((other.gameObject.GetComponent<Rigidbody>() != null) && !other.attachedRigidbody.isKinematic)) {
            other.transform.Translate(transform.right * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
