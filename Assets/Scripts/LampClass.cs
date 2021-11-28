using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LampClass : MonoBehaviour
{
    // public 
    public static LampClass instance;

    [SerializeField] private GameObject modePrefab;
    [SerializeField] private GameObject modePanel;

    public enum ConnectionState {
        Conncected,
        NotConnected
    };

    private readonly Dictionary<string, int> modes = new Dictionary<string, int>
    {
        {"Стандартный", 0},
        {"Светлчячки", 1},
        {"костер", 2},
        {"Вертикальный дождь", 3},
        {"Горизонтальный дождь", 4},
        {"Цветики", 5},
        {"Безумие", 6},
        {"Тучки", 7},
        {"Лава", 8},
        {"Плазма", 9},
        {"Дождевой шум", 10},
        {"Дождевые полоски", 11},
        {"Зебра шум", 12},
        {"Лес", 13},
        {"Океан", 14},
        {"Свет", 15},
        {"Снег", 16},
        {"Матрица", 17},
        {"Отблески", 18}
    };

    public struct ChangeSetting
    {
        public int mode;
        public int speed;
        public int scale;
        public int brightness;
        public bool ONflag;
    }

    public bool ChageFlag = false;

    public ConnectionState currentState = ConnectionState.NotConnected;
    public int mode = 0;
    public int speed = 0;
    public int scale = 0;
    public int brightness = 0;
    public bool ONflag = false;

    public ChangeSetting setting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            setting = new ChangeSetting();
        }
        else if (instance != this)
        {
#if UNITY_EDITOR
            Debug.Log("Instance already exists, destroying object!");
#endif
            Destroy(this);
        }
    }

    private void Update()
    {
        if (ChageFlag)
        {
            if (setting.brightness != brightness) 
            {

            }
            if (setting.speed != speed)
            {

            }
            if (setting.scale != scale) 
            {

            }
            if (setting.mode != mode)
            {

            }
            if (setting.ONflag != ONflag)
            {

            }
            ChageFlag = false;
        }
    }


    public void ModeAppear() {
        foreach( KeyValuePair<string, int> kvp in modes )
        {
#if UNITY_EDITOR
            Debug.Log($"Key = {kvp.Key}, Value = {kvp.Value}");
#endif
            if (kvp.Value != 0)
            {
                GameObject button = Instantiate(modePrefab, modePanel.transform);
                GameObject textIP = button.transform.GetChild(0).gameObject;
                textIP.GetComponent<Text>().text = kvp.Key;
            }
        }
    }


}
