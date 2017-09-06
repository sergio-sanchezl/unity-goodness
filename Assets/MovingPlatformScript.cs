using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
     
    public Transform initialPosition;
    public Transform finalPosition;

    public Transform platform;

    public float platformSpeed;

    private Rigidbody platformRigidbody;

    Transform destination;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        platformRigidbody = platform.GetComponent<Rigidbody>();
        SetDestination(initialPosition);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        platformRigidbody.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        // Si lo que vamos a recorrer en el siguiente frame se va a pasar de la posicion a la que queremos llegar,
        // damos la vuelta.
        if(Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == initialPosition ? finalPosition : initialPosition);
        }
	}

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }
}
    