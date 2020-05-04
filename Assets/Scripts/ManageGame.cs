using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adds the MazeGenerator compenent to any GameObject this script is attached to.
[RequireComponent(typeof(MazeGenerator))]

public class ManageGame : MonoBehaviour
{
	[SerializeField] private GameObject player;

	private MazeGenerator mazeGenerator;

	void Start()
    {
		// finds the MazeGenerator class
		mazeGenerator = GetComponent<MazeGenerator>();
		StartNewMaze();
    }

	private void StartNewMaze() 
	{
		// generates a maze using the logic in the MazeGenerator class
		// (13 rows, 15 columns)
		mazeGenerator.GenerateNewMaze(13, 15);

		// variables to store the x, y, z coords for the maze start
		float x = mazeGenerator.startCol * mazeGenerator.hallWidth;
		float y = 1;
		float z = mazeGenerator.startRow * mazeGenerator.hallWidth;

		// places player at the start of the maze
		player.transform.position = new Vector3(x, y, z);
	}
}
