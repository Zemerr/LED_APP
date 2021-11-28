using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    // private static void SendTCPData(Packet _packet)
    // {
    //     _packet.WriteLength();
    //     Client.instance.tcp.SendData(_packet);
    // }

    private static void SendUDPData(Packet _packet)
    {
        // _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    // public static void WelcomeReceived()
    // {
    //     using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
    //     {
    //         _packet.Write(Client.instance.myId);
    //         _packet.Write(UIManager.instance.usernameField.text);

    //         SendTCPData(_packet);
    //     }
    // }

    public static void UDPTestReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.updateReceived))
        {
#if UNITY_EDITOR
            Debug.Log(".........SEND UPD TEST............");
#endif
            _packet.Write("Received a UDP packetZ.");

            SendUDPData(_packet);
        }
    }


    public static void SendP_OFF()
    {
        using (Packet _packet = new Packet((int)ClientPackets.updateReceived))
        {
#if UNITY_EDITOR
            Debug.Log(".........SEND P_OFF............");
#endif
            _packet.Write("P_OFF");

            SendUDPData(_packet);
        }
    }

    public static void SendP_ON()
    {
        using (Packet _packet = new Packet((int)ClientPackets.updateReceived))
        {
#if UNITY_EDITOR
            Debug.Log(".........SEND P_ON...........");
#endif
            _packet.Write("P_ON");

            SendUDPData(_packet);
        }
    }
    #endregion
}
