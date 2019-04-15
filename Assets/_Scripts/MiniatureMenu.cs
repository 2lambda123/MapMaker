using UnityEngine;

public class MiniatureMenu : MonoBehaviour
{
    Miniature mini = this.transform.parent.gameObject;

    public void updateAttr(string key, string value)
    {
        mini.setAttribute(key, value);
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
