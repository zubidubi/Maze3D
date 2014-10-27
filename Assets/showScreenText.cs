using UnityEngine;
using System.Collections;

public class showScreenText : MonoBehaviour {

    ScreenTextManager stm;
    public string text;
	// Use this for initialization
	void Start () 
    {
        stm = GameObject.FindGameObjectWithTag("screenText").GetComponent<ScreenTextManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
            stm.changeText(text);
    }
}
