using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class LanSearch : MonoBehaviour
{
    [SerializeField] private ShowLampButtons ShowLamp;
    [SerializeField] private GameObject radar;
    public string IP = "";

    // public delegate void delJoinServer(string strIP); // Definition of JoinServer Delegate, takes a string as argument that holds the ip of the server
    // public delegate void delStartServer(); // Definition of StartServer Delegate

    public enum enuState {
        NotActive,
        Searching,
        Finished
    }; // Definition of State Enumeration.
    public struct ReceivedMessage { 
        // public float fTime;
        public string strIP;
    } // Definition of a Received Message struct. This is the form in which we will store messages

    public enuState currentState = enuState.NotActive;
    private UdpClient objUDPClient; // The UDPClient we will use to send and receive messages
    public HashSet<ReceivedMessage> lstReceivedMessages; // The list we store all received messages in, when searching

    // private delJoinServer delWhenServerFound; // Reference to the delegate that will be called when a server is found, set by StartSearchBroadcasting()
    // private delStartServer delWhenServerMustStarted; // Reference to the delegate that will be called when a server must be created, set by StartSearchBroadcasting()

    private float fTimeLastMessageSent;
    private float fIntervalMessageSending = 1f; // The interval in seconds between the sending of messages
    // private float fTimeMessagesLive = 3; // The time a message 'lives' in our list, before it gets deleted
    private float fTimeToSearch = 5; // The time the script will search, before deciding what to do
    private float fTimeSearchStarted;


    void Start()
    {
        // Create our list
        lstReceivedMessages = new HashSet<ReceivedMessage>();
        IP = GetIP(ADDRESSFAM.IPv4);
        
        // delWhenServerMustStarted = StartServer;
        // delWhenServerFound = FindServer;


        // StartSearchBroadCasting(FindServer, StartServer);
    }

    void Update()
    {
        // Check if we need to send messages and the interval has espired
        if (currentState == enuState.Searching
            && Time.time > fTimeLastMessageSent + fIntervalMessageSending)
        {
            // Determine out of our current state what the content of the message will be
            byte[] objByteMessageToSend = System.Text.Encoding.ASCII.GetBytes(NetworkingVal.findSereverLamp);
            // Send out the message
            objUDPClient.Send(objByteMessageToSend, objByteMessageToSend.Length, new IPEndPoint(IPAddress.Broadcast, NetworkingVal.PORT));
            // Restart the timer
            fTimeLastMessageSent = Time.time;


        }

        //  foreach (ReceivedMessage objMessage in lstReceivedMessages)


         
        if (currentState == enuState.Searching && Time.time > fTimeSearchStarted + fTimeToSearch)
        {
            // We are. Now determine who's gonna be the server.

            // This string holds the ip of the new server. We will start off pointing ourselves as the new server
            string strIPOfServer = IP;
            StopSearching();
            currentState = enuState.Finished;
#if UNITY_EDITOR
            Debug.Log("-------- END OF SEARCH ------");
#endif
            // Next, we loop through the other messages, to see if there are other players that have more right to be the server (based on IP)
            foreach (ReceivedMessage objMessage in lstReceivedMessages)
            {
#if UNITY_EDITOR
                Debug.Log(objMessage.strIP);
#endif
            }
            // StopBroadCasting();
            // If after the loop the highest IP is still our own, call delegate to start a server and stop searching
            // if (strIPOfServer == IP)
            // {
            //     StopSearching();
            // }
            // // If it's not, someone else must start the server. We will simply have to wait as the server is clearly not ready yet
            // else
            // {

            //     // Clear the list and do the search again.
            //     lstReceivedMessages.Clear();
            //     // fTimeSearchStarted = Time.time;
            // }
        }
    
    }

    // Method to start an Asynchronous receive procedure. The UDPClient is told to start receiving.
    // When it received something, the UDPClient is told to call the EndAsyncReceive() method.
    private void BeginAsyncReceive()
    {
        objUDPClient.BeginReceive(new AsyncCallback(EndAsyncReceive), null);
    }
    // Callback method from the UDPClient.
    // This is called when the asynchronous receive procedure received a message
    private void EndAsyncReceive(IAsyncResult objResult)
    {
        bool answer = false;
        // Create an empty EndPoint, that will be filled by the UDPClient, holding information about the sender
        IPEndPoint objSendersIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        // Read the message
        byte[] objByteMessage = objUDPClient.EndReceive(objResult, ref objSendersIPEndPoint);
#if UNITY_EDITOR
        Debug.Log("Start recive mes");
#endif
        // If the received message has content and it was not sent by ourselves...
        if (objByteMessage.Length > 0 &&
            !objSendersIPEndPoint.Address.ToString().Equals(IP))
        {
            // Translate message to string
            string strReceivedMessage = System.Text.Encoding.ASCII.GetString(objByteMessage);
#if UNITY_EDITOR
            Debug.Log("New server found " + objSendersIPEndPoint.Address.ToString());
#endif
            if (strReceivedMessage == NetworkingVal.answerFromServer) 
            {
                // Create a ReceivedMessage struct to store this message in the list
                ReceivedMessage objReceivedMessage = new ReceivedMessage();
                // objReceivedMessage.fTime = Time.time;
                objReceivedMessage.strIP = objSendersIPEndPoint.Address.ToString();
#if UNITY_EDITOR
                Debug.Log(objReceivedMessage.strIP);
#endif

                answer = lstReceivedMessages.Add(objReceivedMessage);
                if (answer &&  (ShowLamp.couldAddlampBut == false))
                {
                    ShowLamp.IPtoADD = objReceivedMessage.strIP;
                    ShowLamp.couldAddlampBut = true;
                }
#if UNITY_EDITOR
                Debug.Log("ADD = " + answer);
#endif
            }
        }
        // Check if we're still searching and if so, restart the receive procedure
        if (currentState == enuState.Searching)
            BeginAsyncReceive();
    }

    // Method to start this object searching for LAN Broadcast messages sent by players, used by the script itself
    private void StartSearching()
    {
        lstReceivedMessages.Clear();
        BeginAsyncReceive();
        fTimeSearchStarted = Time.time;
        currentState = enuState.Searching;
        radar.SetActive (true);
    }
    // Method to stop this object searching for LAN Broadcast messages sent by players, used by the script itself
    private void StopSearching()
    {
        radar.SetActive (false);
        currentState = enuState.NotActive;
    }

    // Method to be called by some other object (eg. a NetworkController) to start a broadcast search
    // It takes two delegates; the first for when this object finds a server that can be connected to, 
    // the second for when this player is determined to start a server itself.
    public void StartSearchBroadCasting()
    {
        // Start a broadcasting session (this basically prepares the UDPClient)
        if (currentState == enuState.NotActive)
        {
            StartBroadcastingSession();
        }
        // Start a search
        StartSearching();
    }

    // Method to start a general broadcast session. It prepares the object to do broadcasting work. Used by the script itself.
    private void StartBroadcastingSession()
    {
        // If the previous broadcast session was for some reason not closed, close it now
        if (currentState != enuState.NotActive) StopBroadCasting();
        // Create the client
        objUDPClient = new UdpClient(NetworkingVal.PORT);
        objUDPClient.EnableBroadcast = true;
        // Reset sending timer
        fTimeLastMessageSent = Time.time;
    }
    // Method to be called by some other object (eg. a NetworkController) to stop this object doing any broadcast work and free resources.
    // Must be called before the game quits!
    public void StopBroadCasting()
    {
        if (currentState == enuState.Searching) StopSearching();

        if (objUDPClient != null)
        {
            objUDPClient.Close();
            objUDPClient = null;
        }
    }
    // Method that calculates a 'score' out of an IP adress. This is used to determine which of multiple clients will be the server. Used by the script itself.
    private long ScoreOfIP(string strIP)
    {
        long lReturn = 0;
        string strCleanIP = strIP.Replace(".", "");
        lReturn = long.Parse(strCleanIP);
        return lReturn;
    }

    public static string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }

    private static void StartServer()
    {
       Debug.Log("Start Server");
    }
    private static void FindServer(string strIP)
    {
        Debug.Log("Find Server");
    }

    public void Testbut()
    {
        Debug.Log("000000000000000000Clcik00000000000000000");
    }

    public enum ADDRESSFAM
    {
        IPv4, IPv6
    }
}