using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

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
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().material = mRed;

    }
    [PunRPC]
    void setMaterialGreen()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().material = mGreen;
    }
    [PunRPC]
    void setMaterialBlue()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().material = mBlue;
    }
    [PunRPC]
    void setMaterialYellow()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().material = mYellow;
    }



}
