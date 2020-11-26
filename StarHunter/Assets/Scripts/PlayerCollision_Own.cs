using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision_Own : MonoBehaviour
{

    PhotonView photonViewPlayer;
    PhotonView photonViewStarOwn;
    PhotonView photonViewStarOther;
    public static int starTime = 0;

    private void OnTriggerEnter(Collider other)
    {
   
        starTime = PhotonNetwork.ServerTimestamp;
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
          
            bool me = transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled;

            if (you)
            {
               
                print("GIB KROOONE!!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                photonViewStarOther.RPC("switchOff", RpcTarget.All);
            
                photonViewStarOwn.RPC("switchOn", RpcTarget.All);
              

            }

            else if (me && !you)
            {
               
                print("Krone Weg!");
                // other.transform.Find("StarGhost").GetComponent<MeshRenderer>().enabled = false;
                photonViewStarOwn.RPC("switchOff", RpcTarget.All);
                photonViewStarOther.RPC("switchOn", RpcTarget.All);
            }

        }


        transform.GetComponent<CapsuleCollider>().enabled = true;
    }
}


