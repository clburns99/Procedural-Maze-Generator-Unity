using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
	// display private variables in inspector (materials for the maze)
	[SerializeField] private Material mazeFloor;
	[SerializeField] private Material mazeWall;
	[SerializeField] private GameObject mazeStart;
	[SerializeField] private GameObject mazeEnd;

	private MazeData dataGenerator;
	private MazeMesh meshGenerator;

	// mazeData is a 2D array, 0 or 1, to represent open(floor) or blocked(wall)
	public int[,] mazeData 
	{
		get; private set; // Maze data can't be modified from outside this class 
	}

	// properties to store sizes and positions
	public float hallWidth 
	{
		get; private set;
	}
	public float hallHeight 
	{
		get; private set;
	}
	public int startRow 
	{
		get; private set;
	}
	public int startCol 
	{
		get; private set;
	}
	public int endRow 
	{
		get; private set;
	}
	public int endCol 
	{
		get; private set;
	}
	private void Awake() 
	{
		// instantiate it in a new variable in Awake
		dataGenerator = new MazeData();
		meshGenerator = new MazeMesh();
	}

	public void DisplayMaze() 
	{
		// creates a new game objects which will build the maze
		GameObject mazeObject = new GameObject();
		mazeObject.transform.position = Vector3.zero;

		// names the object and gives it a tag
		mazeObject.name = "Procedural Maze";
		mazeObject.tag = "Generated";

		// adds a mesh filter to the game object.
		MeshFilter meshFilter = mazeObject.AddComponent<MeshFilter>();
		meshFilter.mesh = meshGenerator.Data(mazeData);

		// adds a mesh collider to the game object.
		MeshCollider meshCollider = mazeObject.AddComponent<MeshCollider>();
		meshCollider.sharedMesh = meshFilter.mesh;

		// adds a mesh renderer to the game object.
		MeshRenderer meshRenderer = mazeObject.AddComponent<MeshRenderer>();
		// assigns the array of materials with the floor and wall material
		meshRenderer.materials = new Material[2] { mazeFloor, mazeWall };
	}

	public void GenerateNewMaze (int sizeRows, int sizeCols) 
	{
		// checks to see if the rows/columns are divisible by 2,
		if (sizeRows % 2 == 0 && sizeCols % 2 == 0) 
		{
			// because the generated maze will be surrounded by walls
			Debug.LogError("Odd Numbers work better for maze size");
		}

		DestroyMaze();

		// assigns the mazeData array with the dimensions array
		mazeData = dataGenerator.Dimensions(sizeRows, sizeCols);

		FindStartPosition();
		FindFinishPosition();

		hallWidth = meshGenerator.width;
		hallHeight = meshGenerator.height;

		// Places the start flag at the start of the maze.
		Instantiate(mazeStart, new Vector3(startCol * hallWidth, 0.0f, startRow * hallWidth), Quaternion.identity);
		// Places the finish flag at the end of the maze.
		Instantiate(mazeEnd, new Vector3(endCol * hallWidth, 0.0f, endRow * hallWidth), Quaternion.identity);

		DisplayMaze();
	}

	// deletes an existing maze
	public void DestroyMaze() 
	{
		// finds all objects with the "Generated" tag
		GameObject[] maze = GameObject.FindGameObjectsWithTag("Generated");
		
		// destroys all the "Generated" objects
		foreach (GameObject mazeObject in maze) 
		{
			Destroy(mazeObject);
		}

		Destroy(GameObject.FindGameObjectWithTag("End"));
	}

	private void FindStartPosition() 
	{
		// creates an array that stores the array in mazeData
		int[,] maze = mazeData;

		// finds the maximum row and column values
		int rowMax = maze.GetUpperBound(0);
		int colMax = maze.GetUpperBound(1);

		// starts at (0,0), iterates through until it finds a space.
		for (int i = 0; i <= rowMax; i++) 
		{
			for (int j = 0; j <= colMax; j++) 
			{
                // checks that there is no wall
				if (maze[i, j] == 0) 
				{
					// stores coords as the maze start position.
					startRow = i;
					startCol = j;
					return;
				}
			}
		}


	}

	private void FindFinishPosition() 
	{
		// creates an array that stores the array in mazeData
		int[,] maze = mazeData;

		// finds the maximum row and column values
		int rowMax = maze.GetUpperBound(0);
		int colMax = maze.GetUpperBound(1);

		// starts with the max values, iterates in reverse until it finds a space.
		for (int i = rowMax; i >= 0; i--) 
		{
			for (int j = colMax; j >= 0; j--) 
			{
				if (maze[i, j] == 0) 
				{
					// stores coords as the maze end position.
					endRow = i;
					endCol = j;
					return;
				}
			}
		}
	}
}
