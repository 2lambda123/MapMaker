using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniature : MonoBehaviour
{
	[Header("Click/Drag Variables")]
	[SerializeField]
	private bool isPickedUp = false;
	[SerializeField]
	private Vector3 mousePosition = default;
	[SerializeField]
	private Vector3 clickedPos;
	[SerializeField]
	private Vector3 startPos;
	[SerializeField]
	private Vector3 offset;

	private InputManager inputManager;

	// Start is called before the first frame update
	void Start()
	{
		// Get the reference to the input manager
		inputManager = GameObject.FindObjectOfType<InputManager>();
	}

	// Update is called once per frame
	void Update()
	{
		// Follow the user's mouse whenever the miniature is picked up
		if (isPickedUp)
		{
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = mousePosition + offset;
		}
	}

	public void OnMouseUpAsButton()
	{
		// Only allow miniatures to be moved in play mode
		if (inputManager.EditorMode.Equals("SELECT"))
		{
			PickUp();
		}
	}

	// Pick up the miniature
	public void PickUp()
	{
		// Calculate the position of the miniature the user pressed on
		startPos = transform.position;
		clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		offset = transform.position - clickedPos;

		// Flag the miniature as being picked up or not
		isPickedUp = !isPickedUp;
	}
}
