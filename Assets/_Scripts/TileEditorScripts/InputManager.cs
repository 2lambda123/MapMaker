using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This class is used to manage the user's input

public class InputManager : MonoBehaviour
{
	[Header("Editor Parameters")]
	[SerializeField]
	private string editorMode = "PAINT";

	[Header("Tilemap Editor Variables")]
	[SerializeField]
	private CanvasManager tilemapManager = default;
	[Tooltip("The selected tile that will be placed in the scene using left mouse button")]
	[SerializeField]
	private TileObject leftSelectedTile = default;
	[SerializeField]
	private Image leftSelectedTileImage = default;
	[Tooltip("The selected tile that will be placed in the scene using right mouse button")]
	[SerializeField]
	private TileObject rightSelectedTile = default;
	[SerializeField]
	private Image rightSelectedTileImage = default;

	[Header("Erase Mode")]
	[SerializeField]
	private Sprite eraserSprite = default;
	[SerializeField]
	private TileObject eraseTile = default;

	[Header("Debug Settings")]
	[SerializeField]
	private bool mousePressStartedOnUI = default;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (editorMode.Equals("PAINT"))
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

			// Whenever the left-mouse button is held down above a tile, update the cell under the cursor depending on the primary tile
			if (Input.GetMouseButton(0))
			{
				// Modify the tile map when the following conditions are satisfied:
				// 1. The mouse cursor is NOT above a UI element
				// 2. The mouse click did NOT begin on a UI element
				// 3. A tile has been selected
				if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI && leftSelectedTile != null)
				{
					// Update the tilemap via the tilemap manager
					tilemapManager.SetTile(leftSelectedTile);
				}
			}

			// Whenever the right-mouse button is held down above a tile, update the cell under the cursor depending on the secondary tile
			if (Input.GetMouseButton(1))
			{
				// Modify the tile map when the following conditions are satisfied:
				// 1. The mouse cursor is NOT above a UI element
				// 2. The mouse click did NOT begin on a UI element
				// 3. A tile has been selected
				if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI && leftSelectedTile != null)
				{
					// Update the tilemap via the tilemap manager
					tilemapManager.SetTile(rightSelectedTile);
				}
			}

			// Release all flags wheneven the left-mouse button is released
			if (Input.GetMouseButtonUp(0))
			{
				mousePressStartedOnUI = false;
			}
		}
		else if (editorMode.Equals("ERASE"))
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

			// Whenever the left-mouse button is held down above a tile, erase the cell under the cursor
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
			{
				// Modify the tile map when the following conditions are satisfied:
				// 1. The mouse cursor is NOT above a UI element
				// 2. The mouse click did NOT begin on a UI element
				if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI)
				{
					// Update the tilemap via the tilemap manager
					tilemapManager.SetTile(eraseTile);
				}
			}

			// Release all flags wheneven the left-mouse button is released
			if (Input.GetMouseButtonUp(0))
			{
				mousePressStartedOnUI = false;
			}
		}
	}

	public string EditorMode
	{
		get { return editorMode; }
		set { editorMode = value; }
	}

	// === TILEMAP FUNCTIONALITY === //

	// Change the tile that is currently selected
	public void SelectTile(TileObject tile)
	{
		// Change the editor mode to SELECT automatically when the user selects a tile
		editorMode = "PAINT";

		// Set the left selected tile
		leftSelectedTile = tile;

		// Set the left selected tile image
		leftSelectedTileImage.sprite = tile.Tile.sprite;
	}

	// Swap the left and right selected tiles
	public void SwapSelectedTiles()
	{
		// Swap the tile objects
		TileObject temp = rightSelectedTile;
		rightSelectedTile = leftSelectedTile;
		leftSelectedTile = temp;

		// Swap the images
		Sprite tempImage = rightSelectedTileImage.sprite;
		rightSelectedTileImage.sprite = leftSelectedTileImage.sprite;
		leftSelectedTileImage.sprite = tempImage;
	}
}
