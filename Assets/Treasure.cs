﻿using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "character")
            return;
        other.gameObject.GetComponent<ResourceManager>().tryWin();
    }
}
