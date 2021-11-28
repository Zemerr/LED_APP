using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SwichBut : MonoBehaviour, IPointerDownHandler
{


    public bool swith = true;

    [SerializeField] private GameObject switcher;
    [SerializeField] private GameObject leftSide;
    private Vector3 switchPos;
    private Vector3 rightSwitchPos;
    private Vector3 leftSwitchPos;
    private Color onColor;
    private Color ofColor;

    void Start() {
        switchPos = switcher.gameObject.transform.position;
        rightSwitchPos = switchPos;
        leftSwitchPos = leftSide.gameObject.transform.position;
        onColor = new Color32(82,157,209,255);
        ofColor = new Color32(212,111,120,255);

    }


    public void OnPointerDown(PointerEventData eventData) {
        SwitchOnOf(swith);
    }

    public void SwitchOnOf(bool state, bool onlyFronSide = false) {
        if (state) 
        {
#if UNITY_EDITOR
            Debug.Log("..........SWITCH OFF............");
#endif
            if (!onlyFronSide)
            {
                ClientSend.SendP_OFF();
            }
            GetComponent<Image>().color = ofColor;
            switcher.gameObject.transform.position = leftSwitchPos;
            swith = false;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("..........SWITCH ON............");
#endif
            if (!onlyFronSide)
            {
                ClientSend.SendP_ON();
            }
            GetComponent<Image>().color = onColor;
            switcher.gameObject.transform.position = rightSwitchPos;
            swith = true;
        }
    }
}
