using UnityEngine;
using UnityEngine.EventSystems;


public class LampOption : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject main = null;

    void Start() 
    {
        main =  GameObject.Find("Main");
#if UNITY_EDITOR
        if (main != null)
        Debug.Log("..........Find object " + main.name);
#endif
    }

    public void OnPointerDown(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
    }
    public void OnPointerUp(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
        main.GetComponent<OptionButtonHelper>().option.SetActive(true);
        main.GetComponent<OptionButtonHelper>().optionTop.SetActive(true);
#if UNITY_EDITOR
        Debug.Log("..........LAMP OPTION............");
#endif
    }
}