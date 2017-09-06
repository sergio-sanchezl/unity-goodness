using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingDebrisScript : MonoBehaviour {

    public float secondsToStartDisappearing = 5f;
    public float durationOfDesintegration = 2f;

    private float timeSpent;

    private Vector3 initialScale;

    private bool desintegrating = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(ScheduleDesintegration(secondsToStartDisappearing));
        initialScale = this.gameObject.transform.localScale;
        timeSpent = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(desintegrating)
        {
            timeSpent += Time.deltaTime;
            float interpolant = Mathf.InverseLerp(0, durationOfDesintegration, timeSpent);
            this.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, interpolant);
            if(this.transform.localScale == Vector3.zero)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
	}

    IEnumerator ScheduleDesintegration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        desintegrating = true;
    }


}
