using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonNetworkController : MonoBehaviourPunCallbacks
{
    [Header("Players")]
    public GameObject Player1ON;
    public GameObject Player1OFF;
    public Text ReadyDisplay;
    string tempPlayerName;
    [Header("Lobby")]
    public Text lobbyPn;
    private string roomTempName;
    [Header("Player")]
    public GameObject playerObj;
    public GameObject mainCamera;


    private void Start()
    {
        lobbyPn.gameObject.SetActive(true);

        Player1ON.gameObject.SetActive(false);
        Player1OFF.gameObject.SetActive(true);
        ReadyDisplay.gameObject.SetActive(true);

        roomTempName = "Lobby: " + Random.Range(1, 10);
        tempPlayerName = "Player " + Random.Range(1, 20);

        Login();
    }

    public void Login()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = tempPlayerName;

        buscarPartidaRapida();

    }
    // BUTTONS //

    public void buscarPartidaRapida()
    {
        PhotonNetwork.JoinLobby();
    }
    public void sairDoLobby()
    {
        Debug.LogWarning("Saiu do Lobby");
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();

        Player1ON.gameObject.SetActive(false);
        Player1OFF.gameObject.SetActive(true);
        ReadyDisplay.color = Color.red;
    }

    public override void OnConnected()
    {
        //base.OnConnected();
        Debug.LogWarning("OnConnected");
    }
    public void createRoom()
    {
        //base.OnCreatedRoom();
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomTempName, roomOptions, TypedLobby.Default);
    }

    // PUN CALLBACKS //
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.LogWarning("OnConnectedToMaster");
        Debug.LogWarning("Server Region:" + PhotonNetwork.CloudRegion);
        Debug.LogWarning("Ping:" + PhotonNetwork.GetPing());
  
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        Debug.LogWarning("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        Debug.LogError("Erro ao entrar na sala");
        createRoom();
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        Debug.LogWarning("OnJoinedRoom");
        Debug.LogWarning("Nome da Sala: " + PhotonNetwork.CurrentRoom.Name);
        Debug.LogWarning("Nome da Player: " + PhotonNetwork.NickName);
        Debug.LogWarning("Players Conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        Player1ON.gameObject.SetActive(true);
        Player1OFF.gameObject.SetActive(false);
        lobbyPn.text = roomTempName;
        lobbyPn.fontSize = 14;
        ReadyDisplay.color = Color.green;

    }


}
