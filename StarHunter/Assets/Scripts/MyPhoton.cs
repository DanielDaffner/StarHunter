using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class MyPhoton : MonoBehaviourPunCallbacks
{
    //lobbyMenu Player Names Test

    public static TMP_Text player1;
    public static TMP_Text player2;
    public static TMP_Text player3;
    public static TMP_Text player4;

    public InputField lobbyname;

    public GameObject game;
    public GameObject menu;
    public GameObject mainMenu;
    public GameObject JoinCreateLobby;
    public GameObject lobbyMain;
    public GameObject lobbyMainButton;
    public GameObject startButton;

    private int playerNumber;
    private bool connected;


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
        Cursor.lockState = CursorLockMode.Confined;
       

    }

   public void connectToMaster() { 

     PhotonNetwork.ConnectUsingSettings();

    }
    

    public override void OnConnectedToMaster()
    {
        // now connected to main server

      mainMenu.SetActive(false);
    JoinCreateLobby.SetActive(true);

}

    public void createPrivateLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom(lobbyname.text, roomOptions, null);
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
        connected = true;
        playerNumber = PhotonNetwork.PlayerList.Length;
        GetComponent<PhotonView>().RPC("showNamesinLobby", RpcTarget.All);
        lobbyMainButton.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
     base.OnJoinRoomFailed(returnCode, message);
        returnToStartMenu();    
    }

    [PunRPC]

    public void showNamesinLobby()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playersText[i].SetText(PhotonNetwork.PlayerList[i].ToString());
        }
    }

    [PunRPC]
    public void decreasePlayerNumber()
    {
        playerNumber--;
        print(playerNumber);
    }

    [PunRPC]
    public void hostLeft()
    {
        exit_gracefully();
        returnToStartMenu();
    }



    public void startGame()
    {


        GetComponent<PhotonView>().RPC("gameOn", RpcTarget.AllBuffered);
    }

    //ausgelagert für rpc
    
 

    [PunRPC]
    public void gameOn()
    {   //

        menu.SetActive(false);
        game.SetActive(true);

        //
        Vector3 spawn = new Vector3();

        print("playernumber at gamestart" + playerNumber);
        switch (playerNumber)
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


        if (playerNumber == 1)
        {


            print(PhotonNetwork.IsMasterClient);
            
            newPlayer.transform.Find("StarGhost").GetComponent<PhotonView>().RPC("switchOn", RpcTarget.AllBuffered);
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialRed", RpcTarget.AllBuffered);
        }

        if (playerNumber == 2)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialGreen", RpcTarget.AllBuffered);
        }

        if (playerNumber == 3)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialBlue", RpcTarget.AllBuffered);
        }

        if (playerNumber == 4)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialYellow", RpcTarget.AllBuffered);
        }
    }

  

    // Update is called once per frame
    void Update()
    {

        print("isMaster"+ PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
            startButton.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!JoinCreateLobby.activeSelf&&!mainMenu.activeSelf) {

                // if (PhotonNetwork.IsMasterClient) {

                //      print("leave when with master");
                //    GetComponent<PhotonView>().RPC("hostLeft", RpcTarget.AllViaServer);
                //     return;
                //   }
                //  else
                GetComponent<PhotonView>().RPC("decreasePlayerNumber", RpcTarget.AllViaServer);
                exit_gracefully();
            }

            returnToStartMenu();
            
        }

    }

    public void exit_gracefully()
    {
        connected = false;
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
       // PhotonNetwork.LeaveRoom();
      //  PhotonNetwork.LoadLevel(0);
        PhotonNetwork.Disconnect();
    
}

 
    public void returnToStartMenu()
{
        PhotonNetwork.Disconnect();
        mainMenu.SetActive(true);
    JoinCreateLobby.SetActive(false);
    lobbyMainButton.SetActive(false);
    lobbyMain.SetActive(false);
        startButton.SetActive(false);
    game.SetActive(false);
    menu.SetActive(true);
    Cursor.lockState = CursorLockMode.Confined;
}

    public void mainMenuQuit()
    {
        Application.Quit();
       
    }
    public void lobbyQuit()
    {
        GetComponent<PhotonView>().RPC("decreasePlayerNumber", RpcTarget.AllViaServer);
    }

}