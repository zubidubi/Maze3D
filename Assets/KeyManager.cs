using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {

    public int KeysToWin;
    private int actualKeys;
    private Text keyCountUI;
    ScreenTextManager stm;
	// Use this for initialization
	void Start () 
    {
        actualKeys = 0;
        keyCountUI = GameObject.FindGameObjectWithTag("keyCount").GetComponent<Text>();
        stm = GameObject.FindGameObjectWithTag("screenText").GetComponent<ScreenTextManager>();
        keyCountUI.text = actualKeys + "/" + KeysToWin;
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void keyFound()
    {
        actualKeys++;
        stm.changeText("You found a key");
        keyCountUI.text = actualKeys + "/" + KeysToWin;
    }

    internal void tryWin()
    {
        if (actualKeys >= KeysToWin)
            Application.LoadLevel("winGame");
        else
            stm.changeText("You need " + (KeysToWin - actualKeys) + " keys more, fool."); //mensaje de que faltan keys
    }
}
