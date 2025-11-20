using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class ArduinoControls : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM5", 9600);

    public float leftHand;
    public float rightHand;
    public int dialPos;

    string data = "";
    string toRead = "";
    string value = "";

    bool canRead = false;
    public void StartReading()
    {
        serial.DtrEnable = true;
        serial.ReadTimeout = 5000;
        serial.Open();
        canRead = true;
    }

    public void StopReading()
    {
        canRead = false;
        serial.Close();
    }

    float delay;
    void Update()
    {
        if (canRead && Time.time > delay)
        {
            data = serial.ReadLine();

            toRead = "";

            if (data.Length > 9) {
                for (int i = 0; i < 9; i++)
                    toRead += data[i];
            }

            SortLine(toRead);

            delay = Time.time + 0.2f;
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
        }
    }
}
