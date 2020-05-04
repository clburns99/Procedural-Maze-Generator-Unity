using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeData
{
	// variable to determine whether a space is empty
	public float cell;

	public MazeData() 
	{
		// default value, public so can be adapted
		cell = .1f;
	}

	public int[,] Dimensions(int rowSize, int colSize) 
	{
		// creates an array to store maze columns and rows
		int[,] maze = new int[rowSize, colSize];

		// variables to find the max value of rows and columns
		int rowMax = maze.GetUpperBound(0);
		int colMax = maze.GetUpperBound(1);

		// iterates through the rows and columns of the maze, assigning walls (creating maze)
		for ( int i = 0; i <= rowMax; i++) 
		{
			for ( int j = 0; j <= colMax; j++) 
			{
				// checks to see if the current cell is on the array boundaries 
				// (outside the grid) so assigns 1 making it a wall.
				if ( i == 0 || j == 0 || i == rowMax || j == colMax) 
				{
					maze[i, j] = 1;
				}

				// checks if the coords are divisible by 2 to operate on every other cell
				else if (i % 2 == 0 && j % 2 == 0) 
				{
					// randomly skips a cell and continues iterating through the array
					if (Random.value > cell) 
					{
						// assigns 1 (wall) to the current cell
						maze[i, j] = 1;

                        // uses a series of ternary operators to randomly add 0, 1, or - 1 to the array index
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
						int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);

						// adds 0,1,-1 to the array index to randomly assign 1 (walls) to 
						// the adjacent cells
						maze[i + a, j + b] = 1;
					}
				}
			}
		}
		return maze;
	}
}
