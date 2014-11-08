using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("timeElapsed").GetComponent<Text>().text = GameManager.ElapsedTime.Minutes + ":" + GameManager.ElapsedTime.Seconds.ToString("00");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
