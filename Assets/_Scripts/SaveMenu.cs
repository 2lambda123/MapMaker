using UnityEngine;
using SFB;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//StandardFileBrowser: https://github.com/gkngkc/UnityStandaloneFileBrowser

public class SaveMenu : MonoBehaviour
{
	private string _path;
	private string fileName;
	private string fileExt = "dat";

	private void Start()
	{
		if (fileName == null)
		{
			fileName = "untitled_map";
		}
		if (_path == null)
		{
			_path = Path.Combine(Application.persistentDataPath, fileName + "." + fileExt);
		}
	}

	// opens up the save as menu
	public void SaveAs()
	{
		string temp_path = StandaloneFileBrowser.SaveFilePanel("Save Map As", "", fileName, fileExt);
        if (temp_path == "") {
            Debug.Log("Canceled save");
            return;
        }
        _path = temp_path;
		Debug.Log("Save:" + _path);
		Save();
	}

	//Just save current name and data
	public void Save()
	{
		int[,] tiles = gameObject.GetComponent<CanvasManager>().GetTilemap();
		BinaryFormatter binaryFormatter = new BinaryFormatter();

		using (FileStream fileStream = File.Open(_path, FileMode.OpenOrCreate))
		{
			binaryFormatter.Serialize(fileStream, tiles);
		}
		Debug.Log("Saved");
	}

	//loads file selected, binary int[,]
	public void Load()
	{
		var extensions = new[] {
				new ExtensionFilter("Map Files", fileExt),
				new ExtensionFilter("All Files", "*" ),
			};
		WriteResult(StandaloneFileBrowser.OpenFilePanel("Open Map", "", extensions, true));
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using (FileStream fileStream = File.Open(_path, FileMode.Open))
		{
			int[,] tiles = (int[,])binaryFormatter.Deserialize(fileStream);
			gameObject.GetComponent<CanvasManager>().GenerateTilemap(tiles);
		}
		Debug.Log("Load:" + _path);
	}

	//loads file selected given a path, binary int[,]
	public void Load(string path)
	{
        _path = path;
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using (FileStream fileStream = File.Open(_path, FileMode.Open))
		{
			int[,] tiles = (int[,])binaryFormatter.Deserialize(fileStream);
			gameObject.GetComponent<CanvasManager>().GenerateTilemap(tiles);
		}
	}

	public void WriteResult(string[] paths)
	{
		if (paths.Length == 0)
		{
			return;
		}

		_path = paths[0];
	}

	public void WriteResult(string path)
	{
		_path = path;
	}
}