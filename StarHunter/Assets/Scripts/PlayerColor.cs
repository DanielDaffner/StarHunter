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
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = new Color(1f, 0, 0);
    }
    [PunRPC]
    void setMaterialGreen()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = new Color(0, 1f, 0);
    }
    [PunRPC]
    void setMaterialBlue()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = new Color(0, 0, 1f);
    }
    [PunRPC]
    void setMaterialYellow()
    {
        photonView.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].color = new Color(1f, 1f, 0);
    }



}
