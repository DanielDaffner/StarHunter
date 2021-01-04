using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class MenuControl : MonoBehaviour
{
    public MyPhoton photon;

    public GameObject mainMenu;
    public GameObject joinCreateLobby;
    public GameObject LobbyMain;
    public GameObject LobbyClient;

    public Text cLobbyName;

    public void mainMenuQuit()
    {
        Application.Quit();
    }

  
}
