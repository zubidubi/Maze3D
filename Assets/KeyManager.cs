using UnityEngine;
using System.Collections;

public class KeyManager : MonoBehaviour {

    public int KeysToWin { get; set; }
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
}
