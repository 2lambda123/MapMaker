using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraInteraction : MonoBehaviour
{
	[SerializeField]
	private float speed = 20.0f;
	[SerializeField]
	private int maxZoom = 3;
	[SerializeField]
	private int minZoom = 40;

	private int canvasHeight = default;
	private int canvasWidth = default;

	private const float TILE_SIZE = 2.56f;

    // Start is called before the first frame update
    void Start()
	{
		Debug.Log("Camera Start Script");

		canvasHeight = PlayerPrefs.GetInt("Canvas Height", 64);
		canvasWidth = PlayerPrefs.GetInt("Canvas Width", 64);

    }

    // Update is called once per frame
    void Update()
	{
		//move camera directionally
		//conditional statements for each of the arrow and "w,a,s,d" keys
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			//Debug.Log(Camera.main.transform.position[1]);
			//Debug.Log("up arrow");
			if (Camera.main.transform.position[1] < canvasHeight * TILE_SIZE)
			{
                Debug.Log("speed = " + speed);
				transform.Translate(Vector2.up * speed * Time.deltaTime);
                
			}
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			//Debug.Log("down arrow");
			if (Camera.main.transform.position[1] > 0)
			{
				transform.Translate(Vector2.down * speed * Time.deltaTime);
			}
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			//Debug.Log("right arrow");
			if (Camera.main.transform.position[0] < canvasWidth * TILE_SIZE)
			{
				transform.Translate(Vector2.right * speed * Time.deltaTime);
			}
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			//Debug.Log("left arrow");
			if (Camera.main.transform.position[0] > 0)
			{
				transform.Translate(Vector2.left * speed * Time.deltaTime);
			}
		}

		//zoom camera with key presses "-" decrement, "=" for increment
		if (Input.GetKey(KeyCode.Equals))
		{
			//Debug.Log("plus");
			if (Camera.main.orthographicSize > maxZoom)
			{
				Camera.main.orthographicSize -= 1;
			}


		}
		if (Input.GetKey(KeyCode.Minus))
		{
            //Debug.Log("minus");
            if (Camera.main.orthographicSize < minZoom)
            {
                Camera.main.orthographicSize += 1;
            }
            else {
                
            }

		}

	}

    public float increaseSpeed {
        get { return speed; }
        set { speed += value; }
    }

    public float decreaseSpeed
    {
        get { return speed; }
        set {
            float check = speed - value;
            if (check > 0) {
                speed -= value;
            }
        }
    }

    public float defaultSpeed
    {
        get { return speed; }
        set { speed = value; }
    }

}
