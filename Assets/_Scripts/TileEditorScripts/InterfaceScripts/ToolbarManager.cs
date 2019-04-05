/*
 * Nate Brewer
 * CECS 550
 * Team Project - Tous' Tiles
 * 2019-02-22
 * 
 * Animation for open/close of Toolbar (top)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolbarManager : MonoBehaviour
{
	Animator anim2;

	//variable for checking if the menu is open 
	private bool isOpen2 = true;

	private void Start()
	{
		anim2 = GetComponent<Animator>();
	}

	public void toolbarTab()
	{
		//open if closed
		if (!isOpen2)
		{
			isOpen2 = true;
			ToolbarSlideIn();
		}
		//close if opened
		else if (isOpen2)
		{
			isOpen2 = false;
			ToolbarSlideOut();
		}
        else
        {
            ;//do nothing
        }
	}

	public void ToolbarSlideOut()
	{
		anim2.SetTrigger("ToolbarSlideOut");
	}

	public void ToolbarSlideIn()
	{
		anim2.SetTrigger("ToolbarSlideIn");
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
}



//SDG