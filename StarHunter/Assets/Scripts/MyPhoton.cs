using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    public Vector3 spawn1 = new Vector3(7.5f, 6.6f, 12.5f);
    public Vector3 spawn2 = new Vector3(11f, 6.6f, -15.5f);
    public Vector3 spawn3 = new Vector3(-20f, 4.8f, -6.5f);
    public Vector3 spawn4 = new Vector3(-17.5f, 4.8f, 13.5f);

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Room 2", roomOptions, null);
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
                spawn = spawn1;
                break;
            case 2:
                spawn = spawn2;
                break;
            case 3:
                spawn = spawn3;
                break;
            case 4:
                spawn = spawn4;
                break;
        }


        GameObject newPlayer = PhotonNetwork.Instantiate("Character1", spawn, Quaternion.identity);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform;
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().LookAt = newPlayer.transform;

        // GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>().Follow = newPlayer.transform;
        //GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>().LookAt = newPlayer.transform;
        newPlayer.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().transform.localPosition = new Vector3(0f, 0f, 0f);

        if (PhotonNetwork.PlayerList.Length==1)
        {
            GameObject star = newPlayer.transform.Find("StarGhost").gameObject;
            star.GetComponent<MeshRenderer>().enabled = true;
            print(PhotonNetwork.IsMasterClient);
            newPlayer.GetComponent<MeshRenderer>().material = mRed;
        }

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            newPlayer.GetComponent<MeshRenderer>().material = mGreen;
        }

        if (PhotonNetwork.PlayerList.Length == 3)
        {
            newPlayer.GetComponent<MeshRenderer>().material = mBlue;
        }

        if (PhotonNetwork.PlayerList.Length == 4)
        {
            newPlayer.GetComponent<MeshRenderer>().material = mYellow;
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
