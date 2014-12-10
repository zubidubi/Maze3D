using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public float turnSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float turnAngleY = turnSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float turnAngleX = turnSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
    transform.parent.Rotate (0, turnAngleY, 0);
    transform.parent.Rotate(-turnAngleX, 0, 0);
	}
}
