using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile", menuName = "Tile Scriptable Object", order = 1)]
public class TileObject : ScriptableObject
{
	[Header("Tile Parameters")]
	[SerializeField]
	private string _tileName;
	[SerializeField]
	private int _tileId;
	[SerializeField]
	private TileBase _tileBase;

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

	public TileBase TileBase
	{
		get { return _tileBase; }
	}
}
