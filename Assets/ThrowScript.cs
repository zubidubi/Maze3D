using UnityEngine;
using System.Collections;

public class ThrowScript : MonoBehaviour {
    
    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pTarget")
        {
            rigidbody.isKinematic = true;

        }
    }*/
    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "pTarget")
        {
            Debug.LogError(collision.collider.tag);
            rigidbody.isKinematic = true;
        }
    }
}
