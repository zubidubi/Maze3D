using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

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

    public List<Vector2> erasedWalls;
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
    
    internal void borrarParedAleatoria(MazeDirection direccion)
    {
        System.Random rnd = new System.Random();
        int x = 0;
        int y = 0;
        do
        {
            x = rnd.Next(size.x/4, 3*size.x/4);
            y = rnd.Next(size.z/4, 3*size.z/4);
        }
        while(erasedWalls.FindAll(pared => pared.x == x && pared.y == y).Count != 0);

        switch (direccion)
        {
            // Entrada
            case MazeDirection.West:
                x = 0;
                return;
            case MazeDirection.East:
                x= size.x-1;
                break;
            case MazeDirection.South:
                y =0;
                break;
            case MazeDirection.North:
            default:
                y=size.z-1;
                break;
        }
        eraseWall(x, y);
    }

    internal void crearPasillos()
    {
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Pasillo.prefab", typeof(GameObject));
        List<GameObject> pasillos = new List<GameObject>();
        
        foreach (Vector2 coord in erasedWalls)
        {
            Debug.Log(coord);
            if (coord == erasedWalls[0])
                continue;
            MazeCell celda = cells[(int)coord.x, (int)coord.y];
            GameObject pasillo = Instantiate(prefab) as GameObject;
            MazeDirection direc;
            if (coord.x == 0)
                direc = MazeDirection.West;
            else if (coord.x == size.x-1)
                direc = MazeDirection.East;
            else if (coord.y == 0)
                direc = MazeDirection.South;
            else
                direc = MazeDirection.North;


            if (direc == MazeDirection.North)
            {
                pasillo.transform.Rotate(0, 90, 0);
                pasillo.transform.position = new Vector3(celda.transform.position.x - 0.5f, pasillo.transform.position.y, celda.transform.position.z + 0.76f);
            }
            else if (direc == MazeDirection.East)
            {
                pasillo.transform.Rotate(0, 180, 0);
                pasillo.transform.position = new Vector3(celda.transform.position.x + 0.76f, pasillo.transform.position.y, celda.transform.position.z + 0.5f);
            }
            else if (direc == MazeDirection.South)
            {
                pasillo.transform.position = new Vector3(celda.transform.position.x + 0.5f, pasillo.transform.position.y, celda.transform.position.z - 0.76f);
                pasillo.transform.Rotate(0, 270, 0);
            }
            else
                pasillo.transform.position = new Vector3(celda.transform.position.x -0.76f, pasillo.transform.position.y, celda.transform.position.z - 0.5f);
                //cells[coord.x, coord.y].transform.position
                pasillos.Add(pasillo);
        }
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
        Debug.Log("Lo puse en " + x + "  " + z);
        gameObject.transform.position = cells[x, z].transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f * (int)mazePos, gameObject.transform.position.z);
        cellsWithElement.Add(new Vector2(x, z));
    }


    internal void borrarParedUnidaACentral(Vector2 vector2, MazeDirection direction)
    {
        //case MazeDirection.North:
        if(vector2.y == 0)
            eraseWall((int)vector2.x, (int)size.z-1);
        else if(vector2.y == size.z-1)
            eraseWall((int)vector2.x, 0);
        else if (vector2.x == size.x-1)
            eraseWall(0, (int)vector2.y);
        else if (vector2.x == 0)
            eraseWall(size.x-1, (int)vector2.y);
        //
    }
}
