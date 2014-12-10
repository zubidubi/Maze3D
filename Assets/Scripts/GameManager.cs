using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Timers;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    public int BatterysPerMaze;
    public int KeysPerMaze;
    public int DemonsPerMaze;
    public int FiresPerMaze;
    private List<Maze> mazeInstancesFloor;
    private Maze mazeInstanceCeil;
    const int MINUTES = 5;
    public int seconds = 60 * MINUTES;
    public static TimeSpan ElapsedTime { get { return elapsedTime; } }
    private static TimeSpan elapsedTime;
    Text textMesh;
    public bool showCursor;
    public GameObject Battery;
    public GameObject Key;
    public GameObject Demon;
    public GameObject Fire;
    public GameObject Treasure;
    private void Start()
    {
        Screen.showCursor = showCursor;
        mazeInstancesFloor = new List<Maze>();
        textMesh = GameObject.FindGameObjectWithTag("timer").GetComponent<Text>();
        textMesh.text = seconds.ToString();
        elapsedTime = new TimeSpan();
        BeginGame();
        Pathfinder.Instance.StartNow();
        InvokeRepeating("Countdown", 1.0f, 1.0f);
    }
    private void Countdown()
    {
        if (--seconds == 0) CancelInvoke("Countdown");
        textMesh.text = seconds.ToString();
        elapsedTime = elapsedTime.Add(new TimeSpan(0, 0, 1));
    }
    private void Update()
    {
        if (seconds == 0)
        {
            Application.LoadLevel("loseGame");
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
        mazeInstancesFloor[0].borrarParedAleatoria(MazeDirection.North);
        mazeInstancesFloor[0].borrarParedAleatoria(MazeDirection.South);

        GameObject entrance = GameObject.FindGameObjectWithTag("entrance");
        //entrance.transform.position = new Vector3(- 1.842f - mazeInstancesFloor[0].size.x/2, entrance.transform.position.y, entrance.transform.position.z);
        float zPos, xPos;
        if (mazeInstancesFloor[0].size.z % 2 == 0)
        {
            zPos = 1f;
            xPos = -1.8f -mazeInstancesFloor[0].size.x/2;
        }
        else
        {
            zPos = 0.5f;
            xPos = -2.3f -mazeInstancesFloor[0].size.x/2;
        }
        entrance.transform.position = new Vector3(xPos, entrance.transform.position.y, zPos);
        
        float largoPasillo = 6;
        float desp = largoPasillo + mazeInstancesFloor[0].size.x;

        mazeInstancesFloor[1].transform.position = new Vector3(mazeInstancesFloor[1].transform.position.x + desp, mazeInstancesFloor[1].transform.position.y, mazeInstancesFloor[1].transform.position.z);
        mazeInstancesFloor[1].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[1], MazeDirection.East);
        mazeInstancesFloor[2].transform.position = new Vector3(mazeInstancesFloor[2].transform.position.x, mazeInstancesFloor[2].transform.position.y, mazeInstancesFloor[2].transform.position.z + desp);
        mazeInstancesFloor[2].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[2], MazeDirection.North);
        mazeInstancesFloor[3].transform.position = new Vector3(mazeInstancesFloor[3].transform.position.x, mazeInstancesFloor[3].transform.position.y, mazeInstancesFloor[3].transform.position.z - desp);
        mazeInstancesFloor[3].borrarParedUnidaACentral(mazeInstancesFloor[0].erasedWalls[3], MazeDirection.South);
        
        //mazeInstanceCeil.transform.Rotate(transform.right, 180f);

        MiniMapScript miniMapScript = GameObject.FindGameObjectWithTag("character").GetComponent<MiniMapScript>();

        UnityEngine.Object bat = Battery; //AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Battery.prefab", typeof(GameObject));
        for (int i = 0; i < BatterysPerMaze; i++)
        {
            foreach (Maze maze in mazeInstancesFloor)
            {
                GameObject clone = Instantiate(bat) as GameObject;
                maze.ponerElementoEnLugarAleatorio(clone);

                miniMapScript.batteries.Add(clone.transform);
            }
        }
        UnityEngine.Object key = Key;// AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Key.prefab", typeof(GameObject));
        for (int i = 0; i < KeysPerMaze; i++)
        {
            foreach (Maze maze in mazeInstancesFloor)
            {
                GameObject clone = Instantiate(key) as GameObject;
                maze.ponerElementoEnLugarAleatorio(clone);
                miniMapScript.keys.Add(clone.transform);

            }
        }

        UnityEngine.Object demon = Demon;// AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Demon.prefab", typeof(GameObject));
        for (int i = 0; i < DemonsPerMaze; i++)
        {
            foreach (Maze maze in mazeInstancesFloor)
            {
                GameObject clone = Instantiate(demon) as GameObject;
                clone.gameObject.GetComponent<AI>().player = GameObject.FindGameObjectWithTag("character").transform;
                maze.ponerElementoEnLugarAleatorio(clone);

                miniMapScript.demons.Add(clone.transform);

            }
        }

        UnityEngine.Object fire = Fire;// AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire.prefab", typeof(GameObject));
        for (int i = 0; i < FiresPerMaze; i++)
        {
            foreach (Maze maze in mazeInstancesFloor)
            {
                GameObject clone = Instantiate(fire) as GameObject;
                maze.ponerElementoEnLugarAleatorio(clone);
            }
        }

        UnityEngine.Object treasure = Treasure;// AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Treasure.prefab", typeof(GameObject));
        System.Random rnd = new System.Random();
        
        // Treasure to win..
        GameObject treasureGO = Instantiate(treasure) as GameObject;
        mazeInstancesFloor[rnd.Next(0, 4)].ponerElementoEnLugarAleatorio(treasureGO);

        miniMapScript.treasure = treasureGO.transform;
        
        mazeInstancesFloor[0].crearPasillos();
    }

    private void RestartGame() 
    {
        elapsedTime = new TimeSpan();
        StopAllCoroutines();
        Destroy(mazeInstancesFloor[0].gameObject);
        //Destroy(mazeInstanceCeil.gameObject);
        BeginGame();
    }
}