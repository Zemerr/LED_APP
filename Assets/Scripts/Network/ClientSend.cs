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

    public static void SendP_Mode(int mode)
    {
        using (Packet _packet = new Packet())
        {

            string info = "EFF" + mode;
            _packet.Write(info);
#if UNITY_EDITOR
            Debug.Log(".........SEND MODE........... " + info);
#endif

            SendUDPData(_packet);
        }
    }

    public static void SendP_Speed(int speed)
    {
    using (Packet _packet = new Packet())
        {
        string info = "SPD" + speed;
        _packet.Write(info);
#if UNITY_EDITOR
        Debug.Log(".........SEND SPEED........... " + info);
#endif

        SendUDPData(_packet);
        }
    }

    public static void SendP_Scale(int scale)
    {
    using (Packet _packet = new Packet())
        {
        string info = "SCA" + scale;
        _packet.Write(info);
#if UNITY_EDITOR
        Debug.Log(".........SEND SCALE........... " + info);
#endif

        SendUDPData(_packet);
        }
    }

    public static void SendP_Bright(int bright)
    {
    using (Packet _packet = new Packet())
        {
        string info = "BRI" + bright;
        _packet.Write(info);
#if UNITY_EDITOR
        Debug.Log(".........SEND BRIGHT........... " + info);
#endif

        SendUDPData(_packet);
        }
    }
    #endregion
}
