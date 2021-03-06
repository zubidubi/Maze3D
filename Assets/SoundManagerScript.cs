﻿using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {

    public AudioClip FallScream;
    public AudioClip BatteryPickUp;
    public AudioClip KeyPickUp;
    public AudioClip throwPostItSound;

    public void fallScream()
    {
        audio.PlayOneShot(FallScream);
    }

    public void batteryPickUp()
    {
        audio.PlayOneShot(BatteryPickUp);
    }

    public void keyPickUp()
    {
        audio.PlayOneShot(KeyPickUp);
    }

    public void throwPostIt()
    {
        audio.PlayOneShot(throwPostItSound);
    }
}
