using UnityEngine;
using System;

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

        //CURR 0 50 100 7 1

        private void UpdateUI(string _packet) {
                string[] subs = _packet.Split(' ');
                int mode = Int32.Parse(subs[1]);
                int brightness = Int32.Parse(subs[2]);
                int speed = Int32.Parse(subs[3]);
                int scale = Int32.Parse(subs[4]);
                bool OnFlag = false;
                if (Int32.Parse(subs[5]) == 1)
                        OnFlag = true;
                else
                        OnFlag = false;

                LampClass.instance.UpdateValues(mode, brightness, speed, scale, OnFlag);
#if UNITY_EDITOR
                Debug.Log("____UPDATE UI_____");
#endif        
        }
}
