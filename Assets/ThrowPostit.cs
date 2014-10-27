using UnityEngine;
using System.Collections;

public class ThrowPostit : MonoBehaviour {

    public Rigidbody PostItPrefab;
    public float speed = 30.0f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Rigidbody instantiatedThrowable = Instantiate(PostItPrefab, transform.position, transform.rotation) as Rigidbody;
            instantiatedThrowable.velocity = transform.forward * speed;
            //instantiatedThrowable.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
      
        }
    }

}
