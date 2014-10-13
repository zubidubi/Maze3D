using UnityEngine;
using System.Collections;

public class ChSoundController : MonoBehaviour {

    public AudioClip screamSound;

    public void fallScream()
    {
        audio.clip = screamSound;
        audio.Play();
    }

}
