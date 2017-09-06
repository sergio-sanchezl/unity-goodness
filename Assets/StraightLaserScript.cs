using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLaserScript : MonoBehaviour {

    private LineRenderer lr;
    public GameObject bulletSpawner;
    public GameObject movingLight;

	// Use this for initialization
	void Start () {
        lr = bulletSpawner.GetComponent<LineRenderer>();
        lr.numPositions = 2;
        lr.SetPosition(0, bulletSpawner.transform.position);
        lr.SetPosition(1, bulletSpawner.transform.position);
        lr.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(bulletSpawner.transform.position,bulletSpawner.transform.right, out hit, 100, ~(1 << 8)))
        {
            movingLight.SetActive(true);
            lr.SetPosition(0, bulletSpawner.transform.position);
            lr.SetPosition(1, hit.point);
            
            // Vector that represents the direction of the actual line between the bullet spawner and the hit on the surface.
            Vector3 spawnToHitVectorDirection = Vector3.Normalize(hit.point - bulletSpawner.transform.position);
            
            movingLight.transform.position = hit.point - (spawnToHitVectorDirection * 0.15f);
            //movingLight.transform.rotation = bulletSpawner.transform.rotation;

            //Debug.DrawLine(bulletSpawner.transform.position, hit.point, Color.blue);

        } else
        {
            lr.SetPosition(0, bulletSpawner.transform.position);
            lr.SetPosition(1, bulletSpawner.transform.position + (bulletSpawner.transform.right * 100));
            movingLight.SetActive(false);
            //movingLight.transform.position = (bulletSpawner.transform.right * 100);
        }
	}
}
