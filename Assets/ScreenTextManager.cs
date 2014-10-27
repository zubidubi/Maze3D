using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTextManager : MonoBehaviour {

    Text screenText;
    public float timeToFadeOut;
	// Use this for initialization
	void Start () 
    {
        screenText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeText(string text)
    {
        screenText.text = text;
        CancelInvoke("clear");
        Invoke("clear", timeToFadeOut);
    }

    public void clear()
    {
        screenText.text = "";
    }
}
