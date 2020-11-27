using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision_Own : MonoBehaviour
{

    PhotonView photonViewPlayer;
    PhotonView photonViewStarOwn;
    PhotonView photonViewStarOther;
    public bool hasStar = false;
    private bool inRange = false;
    Collider otherTmp;

  

    private void OnTriggerEnter(Collider other)
    {
        otherTmp = other;
    }

    private void OnTriggerExit(Collider other)
    {
        otherTmp = null;
    }

    public void hit()
    {

        if (otherTmp == null) return;
        if (otherTmp.isTrigger) return;

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
            print("able to get star");
            print(hasStar);

        }
        print("hit");
        if (!inRange || !hasStar) return;
        photonViewStarOther.RPC("switchOff", RpcTarget.All);
        photonViewStarOwn.RPC("switchOn", RpcTarget.All);
    }

}


