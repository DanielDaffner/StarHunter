using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerColor : MonoBehaviour
{
    public PhotonView photonView;
    public Material mRed;
    public Material mGreen;
    public Material mBlue;
    public Material mYellow;
    void Start()
    {
     

    
    }


    [PunRPC]
    void setMaterialRed()
    {
        photonView.GetComponent<MeshRenderer>().material = mRed;
    }
    [PunRPC]
    void setMaterialGreen()
    {
        photonView.GetComponent<MeshRenderer>().material = mGreen;
    }
    [PunRPC]
    void setMaterialBlue()
    {
        photonView.GetComponent<MeshRenderer>().material = mBlue;
    }
    [PunRPC]
    void setMaterialYellow()
    {
        photonView.GetComponent<MeshRenderer>().material = mYellow;
    }
  



}
