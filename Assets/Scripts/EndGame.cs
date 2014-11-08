using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    GameObject.FindGameObjectWithTag("timeElapsed").guiText.text = GameManager.ElapsedTime.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
