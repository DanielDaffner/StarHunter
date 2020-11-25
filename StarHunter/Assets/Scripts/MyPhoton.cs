using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class MyPhoton : MonoBehaviourPunCallbacks
{


    //public GameObject star;
  

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
        //if (PhotonNetwork.PlayerList.Length==1)
        //{
           // GameObject star = PhotonNetwork.Instantiate("Star", new Vector3(0, 0, 0), Quaternion.identity);
        //}

        GameObject newPlayer = PhotonNetwork.Instantiate("Character1", new Vector3(13,4,-12), Quaternion.identity);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform;
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().LookAt = newPlayer.transform;


        if (PhotonNetwork.PlayerList.Length==1)
        {
            GameObject star = newPlayer.transform.Find("StarGhost").gameObject;
            star.GetComponent<MeshRenderer>().enabled = true;
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
