using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMode : MonoBehaviour
{
    [SerializeField] private GameObject setObj;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject setButton;

    public void ChooseMOde()
    {
        modePanel = gameObject.transform.parent.gameObject;
        GameObject button =  modePanel.transform.GetChild(LampClass.instance.mode).gameObject;
        GameObject oldSetObj =  button.transform.GetChild(1).gameObject;
        oldSetObj.SetActive(false);

        int mode  = setButton.gameObject.transform.GetSiblingIndex();
#if UNITY_EDITOR
        Debug.Log("..........SENT MODE = " + mode);
#endif
        LampClass.instance.mode = mode;
        ClientSend.SendP_Mode(mode);
        setObj.SetActive(true);
    }
}
