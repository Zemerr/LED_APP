using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLampButtons : MonoBehaviour
{

    [SerializeField] private LanSearch network;
    [SerializeField] private GameObject buttonPrefab;
    public bool couldAddlampBut = false;

    public string IPtoADD = null;
    private GameObject textIP = null;


    // Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)

    
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (LanSearch.currentState == LanSearch.enuState.Searching && couldAddlampBut)
        {
            if (IPtoADD != null)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                textIP = button.transform.GetChild(0).GetChild(1).gameObject;
                textIP.GetComponent<TextMeshProUGUI>().text = IPtoADD;
#if UNITY_EDITOR
                Debug.Log(textIP.name);
#endif
                // button.GetComponentsInChildren<>
                couldAddlampBut = false;
                IPtoADD = null;
            }
        }

    }

    public void DeleteAllButtons()
    {
        foreach (Transform child in transform) {
            if (child.gameObject != LampClass.instance.ConnectionButton)
                GameObject.Destroy(child.gameObject);
        }
    }
    
}
