using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public PhotonView photonview;
    public Camera myCamera;

    [Header("NOME")]
    public Text playerName;

    void Start()
    {
        photonview = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            myCamera.gameObject.SetActive(false);
        }

        Debug.LogWarning("Name: " + PhotonNetwork.NickName + " PhotonView: " + photonview.IsMine);
        playerName.text = PhotonNetwork.NickName;
    }
}
   
