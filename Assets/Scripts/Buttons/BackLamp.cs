using UnityEngine.EventSystems;
using UnityEngine;

public class BackLamp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public GameObject option;
    [SerializeField] public GameObject optionTop;


    public void OnPointerDown(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
    }

    public void OnPointerUp(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
        option.SetActive(false);
        optionTop.SetActive(false);
#if UNITY_EDITOR
        Debug.Log("..........Back in LAmp menu............");
#endif
    }
}
