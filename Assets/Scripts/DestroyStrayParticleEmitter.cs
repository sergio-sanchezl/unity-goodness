﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStrayParticleEmitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent == null)
        {
            Destroy(gameObject);
        }
	}

    void DestroyEmitter()
    {
        
    }


}
