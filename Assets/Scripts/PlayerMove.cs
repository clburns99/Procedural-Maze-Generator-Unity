using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // variables to find the horizontal and vertical input names.
	[SerializeField] private string horizontalInputName;
	[SerializeField] private string verticalInputName;
    // assingn the movement speed
	[SerializeField] private float movementSpeed;
    // an object on the canvas when player has reached the end
    [SerializeField] private GameObject finishScreen;
    // will store the character controller component.
	private CharacterController charController;

	private void Awake() 
	{
        // assigns the character controller component attached to this game object
		charController = GetComponent<CharacterController>();
	}

	private void Update() 
	{
        // calls the player movement function.
		PlayerMovement();
	}

	private void PlayerMovement() 
	{
        // stores the horizontal and vertical movement.
		float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
		float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        // creates a vector 3 that stores the transform of moving forward or right with the players vert/horiz input
		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

        // moves the player
		charController.SimpleMove(forwardMovement + rightMovement);
	}

    // when player enters another collider
    private void OnTriggerEnter(Collider other)
    {
        // when player reached the end flag, show the end screen
        if ( other.gameObject.CompareTag("End"))
        {
            finishScreen.SetActive(true);
        }
    }
}
