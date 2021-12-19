using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [SerializeField] private Slider bright;
    [SerializeField] private Slider speed;
    [SerializeField] private Slider scale;
    
    // Start is called before the first frame update
    private void Start()
    {
        bright.onValueChanged.AddListener((v) =>  {
            Bright(v);
        });

        speed.onValueChanged.AddListener((v) =>  {
            Speed(v);
        });

        scale.onValueChanged.AddListener((v) =>  {
            Scale(v);
        });
    }


    private void Bright(float value) {
#if UNITY_EDITOR
        Debug.Log("Bright val = " + value);
#endif
        ClientSend.SendP_Bright((int)value);
    }

    private void Speed(float value) {
#if UNITY_EDITOR
        Debug.Log("Speed val = " + value);
#endif
        ClientSend.SendP_Speed((int)value);
    }

    private void Scale(float value) {
#if UNITY_EDITOR
        Debug.Log("Scale val = " + value);
#endif
        ClientSend.SendP_Scale((int)value);
    }
    


}
