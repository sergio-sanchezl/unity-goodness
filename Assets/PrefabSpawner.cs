using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    public Object prefab;
    public Camera cam;

	// Use this for initialization
	void Start () {
        //cam = gameObject.transform.GetChild(0).GetComponent<Camera>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 farPoint = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100));
            Vector3 closePoint = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            Vector3 direction = (farPoint - closePoint).normalized;
            RaycastHit hit;
            Ray ray = new Ray(closePoint, direction);

            if(Physics.Raycast(ray, out hit))
            {
                GameObject spawnedObject = Instantiate(prefab) as GameObject;
                spawnedObject.transform.position = hit.point + new Vector3(0,0.60f,0);

            }
        }
	}
}
