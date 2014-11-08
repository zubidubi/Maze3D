using UnityEngine;
using System.Collections;

public class ChSoundController : MonoBehaviour {

    public AudioClip screamSound;
    public AudioClip throwPostItSound;

    public void throwPostIt()
    {
        audio.clip = throwPostItSound;
        audio.Play();
    }
    public void fallScream()
    {
        audio.clip = screamSound;
        audio.Play();
    }

}
