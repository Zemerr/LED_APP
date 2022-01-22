using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


// public class LampButtonToConnect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
// {
//     [SerializeField] private GameObject connectPanel;

//     public void OnPointerDown(PointerEventData eventData) {
//         transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
//     }
//     public void OnPointerUp(PointerEventData eventData) {
//         transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
// #if UNITY_EDITOR
//         Debug.Log("..........Connection............");
// #endif
//         gameObject.SetActive (false);
//         connectPanel.SetActive (true);
//     }

// }

public class LampButtonToConnect : MonoBehaviour
{
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LampClass Lamp;

    public void ConnectButton() {
#if UNITY_EDITOR
        Debug.Log("..........Connection............");
#endif
        if (LampClass.instance.currentState != LampClass.ConnectionState.Conncected) {
            Client.instance.SetIP(text.text);
            Client.instance.StartServer();
            if (Client.instance.ConnectToServer()) {
                gameObject.SetActive(false);
                connectPanel.SetActive(true);
                LampClass.instance.ConnectionButton = transform.parent.gameObject;
                LampClass.instance.currentState = LampClass.ConnectionState.Conncected;
            }
        }
        else
        {
            if (text.text != Client.instance.ip && LampClass.instance.ConnectionButton != gameObject.transform.GetChild(0).gameObject) { //exclude touching button that already use
                Client.instance.CloseServer();
                LampClass.instance.ConnectionButton.gameObject.transform.GetChild(0).gameObject.SetActive(true); // Hide previouse connecting buttons
                LampClass.instance.ConnectionButton.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Client.instance.SetIP(text.text);
                Client.instance.StartServer();
                if (Client.instance.ConnectToServer()) {
                    gameObject.SetActive(false);
                    connectPanel.SetActive(true);
                    LampClass.instance.ConnectionButton = transform.parent.gameObject;
                    LampClass.instance.currentState = LampClass.ConnectionState.Conncected;
                }                
            }
        }
    }
}
