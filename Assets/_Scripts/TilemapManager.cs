using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

// This class is used to manage the tilemap present in the scene

public class TilemapManager : MonoBehaviour
{
	[Header("Tilemap Parameters")]
	[SerializeField]
	[Tooltip("The tilemap that will be edited in the scene")]
	private Tilemap tilemap = null;
	[SerializeField]
	private int tilemapHeight;
	[SerializeField]
	private int tilemapWidth;

	[Header("Mouse Indicator Parameters")]
	[SerializeField]
	[Tooltip("The tilemap that will be used to indicate which tile will be editted")]
	private Tilemap indicatorTilemap;
	[SerializeField]
	private TileBase mouseIndicatorTile;
	[SerializeField]
	private Vector3Int activeMouseIndicatorPosition;

	[Header("Tile Objects")]
	[SerializeField]
	private List<TileObject> tileObjects;

	// Note: Imagine flipping this array vertically to see what it would look like in the tilemap editor
	private int[,] tilemapArray;

	// Start is called before the first frame update
	void Start()
	{
		// TODO: Set these player preferences from the start menu to adjust the bounds of the tile map
		PlayerPrefs.SetInt("Tilemap Height", 10);
		PlayerPrefs.SetInt("Tilemap Width", 10);

		// Retrieve the height and width from the player preferences
		tilemapHeight = PlayerPrefs.GetInt("Tilemap Height");
		tilemapWidth = PlayerPrefs.GetInt("Tilemap Width");

		// Initialize the tilemap array
		tilemapArray = new int[tilemapWidth, tilemapHeight];
	}

	// Sets a tile on the tilemap
	public void SetTile(TileObject tile)
	{
		// Calculate the position of the cell being clicked on
		Vector3Int clickedCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		// Debug.Log(Input.mousePosition + "-> Clicked on coordinates " + clickedCell);

		// Check if the clicked cell is within the map bounds
		if (clickedCell.x >= 0 && clickedCell.x < tilemapWidth && clickedCell.y >= 0 && clickedCell.y < tilemapHeight)
		{
			if (tile != null)
			{
				// Store the tile id in an array
				tilemapArray[clickedCell.x, clickedCell.y] = tile.TileId;

				// Set the tile in the tilemap
				tilemap.SetTile(clickedCell, tile.Tile);
			}
			else
			{
				// Store the tile id in an array
				tilemapArray[clickedCell.x, clickedCell.y] = 0;

				// Set the tile in the tilemap
				tilemap.SetTile(clickedCell, null);
			}

		}
		else
		{
			Debug.Log("Clicked Cell -> Out of Bounds");
		}
	}

	// Sets a mouse indicator on a separate tilemap
	public void SetMouseIndicator()
	{
		// Calculate the position of the cell being clicked on
		Vector3Int hoveredCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		// Debug.Log(Input.mousePosition + "-> Clicked on coordinates " + clickedCell);


		// Remove the mouse indicator if the mouse is hovering over a UI element
		if (EventSystem.current.IsPointerOverGameObject())
		{
			// Remove the previous mouse indicator
			indicatorTilemap.SetTile(activeMouseIndicatorPosition, null);

			// Store a default position that is out of bounds
			activeMouseIndicatorPosition = new Vector3Int(-1, -1, 0);
		}
		// Do nothing if there is no need to move the mouse indicator
		else if (hoveredCell == activeMouseIndicatorPosition) { }
		// Move the mouse indicator (Check if the cell below the mouse is within the map bounds, the mouse is hovering over a different cell, and the mouse is not over a UI element)
		else if (hoveredCell.x >= 0 && hoveredCell.x < tilemapWidth && hoveredCell.y >= 0 && hoveredCell.y < tilemapHeight)
		{
			// Remove the previous mouse indicator when the mouse enters another cell
			indicatorTilemap.SetTile(activeMouseIndicatorPosition, null);

			// Store the current position of the mouse indicator
			activeMouseIndicatorPosition = hoveredCell;

			// Move the mouse indicator to the new cell
			indicatorTilemap.SetTile(hoveredCell, mouseIndicatorTile);
		}
		// Remove the mouse indicator if the mouse is out of bounds
		else
		{
			// Remove the previous mouse indicator
			indicatorTilemap.SetTile(activeMouseIndicatorPosition, null);

			// Store a default position that is out of bounds
			activeMouseIndicatorPosition = new Vector3Int(-1, -1, 0);
		}

	}

	// Generate a tilemap based on an array of tile ids
	public void GenerateTilemap(int[,] ids)
	{
		// Set the current tilemap array to the array of ids provided
		tilemapArray = ids;

		// Loop through the array and set the tiles in the tilemap
		for (int i = 0; i < tilemapArray.GetLength(0); i++)
		{
			for (int j = 0; j < tilemapArray.GetLength(1); j++)
			{
				TileObject tileObject = FindTileObject(tilemapArray[i, j]);
				if (tileObject.Tile != null)
				{
					tilemap.SetTile(new Vector3Int(i, j, 0), tileObject.Tile);
				}
				else
				{
					tilemap.SetTile(new Vector3Int(i, j, 0), null);
				}
			}
		}
	}

	// Return the array representation of the tilemap
	public int[,] GetTilemap()
	{
		return tilemapArray;
	}

	// Find the corresponding tile object based on the provided id
	private TileObject FindTileObject(int id)
	{
		for (int i = 0; i < tileObjects.Count; i++)
		{
			if (tileObjects[i].TileId == id)
			{
				return tileObjects[i];
			}
		}
		// Return NULL if no tile object was found
		return null;
	}

	// DEBUG FUNCTION REMOVE LATER
	public void TestSaveAndLoad()
	{
		Debug.Log("Test save and load");
		GenerateTilemap(GetTilemap());
	}
}

