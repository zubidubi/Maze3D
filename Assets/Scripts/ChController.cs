using UnityEngine;
using System.Collections;

public class ChController : MonoBehaviour {

    public GameObject CPrefab;
    public bool CanMoveOnAir;
    public bool CanJump;
    public float JumpHeight = 1;
    private bool gravityNormal = false;
    private Vector3 groundDirection;
    private float initialHeight;
    private bool isFlying = false;

    public float Speed = 5.0f;
    public Vector3 MoveDirection = Vector3.zero;

    void Start(){
        gravityNormal = true;

	    if (CPrefab == null){
		    CPrefab = GameObject.FindGameObjectWithTag("MainCamera");
		    CPrefab.GetComponent<CameraController>().charObj = transform;
		    CPrefab.GetComponent<CameraController>().Initialize(transform);
	    }
	    else
	    {
		    CPrefab.GetComponent<CameraController>().charObj = transform;
		    CPrefab.GetComponent<CameraController>().Initialize(transform);
	    } 
    } 

    void Update () 
    {
        if (Input.GetButtonDown("Jump") && CanJump && !isFlying)
        {
            isFlying = true;
            initialHeight = transform.position.y;
            InvokeRepeating("Jump", 0f, 0.01f);
        }
        if (Input.GetButtonDown("Run"))
            this.Speed *= 2;
        if(Input.GetButtonUp("Run"))
            this.Speed /= 2;
        if (Input.GetButtonDown("Change Gravity"))
        {
            changeGravity();
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "walkableSurface") 
            isFlying = false;
    }
    private void Jump()
    {
        transform.Translate(-groundDirection * JumpHeight * 0.023f);
        this.constantForce.enabled = false;
        if (initialHeight + JumpHeight <= transform.position.y)
        {            
            CancelInvoke("Jump");
            this.constantForce.enabled = true;
        }
        /*
        for (float i = 0; i < JumpHeight; i = transform.position.y * -groundDirection.y)
        {
            transform.Translate(-groundDirection * JumpHeight * Time.deltaTime);
        }*/
    }
    public void changeGravity()
    {   
        constantForce.force = new Vector3(constantForce.force.x, -constantForce.force.y, constantForce.force.z);
    }

    private bool IsGrounded()
    {        
        if (gravityNormal)
            groundDirection = -Vector3.up;
        else
            groundDirection = Vector3.up;
        return Physics.Raycast(transform.position, groundDirection, 2);
    }

    void FixedUpdate (){
   
        if (Input.GetButton("Rotate"))
        {
            transform.Rotate(0,Input.GetAxisRaw("Rotate")*100*Time.deltaTime,0);
        } 
	    Movement();
    }

    void Movement()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            CPrefab.transform.rotation = Quaternion.Lerp(CPrefab.transform.rotation, this.transform.rotation, Time.deltaTime * 8f);
            //CPrefab.transform.rotation = this.transform.rotation;
            MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), MoveDirection.y, Input.GetAxisRaw("Vertical"));
        }
        //if (IsGrounded())
        IsGrounded();
            this.transform.Translate((MoveDirection.normalized * Speed) * Time.deltaTime);
    }
}
