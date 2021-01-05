using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class MyPhoton : MonoBehaviourPunCallbacks
{


    //public GameObject star;
    public Material mRed;
    public Material mGreen;
    public Material mBlue;
    public Material mYellow;

    //score system
   // public static int scoreRed = 0;
   // GameObject scoreboard;




    public Transform[] spawnPositions = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //scoreboard = GameObject.Find("Canvas").transform.Find("Score").gameObject;
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Show", roomOptions, null);
    }

    public override void OnJoinedRoom()
    {
        //if (PhotonNetwork.PlayerList.Length==1)
        //{
        // GameObject star = PhotonNetwork.Instantiate("Star", new Vector3(0, 0, 0), Quaternion.identity);
        //}
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

       


        if (PhotonNetwork.PlayerList.Length==1)
        {
           // GameObject star = newPlayer.transform.Find("StarGhost").gameObject;
            
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

        // if (newPlayer.GetComponent<PhotonView>().ViewID == 1002)
        //{
        //  print("PEEETER");
        //  GameObject star = PhotonView.Find(1001).gameObject;
        //  star.transform.parent = newPlayer.transform;
        //  star.transform.localPosition = new Vector3(0, 2.1f, 0);


        // if (GameObject.Find("Star")==null) {
        //     print("here");
        //      GameObject star = PhotonNetwork.Instantiate("Star", new Vector3(0, 0, 0), Quaternion.identity);
        //      star.transform.parent = newPlayer.transform;
        //star.transform.localPosition = new Vector3(0, 2.1f, 0);
        ///   }
    }

    // Update is called once per frame
    void Update()
    {

       
            
    }
}
