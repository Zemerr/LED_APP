using UnityEngine.EventSystems;
using UnityEngine;

public class BackSetting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject optionTop;


    public void OnPointerDown(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
    }

    public void OnPointerUp(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
        option.SetActive(false);
        optionTop.SetActive(false);
#if UNITY_EDITOR
        Debug.Log("..........Back in SETTING menu............");
#endif
    }
}
