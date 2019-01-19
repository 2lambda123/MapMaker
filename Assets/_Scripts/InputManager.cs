using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class is used to manage the user's input

public class InputManager : MonoBehaviour
{
	[Header("Tilemap Editor Variables")]
	[SerializeField]
	private TilemapManager tilemapManager;
	[SerializeField]
	[Tooltip("The selected tile that will be placed in the scene")]
	private TileObject selectedTile;

	[Header("Debug Settings")]
	[SerializeField]
	private bool mousePressStartedOnUI;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Update the mouse indicator
		tilemapManager.SetMouseIndicator();

		// Set a flag wheneven the left-mouse button is pressed down while above the UI
		if (Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				mousePressStartedOnUI = true;
			}
		}

		// Whenever the left-mouse button is held down above a tile, update the cell under the cursor depending on the selected tile
		if (Input.GetMouseButton(0))
		{
			// Modify the tile map when the following conditions are satisfied:
			// 1. The mouse cursor is NOT above a UI element
			// 2. The mouse click did NOT begin on a UI element
			// 3. A tile has been selected
			if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI && selectedTile != null)
			{
				// Update the tilemap via the tilemap manager
				tilemapManager.SetTile(selectedTile);
			}
		}

		// Release all flags wheneven the left-mouse button is released
		if (Input.GetMouseButtonUp(0))
		{
			mousePressStartedOnUI = false;
		}
	}

	// Change the tile that is currently selected
	public void SelectTile(TileObject tile)
	{
		selectedTile = tile;
	}
}
