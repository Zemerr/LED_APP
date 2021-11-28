using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "192.168.0.101";
    public int port = 8888;
    public int myId = 3;

    public UDP udp;

    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    public void SetIP(string ip) {
        instance.ip = ip;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
#if UNITY_EDITOR
            Debug.Log("Instance already exists, destroying object!");
#endif
            Destroy(this);
        }
    }

    public void StartServer()
    {
        udp = new UDP();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

#if UNITY_EDITOR
        Debug.Log("CONNECT TO SERVER " + ip);
#endif

        udp.Connect(port);
    }

  

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }

        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }
        }

        public void SendData(Packet _packet)
        {
            try
            {
                // _packet.InsertInt(instance.myId);
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                    
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (_data.Length < 4)
                {
                    // TODO: disconnect
                    return;
                }
#if UNITY_EDITOR
                Debug.Log("RECIEVE DATA FROM LAMP = " + _data.Length);
#endif
                HandleData(_data);
            }
            catch
            {
                // TODO: disconnect
            }
        }

        private void HandleData(byte[] _data)
        {
#if UNITY_EDITOR
            Debug.Log("STARTer HandleData");
#endif
            // using (Packet _packet = new Packet(_data))
            // {
            //     int _packetLength = _packet.ReadInt();
            //     _data = _packet.ReadBytes(_packetLength);
            // }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    // int _packetId = _packet.ReadInt();
                    // packetHandlers[1](_packet);
// #if UNITY_EDITOR
//                 Debug.Log("START THEAD MAN");
// #endif
                    ClientHandle.RecieveUpdate(_packet);
                }
            });
        }
    }
    
    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.mainUpdate, ClientHandle.RecieveUpdate },
        };
#if UNITY_EDITOR
        Debug.Log("Initialized packets.");
#endif
    }
}
