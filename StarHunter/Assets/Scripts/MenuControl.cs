using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MenuControl : MonoBehaviour
{
    public MyPhoton photon;
   public void mainMenuPlay()
    {
        // old
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        switchToJoinCreateLobbyMenu();
        //
    }

    public void mainMenuQuit()
    {
        Application.Quit();
    }

    public void switchToJoinCreateLobbyMenu()
    {
      
    }
}
