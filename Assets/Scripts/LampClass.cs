using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampClass : MonoBehaviour
{
    // public 

    public enum ConnectionState {
        Conncected,
        NotConnected
    };

    public ConnectionState currentState = ConnectionState.NotConnected;
}
