using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatformScript : MonoBehaviour {

	public float jumpMultiplier = 2f;
	// Use this for initialization
	void Start () {
	//	Color color = new Color (100+(50*jumpMultiplier),100,100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float getJumpMultiplier() {
		return jumpMultiplier;
	}
}
