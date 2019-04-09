using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

// This class is used to manage the tilemap and miniatures present in the scene

public class CanvasManager : MonoBehaviour
{
	private InputManager inputManager = null;

	[Header("Tilemap Parameters")]
	[Tooltip("The tilemap that will be edited in the scene")]
	[SerializeField]
	private Tilemap canvasTilemap = default;
	[SerializeField]
	private int canvasHeight = default;
	[SerializeField]
	private int canvasWidth = default;
	[SerializeField]
	private string mapFilePath = default;

	// Note: Imagine flipping this array vertically to see what it would look like in the tilemap editor
	private int[,] tilemapArray;

	[Header("Mouse Indicator Parameters")]
	[Tooltip("The tilemap that will be used to indicate which tile will be editted")]
	[SerializeField]
	private Tilemap indicatorTilemap = default;
	[SerializeField]
	private TileBase mouseIndicatorTile = default;
	[SerializeField]
	private Vector3Int activeMouseIndicatorPosition = default;

	[Header("Miniature Parameters")]
	[SerializeField]
	[Tooltip("A list of miniatures in the scene")]
	private List<GameObject> miniatures = new List<GameObject>();

	[Header("Libraries")]
	[SerializeField]
	private TileLibrary tileLibrary = null;
	[SerializeField]
	private MiniatureLibrary miniatureLibrary = null;

	[Header("Save Manager")]
	private SaveMenu saveMenu;

	// Start is called before the first frame update
	void Start()
	{
		// Get the reference to the input manager
		inputManager = this.gameObject.GetComponent<InputManager>();

		// Get a reference to the save menu
		saveMenu = this.gameObject.GetComponent<SaveMenu>();

		// Retrieve the height and width from the player preferences (by default, if the key does not exist use 64)
		canvasHeight = PlayerPrefs.GetInt("Canvas Height", 64);
		Debug.Log(canvasHeight);
		canvasWidth = PlayerPrefs.GetInt("Canvas Width", 64);

		// Adjust the starting position of the camera depending on the size of the canvas
		Camera.main.transform.transform.SetPositionAndRotation(new Vector3(2.5f * canvasWidth / 2, 2.5f * canvasHeight / 2, -10), Quaternion.identity);

		// Retrieve the map name from player preferences
		mapFilePath = PlayerPrefs.GetString("Map File Path", "");

		// If there is no map name provided in the player preferences, start a new array
		if (mapFilePath.Equals(""))
		{
			// Initialize the tilemap array
			tilemapArray = new int[canvasWidth, canvasHeight];
		}
		// Else, load a map from the path file in the player preferences
		else
		{
			saveMenu.WriteResult(mapFilePath);
			int[,] tiles = saveMenu.Load(mapFilePath);
			GenerateTilemap(tiles);
		}
	}

	// === TILEMAP FUNCTIONALITY === //

	// Sets a tile on the tilemap
	public void SetTile(TileObject tile)
	{
		// Calculate the position of the cell being clicked on
		Vector3Int clickedCell = canvasTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		// Debug.Log(Input.mousePosition + "-> Clicked on coordinates " + clickedCell);

		// Check if the clicked cell is within the map bounds
		if (clickedCell.x >= 0 && clickedCell.x < canvasWidth && clickedCell.y >= 0 && clickedCell.y < canvasHeight)
		{
			if (tile != null)
			{
				// Store the tile id in an array
				tilemapArray[clickedCell.x, clickedCell.y] = tile.TileId;

				// Set the tile in the tilemap
				canvasTilemap.SetTile(clickedCell, tile.Tile);
			}
			else
			{
				// Store the tile id in an array
				tilemapArray[clickedCell.x, clickedCell.y] = 0;

				// Set the tile in the tilemap
				canvasTilemap.SetTile(clickedCell, null);
			}

		}
		else
		{
			// Debug.Log("Clicked Cell -> Out of Bounds");
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
				TileObject tileObject = tileLibrary.GetTile(tilemapArray[i, j]);
				if (tileObject.Tile != null)
				{
					canvasTilemap.SetTile(new Vector3Int(i, j, 0), tileObject.Tile);
				}
				else
				{
					canvasTilemap.SetTile(new Vector3Int(i, j, 0), null);
				}
			}
		}
	}

	// Return the array representation of the tilemap
	public int[,] GetTilemap()
	{
		return tilemapArray;
	}

	// === MOUSE INDICATOR FUNCTIONALITY === //

	// Sets a mouse indicator on a separate tilemap
	public void SetMouseIndicator()
	{
		// Calculate the position of the cell being clicked on
		Vector3Int hoveredCell = canvasTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

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
		else if (hoveredCell.x >= 0 && hoveredCell.x < canvasWidth && hoveredCell.y >= 0 && hoveredCell.y < canvasHeight)
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

	// === MINIATURE FUNCTIONALITY === //

	// Create a prefab miniature in the scene and have it follow the user's mouse (This function is used for the buttons in the scene)
	public void CreateMiniature(GameObject miniature)
	{
		// Only create a new miniature when no other miniature is selected
		if (inputManager.SelectedMiniature == null)
		{
			// Change the editor mode to SELECT automatically when the user creates a miniature
			inputManager.EditorMode = "SELECT";

			// Instantiate the miniature at the position of the user's mouse
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameObject newMiniature = Instantiate(miniature, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);

			// Add the miniature to the list of miniatures in the scene
			miniatures.Add(newMiniature);

			// Pick up the miniature after it is created
			Debug.Log("Picking up new miniature");
			newMiniature.GetComponent<Miniature>().PickUp();
		}
	}

	// Create a miniature in the scene at a specific position and with predefined attributes (Used for loading in miniatures from save one at a time)
	public void CreateMiniature(int miniatureId, Vector3 position, Dictionary<string, string> attributes)
	{
		// Instantiate the miniature at the given position
		GameObject newMiniature = Instantiate(miniatureLibrary.GetMiniature(miniatureId), position, Quaternion.identity);

		// Set the attributes of the miniature
		newMiniature.GetComponent<Miniature>().SetAttributes(attributes);

		// Add the miniature to the list of miniatures in the scene
		miniatures.Add(newMiniature);
	}

	// Delete a miniature in the scene
	public void DeleteMiniature(GameObject miniature)
	{
		miniatures.Remove(miniature);
        Debug.Log("we got there");
	}

	// Return the list of miniatures in the canvas
	public List<GameObject> GetMiniatures()
	{
		return miniatures;
	}

}

