using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyManager : Photon.PunBehaviour {

    public Text statusConnection;
    public Text playerNameText;
    public static readonly string SceneNameGame = "MainLevel";
    public static readonly string SceneNameLobby = "Lobby";
    public static readonly string SceneNameLogin = "Login";

    private string roomName = "myRoom";

    private string ErrorDialog;

    public Text feedbackText;


    private void Awake()
    {
        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.connected)
        {
            SceneManager.LoadScene(LobbyManager.SceneNameLogin);
            return;
        }

        // #Critical
        // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
        PhotonNetwork.autoJoinLobby = false;

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

    }
    // Use this for initialization
    void Start () {
        ConnectionStatus();
        RefreshPlayerName();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ConnectionStatus()
    {
        if (PhotonNetwork.connected)
        {
            Debug.Log("Connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
            statusConnection.text = "Connected";
        }
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                Debug.Log("Connecting to: " + PhotonNetwork.ServerAddress);
                statusConnection.text = "Connecting";
            }
            else
            {
                Debug.Log("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
                statusConnection.text = "Not Connected";
            }
        }
    }


   

    public void RefreshRoomList()
    {
       /*  if (PhotonNetwork.GetRoomList().Length == 0)
                {
                    GUILayout.Label("Currently no games are available.");
                    GUILayout.Label("Rooms will be listed here, when they become available.");
                }
        else
            {
                GUILayout.Label(PhotonNetwork.GetRoomList().Length + " rooms available:");

                // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
                //this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
                foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Label(roomInfo.Name + " " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers);
                    if (GUILayout.Button("Join", GUILayout.Width(150)))
                    {
                        PhotonNetwork.JoinRoom(roomInfo.Name);
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }*/
    }


    public void RefreshPlayerName()
    {
        playerNameText.text = "Welcome: " + PhotonNetwork.playerName;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(this.roomName);
        
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();

        
    }

    //photon Calls
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        
       
    }

    

    public override void OnJoinedRoom()
    {
        
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.room.PlayerCount + " Player(s)");
        Debug.Log("Connected to the room" + PhotonNetwork.room.Name + "with" + PhotonNetwork.room.PlayerCount + "Players");
        PhotonNetwork.LoadLevel(SceneNameGame);
    }


    public void OnPhotonRandomJoinFailed()
    {
        ErrorDialog = "Error: Can't join random room (none found).";
        Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
        CreateRoom();
    }

    void LogFeedback(string message)
    {
        // we do not assume there is a feedbackText defined.
        if (feedbackText == null)
        {
            return;
        }

        // add new messages as a new line and at the bottom of the log.
        feedbackText.text += System.Environment.NewLine + message;
    }

}
