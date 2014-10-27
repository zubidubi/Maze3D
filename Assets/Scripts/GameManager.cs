using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    
    private List<Maze> mazeInstancesFloor;
    private Maze mazeInstanceCeil;

    int seconds = 120;
    GUIText textMesh;
    private void Start()
    {
        mazeInstancesFloor = new List<Maze>();
        textMesh = GameObject.FindGameObjectWithTag("timer").GetComponent<GUIText>();
        textMesh.text = seconds.ToString();
        BeginGame();

        InvokeRepeating("Countdown", 1.0f, 1.0f);
    }
    private void Countdown()
    {
        if (--seconds == 0) CancelInvoke("Countdown");
        textMesh.text = seconds.ToString();
    }
    private void Update()
    {
        if (seconds == 0)
        {
            //Application.LoadLevel("loseGame");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void BeginGame() 
    {
        for(int i=0; i<4; i++)
        { 
            mazeInstancesFloor.Add((Instantiate(mazePrefab) as Maze));
            mazeInstancesFloor[i].Generate(MazePos.Floor);
        }

        mazeInstancesFloor[0].borrarParedCentralFija();
        mazeInstancesFloor[0].borrarParedAleatoria(MazeDirection.East);
        mazeInstancesFloor[0].borrarParedAleatoria(MazeDirection.South);
        mazeInstancesFloor[0].borrarParedAleatoria(MazeDirection.North);

        GameObject entrance = GameObject.FindGameObjectWithTag("entrance");
        entrance.transform.position = new Vector3(- 1.842f - mazeInstancesFloor[0].size.x/2, entrance.transform.position.y, entrance.transform.position.z);
        
        float largoPasillo = 6;
        float desp = largoPasillo + mazeInstancesFloor[0].size.x;

        mazeInstancesFloor[1].transform.position = new Vector3(mazeInstancesFloor[1].transform.position.x + desp, mazeInstancesFloor[1].transform.position.y, mazeInstancesFloor[1].transform.position.z);
        mazeInstancesFloor[1].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[0], MazeDirection.East);
        mazeInstancesFloor[2].transform.position = new Vector3(mazeInstancesFloor[2].transform.position.x, mazeInstancesFloor[2].transform.position.y, mazeInstancesFloor[2].transform.position.z + desp);
        mazeInstancesFloor[2].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[1], MazeDirection.South);
        mazeInstancesFloor[3].transform.position = new Vector3(mazeInstancesFloor[3].transform.position.x, mazeInstancesFloor[3].transform.position.y, mazeInstancesFloor[3].transform.position.z - desp);
        mazeInstancesFloor[3].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[2], MazeDirection.North);
        
        //mazeInstanceCeil.transform.Rotate(transform.right, 180f);



        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Battery.prefab", typeof(GameObject));
        for (int i = 0; i < 10; i++)
        {
            foreach (Maze maze in mazeInstancesFloor)
            {
                GameObject clone = Instantiate(prefab) as GameObject;
                maze.ponerElementoEnLugarAleatorio(clone);
            }
        }
        for (int i = 0; i < 10; i++)
        {
            //GameObject clone = Instantiate(prefab) as GameObject;
            //mazeInstanceCeil.ponerElementoEnLugarAleatorio(clone);
        }
        mazeInstancesFloor[0].crearPasillos();
    }

    private void RestartGame() 
    {
        StopAllCoroutines();
        Destroy(mazeInstancesFloor[0].gameObject);
        //Destroy(mazeInstanceCeil.gameObject);
        BeginGame();
    }



}