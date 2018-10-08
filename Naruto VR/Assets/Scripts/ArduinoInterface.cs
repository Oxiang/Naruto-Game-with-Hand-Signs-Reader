using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoInterface : MonoBehaviour {

    // Use this for initialization
    public String message;
    SerialPort stream;
    private String portName = "\\\\.\\COM14";
    int baudRate = 9600;
    float timeOut = 500f;


    void Start () {
        //stream = new SerialPort(portName, baudRate); // Windows need the 4 slashes and 2 slashes
        //stream.ReadTimeout = 50;
        //stream.Open();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void getInputs(string msg)
    {
        stream = new SerialPort(portName, baudRate); // Windows need the 4 slashes and 2 slashes
        stream.ReadTimeout = 50;
        stream.Open();
        WriteToArduino(msg);
        StartCoroutine(AsynchronousReadFromArduino(
            (string s) => Debug.Log(s),
            () => Debug.LogError("error!"),
            timeOut    //Milliseconds e.g. 10000f is 10seconds
            ));
        stream.Close();
    }

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public string ReadfromArduino(int timeout = 10)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch(System.TimeoutException)
        {
            return null;
        }
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback,
        Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;
        do
        {
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                message = dataString;
                yield break; //Terminates the Coroutine
            }
            else
            {
                message = "";
                yield return null; //Wait for next frame
            }

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;
        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        message = "";
        yield return null;
    }
}
