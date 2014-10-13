using UnityEngine;
using System.Collections;

public class PinchoScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Invoke("looseGame", 3);
        //Application.LoadLevel("loseGame");
    }

    public void looseGame()
    {
        Application.LoadLevel("loseGame");
    }

}
