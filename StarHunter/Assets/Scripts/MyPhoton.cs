using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MyPhoton : MonoBehaviourPunCallbacks
{
    //lobbyMenu Player Names Test

    public static TMP_Text player1;
    public static TMP_Text player2;
    public static TMP_Text player3;
    public static TMP_Text player4;

    public TMP_Text[] playersText = { player1, player2, player3, player4 };

    //public GameObject star;
    public Material mRed;
    public Material mGreen;
    public Material mBlue;
    public Material mYellow;


    public Transform[] spawnPositions = new Transform[4];

    // Start is called before the first frame update
  void Start()
    {
        //connect to master server
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        // check for lobbys

    
     
    }

    public void createLobby(string name)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(name, roomOptions, null);
        // TODO :: switch menu to lobby

    }

    public void createLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Main", roomOptions, null);
        // TODO :: switch menu to lobby

    }

    public override void OnJoinedRoom()
    {
        // wait for start
        // display information
        //startGame();

        GetComponent<PhotonView>().RPC("showNamesinLobby", RpcTarget.AllBuffered);

    }

    [PunRPC]

    public void showNamesinLobby()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playersText[i].SetText(PhotonNetwork.PlayerList[i].ToString());
        }
    }

    public void startGame()
    {
        Vector3 spawn = new Vector3();
        switch (Random.Range(1, 4))
        {

            case 1:
                spawn = spawnPositions[0].position;
                break;
            case 2:
                spawn = spawnPositions[1].position;
                break;
            case 3:
                spawn = spawnPositions[2].position;
                break;
            case 4:
                spawn = spawnPositions[3].position;
                break;
        }


        GameObject newPlayer = PhotonNetwork.Instantiate("Character1", spawn, Quaternion.identity);
        newPlayer.transform.Rotate(Vector3.up, -90);

        GameObject.Find("CM vcam1").GetComponent<CinemachineFreeLook>().Follow = newPlayer.transform;
        GameObject.Find("CM vcam1").GetComponent<CinemachineFreeLook>().LookAt = newPlayer.transform;


        if (PhotonNetwork.PlayerList.Length == 1)
        {
   

            print(PhotonNetwork.IsMasterClient);
            newPlayer.transform.Find("StarGhost").GetComponent<PhotonView>().RPC("switchOn", RpcTarget.AllBuffered);
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialRed", RpcTarget.AllBuffered);
        }

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialGreen", RpcTarget.AllBuffered);
        }

        if (PhotonNetwork.PlayerList.Length == 3)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialBlue", RpcTarget.AllBuffered);
        }

        if (PhotonNetwork.PlayerList.Length == 4)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialYellow", RpcTarget.AllBuffered);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
