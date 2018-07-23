using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

    public Text statusText;
    public Text loginInput;
    public static readonly string SceneNameLobby = "Lobby";


    private bool connectFailed = false;

    // Use this for initialization
    void Start () {
        CheckConnection();
	}
	
	// Update is called once per frame
	void Update () {
    
    }

    public  void ServerConnect()
    {
        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated)
        {
            //HERE WE CONNECT TO THE SERVER USING 
            // Connect to the photon master-server.We use the settings saved in PhotonServerSettings(a.asset file in this project)
            PhotonNetwork.ConnectUsingSettings("0.9");
            PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
        }
        CheckConnection();
    }

    public void CheckConnection()
    {
        if (PhotonNetwork.connected)
        {
            Debug.Log("Connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
            statusText.text = "Connected";
        }
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                Debug.Log("Connecting to: " + PhotonNetwork.ServerAddress);
                statusText.text = "Connecting";
            }
            else
            {
                Debug.Log("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
                statusText.text = "Not Connected";
            }
        }

        if (this.connectFailed)
        {
            GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
            GUILayout.Label(String.Format("Server: {0}", new object[] { PhotonNetwork.ServerAddress }));
            GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID.Substring(0, 8) + "****"); // only show/log first 8 characters. never log the full AppId.

            if (GUILayout.Button("Try Again", GUILayout.Width(100)))
            {
                this.connectFailed = false;
                PhotonNetwork.ConnectUsingSettings("0.9");
            }
        }
    }


    public void OnConnectedToMaster()
    {
        Debug.Log("As OnConnectedToMaster() got called, the PhotonServerSetting.AutoJoinLobby must be off. Joining lobby by calling PhotonNetwork.JoinLobby().");
        PhotonNetwork.JoinLobby();
        CheckConnection();
        SetPlayerName();
        PhotonNetwork.LoadLevel(SceneNameLobby);
    }

    public void SetPlayerName() {

        PhotonNetwork.playerName = loginInput.text;
        PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
    }



    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }



    void FixedUpdate()
    {
        
    }


}

