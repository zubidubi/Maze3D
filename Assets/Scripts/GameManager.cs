using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;

    private Maze mazeInstanceFloor;
    private Maze mazeInstanceCeil;

    int seconds = 120;
    GUIText textMesh;
    private void Start()
    {
         
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
            Application.LoadLevel("loseGame");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void BeginGame() 
    {
        mazeInstanceFloor = Instantiate(mazePrefab) as Maze;
        mazeInstanceFloor.Generate(MazePos.Floor);

        mazeInstanceCeil = Instantiate(mazePrefab) as Maze;
        mazeInstanceCeil.Generate(MazePos.Ceil);
        //StartCoroutine(mazeInstanceCeil.Generate());

        mazeInstanceCeil.transform.Translate(new Vector3(0, 5, 0));
        mazeInstanceCeil.transform.Rotate(transform.right, 180f);

        mazeInstanceFloor.borrarParedCentralFija();
        mazeInstanceFloor.borrarParedAleatoria();
        mazeInstanceFloor.borrarParedAleatoria();
    }

    private void RestartGame() 
    {
        StopAllCoroutines();
        Destroy(mazeInstanceFloor.gameObject);
        Destroy(mazeInstanceCeil.gameObject);
        BeginGame();
    }
}