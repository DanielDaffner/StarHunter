using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision_Own : MonoBehaviour
{

    PhotonView photonViewPlayer;
    PhotonView photonViewStarOwn;
    PhotonView photonViewStarOther;


    private void OnTriggerEnter(Collider other)
    {
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
          
            bool me = transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;

            if (you)
            {
                print("GIB KROOONE!!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                photonViewStarOwn.RPC("switchOn", RpcTarget.All);
                photonViewStarOther.RPC("switchOff", RpcTarget.All);

            }

            else if (me && !you)
            {
                print("Krone Weg!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                photonViewStarOwn.RPC("switchOff", RpcTarget.All);
                photonViewStarOther.RPC("switchOn", RpcTarget.All);
            }

        }


    
    }
}


