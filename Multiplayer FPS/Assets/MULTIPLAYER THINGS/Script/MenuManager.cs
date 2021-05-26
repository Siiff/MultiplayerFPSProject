﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public GameObject canvasShearch;
    public GameObject canvasWaiting;
    public GameObject canvasCountdown;
    public GameObject canvasLoading;

    public GameObject player;
    public Transform[] spawnPosition;

    int spawnPositionUsed;

    void Start()
    {
        PanelControler(canvasShearch.name);

        PhotonNetwork.SendRate = 25;
        PhotonNetwork.SerializationRate = 15;
    }

    void PanelControler(string activePanel)
    {
        canvasShearch.SetActive(activePanel.Equals(canvasShearch.name));
        canvasWaiting.SetActive(activePanel.Equals(canvasWaiting.name));
        canvasCountdown.SetActive(activePanel.Equals(canvasCountdown.name));
        canvasLoading.SetActive(activePanel.Equals(canvasLoading.name));
    }

    public void ButtonShearch()
    {
        PanelControler(canvasLoading.name);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        string roomName = "Sala-" + Random.Range(100, 10000);

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PanelControler(canvasWaiting.name);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            spawnPositionUsed = 0;
        }
        else
        {
            spawnPositionUsed = 1;
        }
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {

        Hashtable props = new Hashtable
        {
            { CountdownTimer.CountdownStartTime, (float)PhotonNetwork.Time }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
        PanelControler(canvasCountdown.name);
    }

    void ContdownAction()
    {
        PanelControler("");
        PhotonNetwork.Instantiate(player.name, spawnPosition[spawnPositionUsed].position, spawnPosition[spawnPositionUsed].rotation);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        CountdownTimer.OnCountdownTimerHasExpired += ContdownAction;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        CountdownTimer.OnCountdownTimerHasExpired -= ContdownAction;
    }
}