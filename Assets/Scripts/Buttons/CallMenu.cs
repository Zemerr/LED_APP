using UnityEngine;
using UnityEngine.EventSystems;


public class CallMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    public void OnPointerDown(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
    }



    public void OnPointerUp(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
#if UNITY_EDITOR
        Debug.Log("..........CALL Menu............");
#endif
    }
}