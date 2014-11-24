using UnityEngine;
using System.Collections;

public class EnemyCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            Application.LoadLevel("loseGame");
        }
    }
}
