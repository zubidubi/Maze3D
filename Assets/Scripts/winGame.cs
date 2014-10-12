using UnityEngine;
using System.Collections;

public class winGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter(Collider collider)
    {
        Application.LoadLevel("winGame");

    }
	// Update is called once per frame
	void Update () {
	
	}
}
