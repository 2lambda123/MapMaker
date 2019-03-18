using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This class is to create scriptable objects for each tile

[CreateAssetMenu(fileName = "Tile", menuName = "Tile Scriptable Object", order = 1)]
public class TileObject : ScriptableObject
{
	[Header("Tile Parameters")]
	[SerializeField]
	private string _tileName = default;
	[SerializeField]
	private int _tileId = default;
	[SerializeField]
	private Tile _tile = default;

	public string TileName
	{
		get { return _tileName; }
	}

	public int TileId
	{
		get
		{
			return _tileId;
		}
	}

	public Tile Tile
	{
		get { return _tile; }
	}
}
