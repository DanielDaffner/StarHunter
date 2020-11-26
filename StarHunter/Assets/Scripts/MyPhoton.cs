using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class MyPhoton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, null);
    }

    public override void OnJoinedRoom()
    {
        GameObject newPlayer = PhotonNetwork.Instantiate("Character1", new Vector3(13,4,-12), Quaternion.identity);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().transform.parent = newPlayer.transform;
        //GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().LookAt = newPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
