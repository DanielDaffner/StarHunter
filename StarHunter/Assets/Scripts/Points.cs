using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Points : MonoBehaviour
{
    public static int score1 = 0;
    public static int score2 = 0;
    public static int score3 = 0;
    public static int score4 = 0;


    public Text score1T;
    public Text score2T;
    public Text score3T;
    public Text score4T;

    public MyPhoton myPhoton;

    static bool keepIncrementing = false;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void startRoutine()
    {
        StartCoroutine(IncrementEachSecond()); 
    }

    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.PlayerList.Length >= 1)
        {
           
            score1T.text = "" + score1; 
                    
        }
        

        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            score2T.text = "" + score2;

        }
        
        if (PhotonNetwork.PlayerList.Length >= 3)
        {
            score3T.text = "" + score3;
        }
            

        if (PhotonNetwork.PlayerList.Length >= 4)
        {
            score4T.text = "" + score4;
        }

    }

    IEnumerator IncrementEachSecond()
    {
      
        while (gameObject.activeSelf)
        {
          
            if (GameObject.FindGameObjectWithTag("OwnPlayer").transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled)
            {
            myPhoton.GetComponent<PhotonView>().RPC("addScore", RpcTarget.AllBuffered, myPhoton.getPlayerNumber());
            yield return new WaitForSeconds(1);
        }
            else
            {
               
                yield return new WaitForSeconds(1);
            }
        }
       
     }


}
