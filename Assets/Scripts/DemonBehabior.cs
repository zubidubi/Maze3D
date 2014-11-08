using UnityEngine;
using System.Collections;

public class DemonBehabior : MonoBehaviour {

	// Use this for initialization
    bool win;
	void Start () 
    {
        InvokeRepeating("demonRawr", 1, 10);	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void demonRawr()
    {
        if (win)
        {
            animation.Play("Jump");
            audio.Play();

            animation.PlayQueued("Rawr");

            animation.PlayQueued("Death");

        }
        else
        {
            animation.Play("Attack 2");
            audio.Play();

            animation.PlayQueued("Attack 1");
            animation.PlayQueued("Attack 2");
            animation.PlayQueued("Rawr");
        }
    }
}
