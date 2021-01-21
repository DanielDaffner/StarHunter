using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class StarSwitch : MonoBehaviour
{

    
    public PhotonView photonView;
    private bool gotHit;
    private PhotonView other;
    private float timeOn;
    private float timeOff;
    // Start is called before the first frame update
    void Start()
    {
   
    }

   
    [PunRPC]
    void switchOn()
    {
            photonView.GetComponent<MeshRenderer>().enabled = true;
         }

    [PunRPC]
    void switchOff()
    {
     
            photonView.GetComponent<MeshRenderer>().enabled = false;

      
    }

    [PunRPC]
    void setHit(string tmp)
    {
        if (gotHit) return;
   
    

        print("hitter");
        print(tmp);
        other = PhotonView.Find(int.Parse(tmp));
        gotHit = true ;

    }

    public void Update()
    {
        Transform particleSystem = gameObject.transform.GetChild(0);
        particleSystem.gameObject.active = photonView.GetComponent<MeshRenderer>().enabled;

        if (!photonView.IsMine) return;
        if (gotHit && photonView.GetComponent<MeshRenderer>().enabled)
        {
            
            photonView.RPC("switchOff", RpcTarget.AllBuffered);
            other.RPC("switchOn", RpcTarget.AllBuffered);
            gotHit = false;
        }
    }

}
