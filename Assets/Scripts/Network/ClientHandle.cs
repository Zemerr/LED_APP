using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{

        private delegate void UIchange(string _packet);
        private static UIchange UIdel;


        private void Start() {
                UIdel = UpdateUI;
        }



        public static void RecieveUpdate(Packet _packet)
        {
                string _msg = _packet.ReadString();
                UIdel(_msg);
#if UNITY_EDITOR
                Debug.Log($"Received packet via UDP. Contains message: {_msg}");
#endif
        }

        private void UpdateUI(string _packet) {
#if UNITY_EDITOR
                Debug.Log("____UPDATE UI_____");
#endif        
        }
}
