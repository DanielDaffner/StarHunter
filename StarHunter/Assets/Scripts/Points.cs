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

    public GameObject[] scorePanels;

    public MyPhoton myPhoton;

    static bool keepIncrementing = false;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void startRoutine()
    {
        scorePanels[0].SetActive(false);
        scorePanels[1].SetActive(false);
        scorePanels[2].SetActive(false);
        scorePanels[3].SetActive(false);
        StartCoroutine(IncrementEachSecond()); 
    }

    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.PlayerList.Length >= 1)
        {
           
            score1T.text = "" + score1;
            scorePanels[0].SetActive(true);

        }
        

        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            score2T.text = "" + score2;
            scorePanels[1].SetActive(true);

        }
        
        if (PhotonNetwork.PlayerList.Length >= 3)
        {
            score3T.text = "" + score3;
            scorePanels[2].SetActive(true);
        }
            

        if (PhotonNetwork.PlayerList.Length >= 4)
        {
            score4T.text = "" + score4;
            scorePanels[3].SetActive(true);
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
