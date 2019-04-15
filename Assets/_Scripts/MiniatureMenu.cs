using UnityEngine;

public class MiniatureMenu : MonoBehaviour
{
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

    public void updateStatus(string value)
    {
        updateAttr("Status", value);
    }
}
