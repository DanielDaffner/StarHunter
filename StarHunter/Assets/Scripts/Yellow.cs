using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Yellow : MonoBehaviour
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
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            scoreR.text = "Player 4: " + scoreRed;
        }

    }
}
