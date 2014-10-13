using UnityEngine;
using System.Collections;

public class Batery : MonoBehaviour {

    public int bateryEnergyRecharge;

    void OnTriggerEnter(Collider other)
    {
        ((LightBehavior)(GameObject.FindGameObjectWithTag("Lantern").GetComponent("LightBehavior"))).bateryEnergy += bateryEnergyRecharge;

        ((SoundManagerScript)(GameObject.FindGameObjectWithTag("soundManager").GetComponent("SoundManagerScript"))).batteryPickUp();

        Destroy(this.gameObject);

    }
}
