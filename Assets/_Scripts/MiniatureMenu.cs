using UnityEngine;

public class MiniatureMenu : MonoBehaviour
{
    public void Awake()
    {
        Miniature mini_script = this.transform.parent.GetComponent<Miniature>();
        setFieldText("Name", mini_script.GetAttribute("Name"));
        setFieldText("Size", mini_script.GetAttribute("Size"));
        setFieldText("HP", mini_script.GetAttribute("HP"));
        setFieldText("Affliction", mini_script.GetAttribute("Status"));
    }

    private void setFieldText(string field, string value)
    {
        TMP_InputField field = GameObject.Find(
            "Canvas/BackgroundPanel/InputPanel/InputField-" + field
        ).GetComponent<TMP_InputField>();
        field.richText = value;
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
