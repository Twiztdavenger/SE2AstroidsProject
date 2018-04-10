using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    public Button serverBtn;
    public Button clientBtn;
    

    NetworkClient myClient;
    bool isSetup = false;
    bool client = false;

    // Use this for initialization
    void Start () {
        NetworkServer.Reset();
    }
	
	// Update is called once per frame
	void Update () {
        Button svr = serverBtn.GetComponent<Button>();
        svr.onClick.AddListener(ServerButton);

        Button cli = clientBtn.GetComponent<Button>();
        cli.onClick.AddListener(ClientButton);
    }

    void ServerButton()
    {
        if(!isSetup)
        {
            SetupServer();
        }
        
    }

    void ClientButton()
    {
        if(!client)
        {
            SetupClient();
        }
    }

    // Create a server and listen on a port
    public void SetupServer()
    {
        int port = 1111;
        NetworkServer.Listen(port);
        Debug.Log("Listening on port " + port);
        isSetup = true;
    }

    public void SetupClient()
    {
        /*
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 1111);*/

        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);

        if (myClient.isConnected)
        {
            client = true;
            Debug.Log("Connected to server");
        } else
        {
            Debug.Log("Not Connected");
            NetworkServer.Reset();
        }
        
    }

    // client function
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
