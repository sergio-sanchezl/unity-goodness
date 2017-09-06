using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(ScheduleDestroy(2));
	}
	
    IEnumerator ScheduleDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
