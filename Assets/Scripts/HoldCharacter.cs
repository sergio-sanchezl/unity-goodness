using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour {
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "CharacterHolder")
        {
            gameObject.transform.parent = col.transform;
            Debug.Log(gameObject.transform.parent + " " + col.transform + " " + "Enter");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "CharacterHolder")
        {
            gameObject.transform.parent = null;
            Debug.Log(gameObject.transform.parent + " " + col.transform + " " + "Exit");
        }
    }
}
