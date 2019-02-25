using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileSelectorPanelSlide : MonoBehaviour
{
    //refrence for the tile selector panel in the hierarchy
    public GameObject tileSelectorPanel;
    //animator reference
    private Animator animation;
    //variable for checking if the menu is open 
    private bool isOpen = true;
    //tab button to open/close
    public Button tileSelectorTab;
    /*
    //Initialization
    void Start()
    {
        //get the animator component
        animation = tileSelectorPanel.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        animation.enabled = false;

        //tileSelectorTab.onClick.AddListener(Update);//?
    }*/

    public void TileSelectorTab()
    {
        //get the animator component
        animation = tileSelectorPanel.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        animation.enabled = false;

        //open if closed
        if (!isOpen)
        {
            OpenTileSelectorMenu();
        }
        //close if opened
        else if (isOpen)
        {
            CloseTileSelectorMenu();
        }
    }

    public void OpenTileSelectorMenu()
    {
        //enable the animator component
        animation.enabled = true;
        //play the Slidein animation
        animation.Play("TileSelectorPanelSlideIn");
        //set the isOpen flag to true since now open
        isOpen = true;
    }

    public void CloseTileSelectorMenu()
    {
        //enable the animator component
        animation.enabled = true;
        //play the SlideOut animation
        animation.Play("TileSelectorPanelSlideOut");
        //set the isOpen flag to false
        isOpen = false;
    }
}
