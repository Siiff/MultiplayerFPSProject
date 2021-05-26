using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("LOGIN")]
    public GameObject loginPn;
    public InputField playerNameInput;
    string tempPlayerName;

    [Space]
    [Header("LOBBY")]
    public GameObject lobbyPn;
    public InputField roomNameInput;
    public Text lobbyNumber;
    string tempRoomName;

    [Space]
    [Header("Start")]
    public GameObject startPN;
    public GameObject Player1;
    public GameObject Player2;


    [Space]
    [Header("PLAYER")]
    public GameObject playerPUN;
    public GameObject mainCamera;
    public GameObject LobbyPreFab;

    [Space]
    [Header("Menssagem na Tela")]
    public Text msgText;
    public string msgEntrada = " Entrou na Sala!";
    public string msgSaida = " Saiu da Sala!";


    void Start()
    {
        Debug.Log("Arma " + PlayerPrefs.GetInt("Arma"));
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Arma " + PlayerPrefs.GetInt("Arma"));
        loginPn.gameObject.SetActive(true);
        lobbyPn.gameObject.SetActive(false);
        startPN.gameObject.SetActive(false);
        msgText.gameObject.SetActive(false);

        tempPlayerName = "Player: " + Random.Range(1, 10);
        playerNameInput.text = tempPlayerName;

        tempRoomName = "Sala: " + Random.Range(1, 10);
    }

    //######## Minhas Funções ##################
    public void Login()
    {
        PhotonNetwork.ConnectUsingSettings();

        if (playerNameInput.text != "")
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        else
        {
            PhotonNetwork.NickName = tempPlayerName;
        }

        loginPn.gameObject.SetActive(false);
        lobbyPn.gameObject.SetActive(true);
        roomNameInput.text = tempRoomName;
    }

    public void QuickSearch()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
    }

    public void ExibeMsg(string msg)
    {
        msgText.gameObject.SetActive(true);
        msgText.text = msg;
        StartCoroutine(WaitForHide(4f));
    }

    IEnumerator WaitForHide(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        msgText.gameObject.SetActive(false);
    }

    //############# PUN Callbacks ##################
    public override void OnConnected()
    {
        Debug.LogWarning("############# LOGIN #############");
        Debug.LogWarning("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.LogWarning("OnConnectedToMaster");
        Debug.LogWarning("Server: " + PhotonNetwork.CloudRegion);
        Debug.LogWarning("Ping: " + PhotonNetwork.GetPing());
    }

    public override void OnJoinedLobby()
    {
        Debug.LogWarning("############# LOOBY #############");
        Debug.LogWarning("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(tempRoomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning("OnJoinedRoom");
        Debug.LogWarning("Nome da Sala: " + PhotonNetwork.CurrentRoom.Name);
        Debug.LogWarning("Nome da Player: " + PhotonNetwork.NickName);
        Debug.LogWarning("Players Conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        lobbyNumber.text = roomNameInput.text;

        loginPn.gameObject.SetActive(false);
        lobbyPn.gameObject.SetActive(false);
        startPN.gameObject.SetActive(true);
        lobbyNumber.gameObject.SetActive(true);
        //LobbyPreFab.gameObject.SetActive(false);
        Player1.gameObject.SetActive(true);
        Player2.gameObject.SetActive(true);
        
        //SPAWN,, MODIFICAR//
        /*Vector3 pos = new Vector3(Random.Range(-1f, -5f), playerPUN.transform.position.y, Random.Range(5, 8));

        PhotonNetwork.Instantiate(playerPUN.name, pos, playerPUN.transform.rotation, 0);*/
    }

    public void StartMatch() { 

    }

    public void LeftRoom()
    {
        Debug.Log("DESCONECTADO");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
    }
    public void OnPlayerEnteredRoom(PlayerController p1)
    {
        Debug.LogWarning("ENTROU NA SALA");
        ExibeMsg(p1.playerName + msgEntrada);
    }

    public void OnPlayerLeftRoom(PlayerController p1)
    {
        Debug.LogWarning("SAIU DA SALA");
        ExibeMsg(p1.playerName + msgSaida);
    }
}
