using UnityEngine;
using System.Collections;

public class FallPinchoScript : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        ((SoundManagerScript)(GameObject.FindGameObjectWithTag("soundManager").GetComponent("SoundManagerScript"))).fallScream();

        Destroy(collider);
    }
}
