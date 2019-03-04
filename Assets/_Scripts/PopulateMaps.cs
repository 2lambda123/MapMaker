using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PopulateMaps : MonoBehaviour
{
    public GameObject edit_panel;
    public GameObject map_info_prefab;
    public FileInfo[] maps;
    public string base_path;

    public void RefreshMaps()
    {
        DirectoryInfo map_directory = new DirectoryInfo(base_path);
        /* maps = map_directory.GetFiles("*.json"); */
        maps = map_directory.GetFiles();
        Populate();
    }

    private void Populate()
    {
        foreach (FileInfo map in maps) {
            GameObject new_map_info = (GameObject) Instantiate(map_info_prefab, transform);
            print(map.Directory.FullName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshMaps();
    }

    // Update is called once per frame
    void Update()
    { }
}
