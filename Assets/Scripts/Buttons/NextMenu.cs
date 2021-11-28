using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMenu : MonoBehaviour
{
    [SerializeField] private GameObject modePanel;


    public void modeMenu()
    {
        modePanel.SetActive(true);
        LampClass.instance.ModeAppear();
    }
}
