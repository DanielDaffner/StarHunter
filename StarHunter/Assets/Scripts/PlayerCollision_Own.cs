using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision_Own : MonoBehaviour
{

    PhotonView photonViewPlayer;
    PhotonView photonViewStarOwn;
    PhotonView photonViewStarOther;
    public bool ableToHit = false;

  

    private void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger) return;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        photonViewPlayer = GetComponent<PhotonView>();
        photonViewStarOwn = transform.Find("StarGhost").GetComponent<PhotonView>();

        photonViewStarOther = other.transform.Find("StarGhost").GetComponent<PhotonView>();
        if (!photonViewPlayer.IsMine)
        {
            print("not mine!");
            return;
           
        }

        print("Collision called from ");
        print(photonViewPlayer.ViewID);
        
        if (other.gameObject.tag == "Player" && other.gameObject !=transform.gameObject)
        {
            bool you = other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;
            ableToHit = true;
            print("able to hit");
        }


        transform.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ableToHit = false;
        print("not able to hit anymore");
    }

    public void hit()
    {
        print("hit");
        photonViewStarOther.RPC("switchOff", RpcTarget.All);
        photonViewStarOwn.RPC("switchOn", RpcTarget.All);
    }
}


