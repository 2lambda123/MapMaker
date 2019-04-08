/*
 * Nate Brewer
 * CECS 550
 * Team Project - Tous' Tiles
 * 2019-02-26
 * 
 * Animation for open/close of Miniatures Selection Menu (right side)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSelectionAnimMgn : MonoBehaviour
{
    Animator anim3;

    //variable for checking if the menu is open 
    private bool isOpen3 = true;

    private void Start()
    {
        anim3 = GetComponent<Animator>();
    }

    public void miniaturesSelectorTab()
    {
        //open if closed
        if (!isOpen3)
        {
            isOpen3 = true;
            slideIn3();
        }
        //close if opened
        else if (isOpen3)
        {
            isOpen3 = false;
            slideOut3();
        }
        else
        {
            ;//do nothing
        }
    }

    public void slideOut3()
    {
        anim3.SetTrigger("MiniSlideOut");
    }

    public void slideIn3()
    {
        anim3.SetTrigger("MiniSlideIn");
    }
}



//SDG