using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    private ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("Map Files", "dat"),
        new ExtensionFilter("All Files", "*" ),
    };
    private SaveMenu save_menu;

    // Start is called before the first frame update
    void Start()
    {
        save_menu = gameObject.AddComponent<SaveMenu>();
    }

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
        PlayerPrefs.SetString("Map File Path", "");
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
        string to_load = StandaloneFileBrowser.OpenFilePanel("Load Map", "", "*", false)[0];
        Debug.Log(to_load);
        if (to_load.Length > 0) {
            PlayerPrefs.SetString(
                "Map File Path",
                to_load
            );
            Application.LoadLevel("_scenes/TileScene");
        }
    }

    private string GetCopyName(string path)
    {
        return System.IO.Path.GetDirectoryName(path) + "/"
            + System.IO.Path.GetFileNameWithoutExtension(path) + "_copy"
            + System.IO.Path.GetExtension(path);
    }

    public void Rename()
    {
        string orig = StandaloneFileBrowser.OpenFilePanel("Rename Map", "", "*", false)[0];
        if (orig.Length == 0) {
            return;
        }
        string new_name = StandaloneFileBrowser.SaveFilePanel("Rename Map As", "", GetCopyName(orig), "dat");
        if (new_name.Length == 0) {
            return;
        }
        System.IO.File.Move(
            orig,
            new_name
        );
    }

    public void Duplicate()
    {
        string orig = StandaloneFileBrowser.OpenFilePanel("Delete Map", "", "*", false)[0];
        if (orig.Length == 0) {
            return;
        }
        System.IO.File.Copy(
            orig, 
            GetCopyName(orig)
        );
    }

    public void Delete()
    {
        string[] to_delete = StandaloneFileBrowser.OpenFilePanel("Delete Map", "", "*", true);
        foreach (string file in to_delete) {
            if (file.Length > 0) {
                System.IO.File.Delete(
                    file
                );
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
