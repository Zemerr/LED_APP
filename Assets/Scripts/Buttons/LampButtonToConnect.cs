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

    public void ConnectButton() {
#if UNITY_EDITOR
        Debug.Log("..........Connection............");
#endif
        Client.instance.SetIP(text.text);
        Client.instance.StartServer();
        Client.instance.ConnectToServer();
        gameObject.SetActive (false);
        connectPanel.SetActive (true);
    }

}
