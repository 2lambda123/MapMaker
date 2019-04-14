using UnityEngine;
using System.Collections.Generic;
using SFB;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//StandardFileBrowser: https://github.com/gkngkc/UnityStandaloneFileBrowser

public class SaveMenu : MonoBehaviour
{
	private string _path;
	private string fileName;
	private string fileExt = "dat";

    [System.Serializable]
    private class Mini {
        public int miniatureId;
        public float x, y, z;
        public Dictionary<string, string> attributes;

        public Mini(GameObject mini) {
            Miniature m = mini.GetComponent<Miniature>();
            miniatureId = m.GetMiniatureId();
            attributes = m.GetAttributes();
            x = mini.transform.position.x;
            y = mini.transform.position.y;
            z = mini.transform.position.z;
        }
    }

    [System.Serializable]
    private class SaveFile {
        public int[,] tiles;
        public List<Mini> minis;

        public void addMinis(List<GameObject> m) {
            minis = new List<Mini>();
            foreach(GameObject mini in m) {
                minis.Add(new Mini(mini));
            }
        }
    }

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
        SaveFile data = new SaveFile();
        data.tiles = gameObject.GetComponent<CanvasManager>().GetTilemap();
        data.addMinis(gameObject.GetComponent<CanvasManager>().GetMiniatures());
        BinaryFormatter binaryFormatter = new BinaryFormatter();

		using (FileStream fileStream = File.Open(_path, FileMode.OpenOrCreate))
		{
			binaryFormatter.Serialize(fileStream, data);
		}
		Debug.Log("Saved");
	}

	//loads file selected
	public void Load()
	{
		var extensions = new[] {
				new ExtensionFilter("Map Files", fileExt),
				new ExtensionFilter("All Files", "*" ),
			};
		WriteResult(StandaloneFileBrowser.OpenFilePanel("Open Map", "", extensions, true));
        Load(_path);
		Debug.Log("Load:" + _path);
	}

	//loads file selected given a path, binary int[,]
	public void Load(string path)
	{
        _path = path;
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using (FileStream fileStream = File.Open(_path, FileMode.Open))
		{
            SaveFile data = (SaveFile)binaryFormatter.Deserialize(fileStream);
            CanvasManager canvas = gameObject.GetComponent<CanvasManager>();
            canvas.GenerateTilemap(data.tiles);
            foreach(Mini m in data.minis) {
                canvas.CreateMiniature(m.miniatureId, new Vector3(m.x, m.y, m.z), m.attributes);
            }
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