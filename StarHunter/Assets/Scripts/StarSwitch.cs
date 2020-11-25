using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class StarSwitch : MonoBehaviour
{
    private PhotonView photonView;
    private float timeOn;
    private float timeOff;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
      
        timeOn = Time.timeSinceLevelLoad;
        timeOff = Time.timeSinceLevelLoad;
    }

   
    [PunRPC]
    void switchOn()
    {

        if (Time.timeSinceLevelLoad > timeOn+1  )
        {
            photonView.GetComponent<MeshRenderer>().enabled = true;

            timeOn = Time.timeSinceLevelLoad;

        }
    }

    [PunRPC]
    void switchOff()
    {
        if (Time.timeSinceLevelLoad > timeOn+1)
        {
            photonView.GetComponent<MeshRenderer>().enabled = false;

            timeOn = Time.timeSinceLevelLoad;

        }
    }


}
