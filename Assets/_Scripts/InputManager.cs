using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The tilemap that will be edited in the scene")]
	private Tilemap tilemap = null;

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
			if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI)
			{
				// Calculate the position of the cell being clicked on
				Vector3Int clickedCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

				Debug.Log(Input.mousePosition + "-> Clicked on coordinates " + clickedCell);

				// Update the selected cell with the seleced tile
				tilemap.SetTile(clickedCell, selectedTile.TileBase);
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
