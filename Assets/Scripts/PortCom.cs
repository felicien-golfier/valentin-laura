#if !UNITY_ANDROID
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
    public ServerLaura _server;

    private string tirRestants = "TIRS_RESTANTS";
    private string answerRestants = "RESTANTS";
    private string demandeTir = "DEMANDE_TIR";
    private string answerGains= "GAINS";

    private Thread portComThread; 

    void Start()
    {
        if (Tools.instance.ImServer)
        {
            _server = Tools.instance.serverLaura;
            comPort = FindMyPortAndConnect();
            portComThread = new Thread(new ThreadStart(mainThread));
            portComThread.IsBackground = true;
            portComThread.Start();
        }
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
        if (sp != null)
            sp.Close();
        if (portComThread != null)
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
                if (sp != null && sp.IsOpen)
                {
                    string tempS;
                    try
                    {
                         tempS = sp.ReadLine();
                    }catch(Exception err)
                    {
                        Debug.Log("CONTINUE\n"+err);
                        continue;
                    }
                    Debug.Log("SerialData : " + tempS);



                    if (tempS.Contains(tirRestants))
                    {
                        string id = tempS.Replace(tirRestants,"");
                        id = id.Substring(0, id.Length - 1);
                        ClientLaura client = _server.GetClientByID(id);
                        if (client != null)
                        {
                            string answer = id + " "+ answerRestants + "["+client.nbRemainingBet+"]";
                            _server.log("Client "+id+" ping. He has " + client.nbRemainingBet +" remaining bets");
                            Debug.Log("answerRestants : " + answer);
                            sp.WriteLine(answer);
                        }
                        else _server.log("The id " + id +" doesn't exist");
                    }
                    else if (tempS.Contains(demandeTir))
                    {
                        string id = tempS.Replace(demandeTir,"");
                        id = id.Substring(0, id.Length - 1);
                        float gain = _server.Tir(id);
                        if (gain == -1)
                        {
                            Debug.LogError("Client not recognized");
                            continue;
                        }
                        string answer = id + " " + answerGains + "[" + gain + "]";
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
#endif