using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Camera Start Script");
    }

    // Update is called once per frame
    void Update()
    {
        //move camera directionally
        //conditional statements for each of the arrow keys
        //note the hardcoded limits (3's and -3's) will need to be replaced with variables for different sized canvases
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Debug.Log(Camera.main.transform.position[1]);
            Debug.Log("up arrow");
            if (Camera.main.transform.position[1] < 18)
            {
                transform.Translate(Vector2.up * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Debug.Log("down arrow");
            if (Camera.main.transform.position[1] > (-9))
            {
                transform.Translate(Vector2.down * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("right arrow");
            if (Camera.main.transform.position[0] < 18)
            {
                transform.Translate(Vector2.right * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("left arrow");
            if (Camera.main.transform.position[0] > (-9))
            {
                transform.Translate(Vector2.left * Time.deltaTime);
            }
        }

        //zoom camera with key presses "-" decrement, "=" for increment
        if (Input.GetKey(KeyCode.Equals))
        {
            Debug.Log("plus");
            if (Camera.main.orthographicSize > 4)
            {
                Camera.main.orthographicSize -= 1;
            }


        }
        if (Input.GetKey(KeyCode.Minus))
        {
            Debug.Log("minus");
            if (Camera.main.orthographicSize < 26)
            {
                Camera.main.orthographicSize += 1;
            }

        }
    }
}
