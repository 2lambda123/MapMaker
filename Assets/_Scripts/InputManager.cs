using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This class is used to manage the user's input

public class InputManager : MonoBehaviour
{
	[Header("Tilemap Editor Variables")]
	[SerializeField]
	private TilemapManager tilemapManager;
	[Tooltip("The selected tile that will be placed in the scene using left mouse button")]
	[SerializeField]
	private TileObject leftSelectedTile;
	[SerializeField]
	private Image leftSelectedTileImage;
	[Tooltip("The selected tile that will be placed in the scene using right mouse button")]
	[SerializeField]
	private TileObject rightSelectedTile;
	[SerializeField]
	private Image rightSelectedTileImage;

	[Header("Sprites")]
	[SerializeField]
	private Sprite eraserSprite;

	[Header("Debug Settings")]
	[SerializeField]
	private bool mousePressStartedOnUI;
	[SerializeField]

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
			if (!EventSystem.current.IsPointerOverGameObject() && !mousePressStartedOnUI && leftSelectedTile != null)
			{
				// Update the tilemap via the tilemap manager
				tilemapManager.SetTile(leftSelectedTile);
			}
		}

		// Whenever the right-mouse button is held down above a tile, erase the cell under the cursor depending on the selected tile
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

	// Change the tile that is currently selected
	public void SelectTile(TileObject tile)
	{
		// Set the left selected tile
		leftSelectedTile = tile;

		// Set the left selected tile image
		if (tile.TileName.Equals("Eraser"))
		{
			leftSelectedTileImage.sprite = eraserSprite;
		}
		else
		{
			leftSelectedTileImage.sprite = tile.Tile.sprite;
		}
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
