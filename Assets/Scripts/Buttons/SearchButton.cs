using UnityEngine;
using UnityEngine.EventSystems;


public class SearchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LanSearch network;

    public void OnPointerDown(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y+6f, transform.position.z);
    }



    public void OnPointerUp(PointerEventData eventData) {
        transform.position = new Vector3 (transform.position.x, transform.position.y-6f, transform.position.z);
#if UNITY_EDITOR
        Debug.Log("..........CALL SEARCH AND RADAR............");
#endif

        network.StartSearchBroadCasting();
    }

    private static void StartServer()
    {
       Debug.Log("Start Server");
    }
    private static void FindServer(string strIP)
    {
        Debug.Log("Find Server");
    }

}