using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using System.Text;
using System.Threading;

public class PortCom : MonoBehaviour {

    public int bauds = 9600;
    private SerialPort sp;
    private string comPort;
    public server _server;

    private string tirRestants = "TIRS_RESTANTS";
    private string answerRestants = "RESTANTS";
    private string demandeTir = "DEMANDE_TIR";
    private string answerGains= "GAINS";

    private Thread portComThread; 

    void Start()
    {
        comPort = FindMyPortAndConnect();
        portComThread = new Thread(new ThreadStart(mainThread));
        portComThread.IsBackground = true;
        portComThread.Start();
    }

    private string FindMyPortAndConnect()
    {
        string[] comPorts = SerialPort.GetPortNames();
        if (comPorts.Length == 0)
        {
            Debug.Log("No com ports.");
            return null;
        }
        foreach (string com in comPorts)
        {
            Debug.Log(com);
            if (InitCOM(com))
            {
                return com;
            }
        }
        return null;
    }

    private bool InitCOM(string comPort)
    {
        if (sp != null && sp.IsOpen)
        {
            sp.Close();
            Debug.Log("Close Port " + comPort + " before connecting");
        }

        sp = new SerialPort(comPort, bauds, Parity.None, 8, StopBits.One);
        try
        {
            sp.Open();
        }
        catch(Exception err)
        {
            Debug.Log(err);
        }
        if (sp.IsOpen)
        {
            sp.ReadTimeout = 10000;
            Debug.Log("COM port Open");
            return true;
        }
        
        return false;
    }

    private void OnApplicationQuit()
    {
        sp.Close();
        portComThread.Abort();
    }

    void mainThread()
    {
        bool stopThread = false;
        Debug.Log("ConnectThread");
        while (!stopThread)
        {
            try
            {
                if (sp.IsOpen)
                {
                    string tempS = sp.ReadLine();
                    Debug.Log("SerialData : " + tempS);
                    string[] splitTempS = tempS.Split(':');
                    if (splitTempS.Length < 2)
                        continue;
                    if (splitTempS[1] == tirRestants)
                    {
                        
                        Client client = _server.GetClient(splitTempS[0]);
                        if (client != null)
                        {
                            string answer = splitTempS[0] + answerRestants + client.nbRemainingBet;
                            Debug.Log("tirRestants :" + answer);
                            sp.WriteLine(answer);
                        }
                    }
                    else if (splitTempS[1] == demandeTir)
                    {
                        float gain = _server.Tir(splitTempS[0]);
                        string answer = splitTempS[0] + answerGains + gain;
                        Debug.Log("demandeTir : " + answer);
                        sp.WriteLine(answer);
                    }
                }
                else
                {
                    Debug.Log("Com not open");
                    Thread.Sleep(2000);
                    FindMyPortAndConnect();
                }
            }
            catch (Exception err)
            {
                Debug.LogError("err " + err.ToString());
                stopThread = true;
            }
        }
    }


}