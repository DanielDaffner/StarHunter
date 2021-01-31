using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public GameObject startStar;

    public Camera cam;
    Vector3 camPos;
    Quaternion camRot;

    public int playerNumber;
    private bool connected;
     

    public TMP_Text[] playersText = { player1, player2, player3, player4 };

    //public GameObject star;
    public Material mRed;
    public Material mGreen;
    public Material mBlue;
    public Material mYellow;


    public Transform[] spawnPositions = new Transform[4];

    public GameObject gameOverImage;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        camPos = cam.transform.position;
        camRot = cam.transform.rotation;
        //connect to master server
        // Cursor.lockState = CursorLockMode.Confined;
     
        Cursor.visible = true;

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
       // GetComponent<PhotonView>().RPC("showNamesinLobby", RpcTarget.All);
        lobbyMainButton.SetActive(true);
      //  PhotonNetwork.LocalPlayer.n
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
     base.OnJoinRoomFailed(returnCode, message);
        connected = false;
        returnToStartMenu();    
    }

    

    public void showNamesinLobby()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playersText[i].SetText("Player"+(i+1));
        }
        for (int i = 3; i > PhotonNetwork.PlayerList.Length-1; i--)
        {
            playersText[i].SetText("Empty");
        }
        print("PlayerList Length for fields " + PhotonNetwork.PlayerList.Length);
    }

    [PunRPC]
    public void decreasePlayerNumber(string number)
    {
        int result;
        int.TryParse(number, out result);
        if (playerNumber>result)
        playerNumber--;
        print("playerNumber after decrease" +playerNumber);

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
        newPlayer.tag = "OwnPlayer";
        GameObject.Find("CM vcam1").GetComponent<CinemachineFreeLook>().Follow = newPlayer.transform;
        GameObject.Find("CM vcam1").GetComponent<CinemachineFreeLook>().LookAt = newPlayer.transform;

        //print("Actor Number" +PhotonNetwork.LocalPlayer.ActorNumber);
        if (playerNumber==1)
        {


            print(PhotonNetwork.IsMasterClient);
            
           // newPlayer.transform.Find("StarGhost").GetComponent<PhotonView>().RPC("switchOn", RpcTarget.AllBuffered);
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialRed", RpcTarget.AllBuffered);
        }

       else if (playerNumber == 2)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialGreen", RpcTarget.AllBuffered);
        }

       else if (playerNumber == 3)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialBlue", RpcTarget.AllBuffered);
        }

        else if (playerNumber == 4)
        {
            newPlayer.GetComponent<PhotonView>().RPC("setMaterialYellow", RpcTarget.AllBuffered);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (lobbyMain.activeSelf&&connected)
        {
            showNamesinLobby();
       
        print("playlistlength" + PhotonNetwork.PlayerList.Length);
        //print(playerNumber);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
            }
        }
        if (game.activeSelf) { 
        if (Points.scores[playerNumber-1] > 99)
        {
           
            GetComponent<PhotonView>().RPC("endGame", RpcTarget.AllBuffered);
        }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           returnToStartMenu();   
        }

    }

   

    [PunRPC]
    public void addScore(int number)
    {
        print("Score soll hochzählen bei " + number);
        switch (number) {
            case 1:
                Points.score1++;
                break;
            case 2:
                Points.score2++;
                break;
            case 3:
                Points.score3++;
                break;
            case 4:
                Points.score4++;
                break;
            default:
                break;
        }
  
    }

    public void returnToStartMenu()
{ 
        
        if (lobbyMain.activeSelf&&connected&&!game.activeSelf)
        {
            GetComponent<PhotonView>().RPC("decreasePlayerNumber", RpcTarget.All, playerNumber.ToString());
            PhotonNetwork.SendAllOutgoingCommands();
            //GetComponent<PhotonView>().RPC("showNamesinLobby", RpcTarget.All);
            //PhotonNetwork.SendAllOutgoingCommands();
        }

     

        //     float time = 10.5f;
        //    while (time > 0)
        //     {
        //         print("peter" + time);
        //         time -= Time.deltaTime;
        //       }
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        //PhotonNetwork.SendAllOutgoingCommands();
        PhotonNetwork.Disconnect();
        mainMenu.SetActive(true);
        JoinCreateLobby.SetActive(false);
        lobbyMainButton.SetActive(false);
        startButton.SetActive(false);
        lobbyMain.SetActive(false);
        game.SetActive(false);
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        cam.transform.position = camPos;
        cam.transform.rotation = camRot;
        cam.farClipPlane = 5000;
        Points.score1 = 0;
        Points.score2 = 0;
        Points.score3 = 0;
        Points.score4 = 0;
        startStar.GetComponent<MeshRenderer>().enabled = true;
    }

    [PunRPC]
    public void endGame()
    {
        gameOverImage.SetActive(true);
        switch (playerNumber)
        {
            case 1:
                if (Points.score1 == 100) 
                    { 
                    gameOverText.GetComponent<Text>().text = "You won!";
                }
                else {
                    gameOverText.GetComponent<Text>().text = "You lost.";
                }
                    break;
            case 2:
                if (Points.score2 == 100)
                {
                    gameOverText.GetComponent<Text>().text = "You won!";
                }
                else
                {
                    gameOverText.GetComponent<Text>().text = "You lost.";
                }
                break;
            case 3:
                if (Points.score3 == 100)
                {
                    gameOverText.GetComponent<Text>().text = "You won!";
                }
                else
                {
                    gameOverText.GetComponent<Text>().text = "You lost.";
                }
                break;
            case 4:
                if (Points.score4 == 100)
                {
                    gameOverText.GetComponent<Text>().text = "You won!";
                }
                else
                {
                    gameOverText.GetComponent<Text>().text = "You lost.";
                }
                break;
        }
        Thread.Sleep(2000);
        returnToStartMenu();

    }
    public void mainMenuQuit()
    {
        Application.Quit();
       
    }

    public int getPlayerNumber()
    {
        return playerNumber;
    }
  

}