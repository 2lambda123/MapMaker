using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    public string active_map_name;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

    private void New(int size)
    {
        if (size <= 0) {
            throw new System.ArgumentException("Size must be larger than zero");
        }
        PlayerPrefs.SetInt("Canvas Height", size);
        PlayerPrefs.SetInt("Canvas Width", size);
        Application.LoadLevel("_scenes/TileScene");
    }

    public void New_Small()
    {
        New(64);
    }

    public void New_Medium()
    {
        New(256);
    }

    public void New_Large()
    {
        New(1024);
    }

    public void Load()
    {
        // TODO: Hook into Stephen's stuff later
        Application.LoadLevel("_scenes/TileScene");
    }

    public void Rename()
    {
        if (active_map_name == null) {
            return;
        }
        string new_map_name = "blah.json";
        string new_map_location = Path.Combine(
            ".",
            new_map_name
        );
        System.IO.File.Move(
            active_map_name,
            new_map_location
        );
        active_map_name = new_map_location;
    }

    public void Duplicate()
    {
        if (active_map_name == null) {
            return;
        }
        string new_map_name = "blah.json";
        string new_map_location = Path.Combine(
            ".",
            new_map_name
        );
        System.IO.File.Copy(
            active_map_name, 
            new_map_location
        );
        active_map_name = new_map_location;
    }

    public void Delete()
    {
        if (active_map_name == null) {
            return;
        }
        System.IO.File.Delete(active_map_name);
        active_map_name = null;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
