using UnityEngine;
using System.IO.Ports;
using System.Collections;
using System.Threading;
using UnityEngine.Events;
using System;

public class ArduinoControls : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM7", 9600);
    Thread serialThread;

    public float leftHand;
    public float rightHand;
    public int dialPos;
    int oldDial;

    string data = "";
    string toRead = "";
    string value = "";
    public bool buttonPressed = false;

    bool canRead = false;

    public static Action OnButtonPressed;

    void Start()
    {
        serial.DtrEnable = true;
        serial.ReadTimeout = 100;
        serial.Open();
        serialThread = new Thread(new ThreadStart(ReadFromPort));
        serialThread.IsBackground = true;
        serialThread.Start();
        canRead = true;
    }

    public void StopReading()
    {
        canRead = false;
        serial.Close();
    }

    private void Update()
    {
        if (dialPos != oldDial)
        {
            if (FindFirstObjectByType<MinigameRules>() != null)
                FindFirstObjectByType<MinigameRules>().SelectCard(dialPos);
            oldDial = dialPos;
        }

        if (FindFirstObjectByType<MinigameRules>() == null)
            buttonPressed = false;
    }

    void ReadFromPort()
    {
        while (true) {
            if (serial.IsOpen)
            {
                try
                {
                    data = serial.ReadLine();

                    toRead = "";

                    if (data.Length > 9)
                    {
                        for (int i = 0; i < 9; i++)
                            toRead += data[i];
                    }

                    SortLine(toRead);
                }
                catch (System.Exception)
                {

                }
            }
        }
    }

    void SortLine(string readThis)
    {
        switch (readThis)
        {
            case "DistanceL":
                value = "";
                for (int i = 1; i < data.Length; i++)
                {
                    if (i > 11)
                        value += data[i];
                }
                leftHand = float.Parse(value) * 0.01f;
                break;
            case "DistanceR":
                value = "";
                for (int i = 1; i < data.Length; i++)
                {
                    if (i > 11)
                        value += data[i];
                }
                rightHand = float.Parse(value) * 0.01f;
                break;
            case "dialValue":
                value = "";
                for (int i = 1; i < data.Length; i++)
                {
                    if (i > 11)
                        value += data[i];
                }
                dialPos = int.Parse(value);
                break;
            case "butnValue":
                buttonPressed = true;
                break;
        }
    }
}
