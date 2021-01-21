using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCollision_Own : MonoBehaviour
{

    PhotonView photonViewPlayer;
    PhotonView photonViewStarOwn;
    PhotonView photonViewStarOther;
    public bool hasStar = false;
    private bool inRange = false;
    Collider otherTmp;
    public string starSteal = "able to get star";


    //bool keepIncrementing = false;  //pts

    void Start()
    {
        //

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled) return;
        if (other.isTrigger) return;
        otherTmp = other;
    }

    private void OnTriggerExit(Collider other)
    {
        otherTmp = null;
    }

    public void hit()
    {

        if (otherTmp == null) return;


        photonViewPlayer = GetComponent<PhotonView>();
        photonViewStarOwn = transform.Find("StarGhost").GetComponent<PhotonView>();
        photonViewStarOther = otherTmp.transform.Find("StarGhost").GetComponent<PhotonView>();
        if (!photonViewPlayer.IsMine)
        {
            print("not mine!");
            return;

        }

        print("Collision called from ");
        print(photonViewPlayer.ViewID);

        if (otherTmp.gameObject.tag == "Player" && otherTmp.gameObject != transform.gameObject)
        {
            inRange = true;
            bool you = otherTmp.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;
            hasStar = you;
            print(starSteal);
            print(hasStar);

        }
        print("hit");
        if (!inRange || !hasStar) return;

        photonViewStarOther.RPC("setHit", RpcTarget.AllBuffered, photonViewStarOwn.ViewID.ToString());
    }
        /* pts
   
        IEnumerator IncementEachSecond()
        {
            keepIncrementing = true;
            while (keepIncrementing)
             {
                //if (PhotonNetwork.PlayerList.Length == 1)
                 {
                    Points.scoreRed++;
                    yield return new WaitForSeconds(1);
                 }
                else if (PhotonNetwork.PlayerList.Length == 2)
                {
                    Red.scoreRed += 0.5;
                    
                    yield return new WaitForSeconds(1);
                }

             }*/
        //}

        
        


}


