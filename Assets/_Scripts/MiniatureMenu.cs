using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniatureMenu : MonoBehaviour
{
    public void Awake()
    {
        Miniature mini_script = this.transform.parent.GetComponent<Miniature>();
        Dictionary<string, string> attributes = mini_script.GetAttributes();
        string name = "",
            size = "",
            hp = "",
            status = "";
        attributes.TryGetValue("Name", out name);
        attributes.TryGetValue("Size", out size);
        attributes.TryGetValue("HP", out hp);
        attributes.TryGetValue("Status", out status);
        setFieldText("Name", name);
        setFieldText("Size", size);
        setFieldText("HP", hp);
        setFieldText("Affliction", status);
    }

    private void setFieldText(string field, string value)
    {
        TMP_InputField input_field = GameObject.Find(
            "Canvas/BackgroundPanel/InputPanel/InputField-" + field
        ).GetComponent<TMP_InputField>();
        input_field.text = value;
    }

    public void updateAttr(string key, string value)
    {
        Miniature mini_script = this.transform.parent.GetComponent<Miniature>();
        mini_script.SetAttribute(key, value);
    }

    public void updateName(string value)
    {
        updateAttr("Name", value);
    }

    public void updateSize(string value)
    {
        updateAttr("Size", value);
    }

    public void updateHP(string value)
    {
        updateAttr("HP", value);
    }

    public void updateStatus(string value)
    {
        updateAttr("Status", value);
    }
}
