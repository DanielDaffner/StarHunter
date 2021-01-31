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
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = mRed;
    }
    [PunRPC]
    void setMaterialGreen()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = mGreen;
    }
    [PunRPC]
    void setMaterialBlue()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = mBlue;
    }
    [PunRPC]
    void setMaterialYellow()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = mYellow;
    }



}
