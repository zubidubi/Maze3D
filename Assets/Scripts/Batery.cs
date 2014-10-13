using UnityEngine;
using System.Collections;

public class Batery : MonoBehaviour {

    public int bateryEnergyRecharge;
    public AudioClip batteryPickUpClip;
    void OnTriggerEnter(Collider other)
    {
        ((LightBehavior)(GameObject.FindGameObjectWithTag("Lantern").GetComponent("LightBehavior"))).bateryEnergy += bateryEnergyRecharge;
        Destroy(this.gameObject);
        audio.PlayOneShot(batteryPickUpClip);
    }
}
