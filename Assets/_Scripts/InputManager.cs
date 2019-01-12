using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap tilemap = null;

	[SerializeField]
	private TileBase selectedTile;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Whenever the left-mouse button is held down, update the cell under the cursor depending on the selected tile
		if (Input.GetMouseButton(0))
		{
			// Calculate the position of the cell being clicked on
			Vector3Int clickedCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			Debug.Log(Input.mousePosition + "-> Clicked on coordinates " + clickedCell);

			// Update the selected cell with the seleced tile
			tilemap.SetTile(clickedCell, selectedTile);
		}
	}
}
