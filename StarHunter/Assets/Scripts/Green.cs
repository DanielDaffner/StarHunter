using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Green : MonoBehaviour
{
    public static int scoreRed = 0;


    Text scoreR;


    // Start is called before the first frame update
    void Start()
    {
        scoreR = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            scoreR.text = "Player 2: " + scoreRed;
        }



    }
}
