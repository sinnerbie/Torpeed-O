using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class ArduinoControls : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM3", 9600);

    public float leftHand;
    public float rightHand;
    public int dialPos;

    string data = "";
    string toRead = "";
    string value = "";

    void Start()
    {
        serial.Open();
        serial.ReadTimeout = 100;
    }

    void Update()
    {
        data = serial.ReadLine();

        for (int i = 0; i < 9; i++)
            toRead += data[i];

        switch (toRead)
        {
            case "DistanceL":
                value = "";
                for (int i = 1; i < data.Length; i++)
                {
                    if (i > 11)
                        value += data[i];
                }
                leftHand = float.Parse(value);
                break;
            case "DistanceR":
                value = "";
                for (int i = 1; i < data.Length; i++)
                {
                    if (i > 11)
                        value += data[i];
                }
                rightHand = float.Parse(value);
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
        }
    }
}
