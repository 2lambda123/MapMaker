using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLibrary : MonoBehaviour
{
	[Header("Tile Objects")]
	[SerializeField]
	private List<TileObject> tileObjects = default;

	// Retrieve a tile object based off an id value
	public TileObject GetTile(int id)
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
}
