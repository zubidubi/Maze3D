using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

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
        other.gameObject.GetComponent<ResourceManager>().keyFound();
        GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundManagerScript>().keyPickUp();
        Destroy(this.gameObject);
        
    }
}
