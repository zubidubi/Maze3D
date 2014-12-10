using UnityEngine;
using System.Collections;

public class EnemyCollide : MonoBehaviour {

	// Use this for initialization
    GameObject character;
	void Start () {
        character = GameObject.FindGameObjectWithTag("character");
	}
    bool loosing = false;
	// Update is called once per frame
	void Update () {
        if (loosing)
        {
        Quaternion newRotation = Quaternion.LookRotation(this.transform.position) * Quaternion.Euler(0, 90, 0);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, newRotation, (float)(Time.deltaTime * 3.0));
        }
        /*if (loosing)
            character.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(character.transform.forward, transform.position - character.transform.position, 10, 0.0f));
	*/
    }

    void lose()
    {
        ChController.CanUseInput = true;
        Application.LoadLevel("loseGame");
        CancelInvoke("lose");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            ChController.CanUseInput = false;
            loosing = true;
            InvokeRepeating("lose", 2, 2);
        }

        
    }
}
