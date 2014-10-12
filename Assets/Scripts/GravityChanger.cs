using UnityEngine;
using System.Collections;
using System;

public class GravityChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    DateTime lastChange = DateTime.Now;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "character")
            return;
        if (lastChange.AddSeconds(5) < DateTime.Now)
        {
            GameObject asdf = other.gameObject;
            ((ChController)asdf.GetComponent<ChController>()).changeGravity();
            lastChange = DateTime.Now;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
