using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectorAnimMgn : MonoBehaviour
{
    Animator anim;

    //variable for checking if the menu is open 
    private bool isOpen = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void tileSelectorTab()
    {
        //open if closed
        if (!isOpen)
        {
            isOpen = true;
            slideIn();
        }
        //close if opened
        else if (isOpen)
        {
            isOpen = false;
            slideOut();
        }
    }

    public void slideOut()
    {
        anim.SetTrigger("SlideOut");
    }

    public void slideIn()
    {
        anim.SetTrigger("SlideIn");
    }
}



//SDG