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

	[Header("Miniature Attributes")]
	[SerializeField]
	private int miniatureId = 0;
	private Dictionary<string, string> miniatureAttributes = new Dictionary<string, string>();
	private bool visible = false;
	public GameObject miniMenu;
	private cameraInteraction cameraMove;

	// Get the input manager when the miniature is created
	void Awake()
	{
		// Get the reference to the input manager
		inputManager = GameObject.FindObjectOfType<InputManager>();
		miniMenu = GameObject.Find("MiniatureInfo");
		cameraMove = GameObject.Find("Main Camera").GetComponent<cameraInteraction>();
		if (miniMenu != null)
		{
			miniMenu.SetActive(visible);
		}
		UpdateMiniatureRender();
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

		//debugging
		bool thisVisible = getVisible();
		if (miniMenu != null)
		{
			miniMenu.SetActive(thisVisible);
		}
	}

	// === Miniature Movement Functionality === //

	// Interact with the miniature
	public void OnMouseOver()
	{
		// Only pick up a miniature if not other miniature is selected
		if (inputManager.SelectedMiniature == null && inputManager.EditorMode.Equals("SELECT") && Input.GetMouseButtonUp(0) && visible == false)
		{
			PickUp();
		}
		// Only call the drop function on the currently selected miniature
		else if (inputManager.SelectedMiniature == this.gameObject && inputManager.EditorMode.Equals("SELECT") && Input.GetMouseButtonUp(0))
		{
			Drop();
		}
		// Bring up the miniature tooltip when the user right clicks on it
		else if (inputManager.EditorMode.Equals("SELECT") && Input.GetMouseButtonUp(1))
		{
			// TODO: Implement tooltip stuff
			setVisible(true);

			Debug.Log("right click detected");

		}
	}

	// Pick up the miniature
	public void PickUp()
	{
		// Calculate the position of the miniature the user pressed on
		startPos = transform.position;
		clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		offset = transform.position - clickedPos;

		// Flag the miniature as being picked up
		isPickedUp = true;
		inputManager.SelectedMiniature = this.gameObject;
	}

	// Drop the miniature
	public void Drop()
	{
		// Flag the miniature as being dropped
		isPickedUp = false;
		inputManager.SelectedMiniature = null;
	}

	// === Miniature Attributes Functionality === //

	// Set the attributes of the miniature
	public void SetAttributes(Dictionary<string, string> attributes)
	{
		miniatureAttributes = attributes;
		UpdateMiniatureRender();
	}

	// Get the attributes of the miniature
	public Dictionary<string, string> GetAttributes()
	{
		return miniatureAttributes;
	}

	// Add an attribute to the miniature
	public void AddAttribute(string name, string value)
	{
		miniatureAttributes.Add(name, value);

		// Attempt to update the rendering of the miniature's sprite
		UpdateMiniatureRender();
	}

	public void SetAttribute(string name, string value)
	{
		miniatureAttributes[name] = value;
		UpdateMiniatureRender();
	}

	// Remove an attribute from the miniature
	public void RemoveAttribute(string name)
	{
		miniatureAttributes.Remove(name);

		// Attempt to update the rendering of the miniature's sprite
		UpdateMiniatureRender();
	}

	public void setVisible(bool vis)
	{
		visible = vis;
		cameraMove.setMove(!visible);
	}

	public bool getVisible()
	{
		return visible;
	}
	// make menu none active, ie not visible after "x" button pressed
	public void closeMenu()
	{
		setVisible(false);
	}

	//delete miniature after pressing "delete button"
	public void deleteMini()
	{
		CanvasManager cm = new CanvasManager();
		GameObject miniature = this.gameObject;
		Debug.Log("selected miniature " + miniature);
		cm.DeleteMiniature(miniature);
		Destroy(miniature);

	}

	public void updateNameTag(string name)
	{
		if (name == "") {
		}
		else {
		}
	}

	public void updateSize(string size)
	{
		switch (size.ToLower())
		{
			case "large":
			case "big":
			case "huge":
			case "giant":
				gameObject.transform.localScale = new Vector3(2, 2, 2);
				break;
			case "small":
			case "tiny":
			case "mini":
			case "petite":
				gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				break;
			default:
				gameObject.transform.localScale = new Vector3(1, 1, 1);
				break;
		}
	}

	public void updateStatus(string status)
	{
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		switch (status.ToLower())
		{
			case "poison": case "poisoned":
				renderer.color = Color.green;
				break;
			case "burn": case "burning":
				renderer.color = Color.red;
				break;
			case "freeze": case "frozen":
				renderer.color = Color.blue;
				break;
			case "dead": case "unconscious":
				renderer.flipY = true;
				break;
			default:
				renderer.flipY = false;
				renderer.color = Color.white;
				break;
		}
	}

	// Update the rendering of the miniature depending on the attributes assigned to the miniature
	public void UpdateMiniatureRender()
	{
		Debug.Log("UPDATING RENDER");
		foreach (var attribute in miniatureAttributes) {
			switch (attribute.Key) {
				case "Name":
					updateNameTag(attribute.Value);
					break;
				case "Size":
					updateSize(attribute.Value);
					break;
				case "Status":
					updateStatus(attribute.Value);
					break;
			}
		}
	}

	// Get the id of the prefab
	public int GetMiniatureId()
	{
		return miniatureId;
	}
}
