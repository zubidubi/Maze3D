using UnityEngine;
using System.Collections;

public class DemonBehabior : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("demonRawr", 1, 10);	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void demonRawr()
    {
        animation.Play("Rawr");
        audio.Play();
        animation.PlayQueued("Idle");
    }
}
