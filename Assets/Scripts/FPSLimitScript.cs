﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimitScript : MonoBehaviour {

	void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
