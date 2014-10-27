using UnityEngine;
using System.Collections;

public class KeyManager : MonoBehaviour {

    public int KeysToWin;
    private int actualKeys;
	// Use this for initialization
	void Start () {
        actualKeys = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void keyFound()
    {
        actualKeys++; Debug.Log(actualKeys);
    }

    internal void tryWin()
    {
        if (actualKeys >= KeysToWin)
            Application.LoadLevel("winGame");
        else
            Debug.Log("Faltan " + (KeysToWin - actualKeys) + " llaves nobato."); //mensaje de que faltan keys
    }
}
