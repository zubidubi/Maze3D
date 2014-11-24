using UnityEngine;
using System.Collections;

public class AI : Pathfinding {

    public Transform player;
    public float maxRangePatrol;
    private Transform currentTarget;
    private CharacterController controller;
    private bool newPath = true;
    private bool moving = false;
    private GameObject[] AIList;
    public float tamanoPaso = 0.5f;
    public float distanceToStop = 0.4f;
    public string idleAnimation, moveAnimation;
    public bool patrol;
    public float distanceOfDetection;


	void Start () 
    {
        AIList = GameObject.FindGameObjectsWithTag("Enemy");
        this.animation.Play(idleAnimation);
        
	}
	
	void Update () 
    {
        if (Vector3.Distance(player.position, transform.position) < 25F && !moving)
        {
            if (newPath)
            {
                StartCoroutine(NewPath());
            }
            moving = true;
        }
        else if (Vector3.Distance(player.position, transform.position) >= distanceOfDetection)
        {
            if (patrol)
                currentTarget.position = new Vector3(transform.position.x + Random.Range(-maxRangePatrol, maxRangePatrol), transform.position.y, transform.position.z + Random.Range(-maxRangePatrol, maxRangePatrol));

        }
        else if (Vector3.Distance(player.position, transform.position) < distanceToStop)
        {
            //Stop!
        }
        else if (Vector3.Distance(player.position, transform.position) < 35F && moving)
        {
            if (Path.Count > 0)
            {
                if (Vector3.Distance(player.position, Path[Path.Count - 1]) > 5F)
                {
                    StartCoroutine(NewPath());
                }
            }
            else
            {
                if (newPath)
                {
                    StartCoroutine(NewPath());
                }
            }
            //Move the ai towards the player
            MoveMethod();
        }
        else
        {
            moving = false;
        }
	}

    IEnumerator NewPath()
    {
        newPath = false;
        FindPath(transform.position, player.position);
        yield return new WaitForSeconds(1F);
        newPath = true;
    }


    private void MoveMethod()
    {
        if (Path.Count > 0)
        {
            Vector3 direction = (Path[0] - transform.position).normalized;

            foreach (GameObject g in AIList)
            {
                if(Vector3.Distance(g.transform.position, transform.position) < 1F)
                {
                    Vector3 dir = (transform.position - g.transform.position).normalized;
                    dir.Set(dir.x, 0, dir.z);
                    direction += 0.2F * dir;
                }
            }

            direction.Normalize();

            this.animation.Play(moveAnimation);
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * tamanoPaso);
            if (lastPosition != null)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, transform.position - lastPosition, 100, 0.0f));
                
            }
            lastPosition = transform.position;

            if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
            {
                Path.RemoveAt(0);
            }

            RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);
            float maxY = -Mathf.Infinity;
            foreach (RaycastHit h in hit)
            {
                if (h.transform.tag == "Untagged")
                {
                    if (maxY < h.point.y)
                    {
                        maxY = h.point.y;
                    }
                }
            }
            if (maxY > -100)
            {
                
                transform.position = new Vector3(transform.position.x, maxY + 1F, transform.position.z);
            }
        }
    }
    private Vector3 lastPosition;
}
