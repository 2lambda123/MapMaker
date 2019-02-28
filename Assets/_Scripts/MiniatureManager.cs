using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureManager : MonoBehaviour
{
	private InputManager inputManager = null;

	[SerializeField]
	[Tooltip("A list of miniatures in the scene")]
	private List<GameObject> miniatures;

	void Start()
	{
		// Get the reference to the input manager
		inputManager = GameObject.FindObjectOfType<InputManager>();
	}

	// Create a miniature in the scene and have it follow the user's mouse (This function is used for the buttons in the scene)
	public void CreateMiniature(GameObject miniature)
	{
		// Change the editor mode to SELECT automatically when the user creates a miniature
		inputManager.EditorMode = "SELECT";

		// Instantiate the miniature at the position of the user's mouse
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		GameObject newMiniature = Instantiate(miniature, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
		miniatures.Add(newMiniature);

		// Pick up the miniature after it is created
		newMiniature.GetComponent<Miniature>().PickUp();
	}

	// Create a miniature in the scene at a specific position
	public void CreateMiniature(GameObject miniature, Vector3 position)
	{
		// Instantiate the miniature at the position of the user's mouse
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		GameObject newMiniature = Instantiate(miniature, position, Quaternion.identity);
		miniatures.Add(newMiniature);
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("_scenes/MainMenuScene");
	}
}
