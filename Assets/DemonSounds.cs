using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonSounds : MonoBehaviour {

    public float distance = 2;
    public List<AudioClip> sounds;
    private GameObject character;

	// Use this for initialization
	void Start () {
        character = GameObject.FindGameObjectWithTag("character");
	
	}
	// Update is called once per frame
	void Update () {
        if (nearToPlayer() && !this.audio.isPlaying)
        {
            if (Random.value <= 0.05)
            {
                playNewSound();
            }
        }
	}

    private bool nearToPlayer()
    {
        if ((character.transform.position - this.transform.position).magnitude <= distance)
            return true;
        else
            return false;

    }

    private void playNewSound()
    {
        this.audio.clip = sounds[Random.Range(0, sounds.Count)];
        this.audio.Play();
    }


}
