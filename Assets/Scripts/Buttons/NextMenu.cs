using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMenu : MonoBehaviour
{
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject modeTop;


    public void modeMenu()
    {
        modeTop.SetActive(true);
        modePanel.SetActive(true);
        LampClass.instance.ModeAppear();
    }
}
