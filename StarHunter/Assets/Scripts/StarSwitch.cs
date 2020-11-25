using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StarSwitch : MonoBehaviour
{
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();  
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


}
