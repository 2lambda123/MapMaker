/*
 * Nate Brewer
 * CECS 550
 * Team Project - Tous' Tiles
 * 2019-02-21
 * 
 * Animation for open/close of Tile Selection Menu (left side)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectorAnimMgn : MonoBehaviour
{
    Animator anim1;

    //variable for checking if the menu is open 
    private bool isOpen1 = true;

    private void Start()
    {
        anim1 = GetComponent<Animator>();
    }

    public void tileSelectorTab()
    {
        //open if closed
        if (!isOpen1)
        {
            isOpen1 = true;
            slideIn1();
        }
        //close if opened
        else if (isOpen1)
        {
            isOpen1 = false;
            slideOut1();
        }
        else
        {
            ;//do nothing
        }
    }

    public void slideOut1()
    {
        anim1.SetTrigger("SlideOut");
    }

    public void slideIn1()
    {
        anim1.SetTrigger("SlideIn");
    }
}



//SDG