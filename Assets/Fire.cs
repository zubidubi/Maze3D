using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	// Use this for initialization
    public float time = 1.72f;
	void Start () {
        InvokeRepeating("Switch", 0, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool firing;
    void Switch()
    {
        firing = !firing;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).particleSystem.enableEmission = firing;
            this.collider.enabled = firing;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "character")
            return;
        Application.LoadLevel("loseGame");
    }
}
