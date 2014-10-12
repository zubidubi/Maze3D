using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Maze : MonoBehaviour {

    //Componente de las celdas a generar
    public MazeCell cellPrefab;

    //Matriz bidimensional que contiene las celdas
    private MazeCell[,] cells;

    //Delay para visualizar en cámara lenta la generación del mapa
    public float generationStepDelay;

    public IntVector2 size;

    public MazePassage passagePrefab;

    public MazeWall wallPrefab;

    private MazePos mazePos;

    List<Vector2> erasedWalls;
    List<Vector2> cellsWithElement;
    Maze(MazePos mazePos)
    {
        erasedWalls = new List<Vector2>();
        cellsWithElement = new List<Vector2>();
        this.mazePos = mazePos;
    }
    Maze()
    {
        erasedWalls = new List<Vector2>();
        cellsWithElement = new List<Vector2>();
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Generate(MazePos pos)
    {
        mazePos = pos;
        //WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            //yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        if (mazePos == MazePos.Floor)
            (newCell.transform.GetChild(0).GetComponent<WallScript>()).normal = new Vector3(0, 1, 0);
        else
        {
            (newCell.transform.GetChild(0).GetComponent<WallScript>()).normal = new Vector3(0, -1, 0);
        }
            cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;

        newCell.transform.localPosition =
        new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);

        return newCell;
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
    internal void borrarParedCentralFija()
    {
        eraseWall(0, (int)Math.Round(((double)size.z / 2.0)));
        
    }

    private void eraseWall(int x, int y)
    {

        MazeDirection direccionABuscar;
        if (x == 0 )
            direccionABuscar = MazeDirection.West;
        else if (x == size.x-1)
            direccionABuscar = MazeDirection.East;
        else if (y==0)
            direccionABuscar = MazeDirection.South;
        else //if (y == size.z -1)
            direccionABuscar = MazeDirection.North;

        foreach (Transform child in cells[x, y].transform)
        {
            var mazeWall = child.gameObject.GetComponent<MazeWall>();
            if (mazeWall == null)
                continue;

            if (mazeWall.direction == direccionABuscar)
            {
                var pared = mazeWall.transform.gameObject;
                transform.parent = null;
                Destroy(pared);
            }
        }

        erasedWalls.Add(new Vector2(x, y));
    }
    
    internal void borrarParedAleatoria()
    {
        System.Random rnd = new System.Random();
        int valorAFijar = rnd.Next(1, 4);//rnd.Next(0, 4);
        int x = 0;
        int y = 0;
        do
        {
            x = rnd.Next(0, size.x);
            y = rnd.Next(0, size.z);
        }
        while(erasedWalls.FindAll(pared => pared.x == x && pared.y == y).Count != 0);

        switch (valorAFijar)
        {
            case 0:
                x = 0;
                break;
            case 1:
                x= size.x-1;
                break;
            case 2:
                y =0;
                break;
            case 3:
            default:
                y=size.z-1;
                break;
        }
        eraseWall(x, y);
    }

    internal void ponerElementoEnLugarAleatorio(GameObject gameObject)
    {
        System.Random rnd = new System.Random();
        int x = 0;
        int z = 0;
        do
        {
            x = rnd.Next(0, size.x);
            z = rnd.Next(0, size.z);
        }
        while (cellsWithElement.FindAll(cell => cell.x == x && cell.y == z).Count != 0);
        Debug.Log("valores " + x + "  " + z);
        gameObject.transform.position = cells[x, z].transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f * (int)mazePos, gameObject.transform.position.z);
        cellsWithElement.Add(new Vector2(x, z));
    }
}
