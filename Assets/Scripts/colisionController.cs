using UnityEngine;
using System.Collections;
using System;

public class colisionController : MonoBehaviour {
    private GameObject CPrefab;
    private GameObject character;
	// Use this for initialization
	void Start () {
	    CPrefab = GameObject.FindGameObjectWithTag("MainCamera");
        character = GameObject.FindGameObjectWithTag("character");
	}
	// Destroy everything that enters the trigger

    private Vector3 targetAngles;
    private DateTime lastGravityChange = DateTime.Now;
    private Quaternion? rotateTo;
    private Vector3 lastNormal = Vector3.back;
	void OnTriggerEnter(Collider other)
    {
        double secondsRefresh = 2;
        /*
        // Código para caminar en paredes
        if (other.tag == "walkableSurface")
        {
         Debug.Log(((WallScript)other.gameObject.GetComponent<WallScript>()).normal);
            //changeGravity(other.normal);
            Debug.Log("trigereo");
            //rotateCharacter(other);
            GameObject.FindGameObjectWithTag("character").transform.rotation *= Quaternion.Euler(180, 180, 0);
        }
        */

        /*
        if (lastGravityChange.AddSeconds(secondsRefresh) < DateTime.Now)
        {*/
        if (other.tag == "walkableSurface")
        {
            WallScript wallScript = other.gameObject.GetComponent<WallScript>();
            if (wallScript.normal != lastNormal)
            {
                if (wallScript.normal.x == 0 && wallScript.normal.y == -1 && wallScript.normal.z == 0)
                    rotateTo = Quaternion.Euler(180, 180, 0);
                //character.transform.rotation = Quaternion.Lerp(character.transform.rotation, , Time.time * 0.01f );

                else if (wallScript.normal.x == 0 && wallScript.normal.y == 1 && wallScript.normal.z == 0)
                    rotateTo = Quaternion.Euler(0, 0, 0);
                //character.transform.rotation = Quaternion.Lerp(character.transform.rotation, Quaternion.Euler(0, 0, 0), Time.time * 0.01f);
                originalRotation = this.transform.rotation;

                lastNormal = wallScript.normal;
            }
        }
	}
    /*
    private void changeGravity(Collider other)
    {
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        throw new System.NotImplementedException();
    }*/

    private Quaternion originalRotation;
	// Update is called once per frame
	void Update () {
        if (rotateTo == null)
            return;
        character.transform.rotation = Quaternion.Lerp(this.transform.rotation, (Quaternion)rotateTo, Time.deltaTime * 5f);

        float difference = Quaternion.Angle(character.transform.rotation, rotateTo.Value);
        if (difference < 5)
        {
            character.transform.rotation = rotateTo.Value;
            rotateTo = null;
        }
        
	}

}
