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
        Debug.Log("llave tocada");
        if (other.gameObject.tag != "character")
            return;
        other.gameObject.GetComponent<KeyManager>().keyFound();
        GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundManagerScript>().keyPickUp();
        Destroy(this.gameObject);
        
    }
}
