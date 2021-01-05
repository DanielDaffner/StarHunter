using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Red : MonoBehaviour
{
    public static double scoreRed = 0;
    public static int scoreGreen = 0;
    public static int scoreBlue = 0;
    public static int scoreYellow = 0;


    Text scoreR;
    Text scoreG;
    Text scoreB;
    Text scoreY;


    // Start is called before the first frame update
    void Start()
    {
        scoreR = GameObject.Find("Text").GetComponent<Text>();
        scoreG = GameObject.Find("Text2").GetComponent<Text>();
        scoreB = GameObject.Find("Text3").GetComponent<Text>();
        scoreY = GameObject.Find("Text4").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        


        if (PhotonNetwork.PlayerList.Length == 1)
        {              
            scoreR.text = "Player 1: " + scoreRed;
                    
        }
        

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            scoreR.text = "Player 1: " + scoreRed;
            scoreG.text = "Player 2: " + scoreGreen;

        }
        
        if (PhotonNetwork.PlayerList.Length == 3)
        {
            scoreR.text = "Player 1: " + scoreRed;
            scoreG.text = "Player 2: " + scoreGreen;
            scoreB.text = "Player 3: " + scoreBlue;
        }
            

        if (PhotonNetwork.PlayerList.Length == 4)
        {
            scoreR.text = "Player 1: " + scoreRed;
            scoreG.text = "Player 2: " + scoreGreen;
            scoreB.text = "Player 3: " + scoreBlue;
            scoreY.text = "Player 4: " + scoreYellow;
        }

    }
}
